using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    /// <summary>A 3D grid of voxels that reads directly from unmanaged memory owned by the caller, rather than copying it into a managed array. Each voxel is converted to a material by the world's UnsafeVoxelMaterialMap, which must be set before adding this primitive to a world. Voxel memory is read by the raytracing threads without synchronisation, and must remain valid until this primitive is removed from its world and its OnRemoved callback is invoked.</summary>
    public unsafe class UnsafeVoxelPrimitive : Primitive
    {
        /// <summary>Pointer to the first voxel. Owned by the caller</summary>
        public readonly void* data;

        /// <summary>Grid size</summary>
        public readonly int width, height, depth;

        /// <summary>Size of each voxel in bytes</summary>
        public readonly int stride;

        /// <summary>Number of voxels between neighbouring voxels along each axis, i.e. the voxel at (x, y, z) is at data + (x * xPitch + y * yPitch + z * zPitch) * stride</summary>
        public readonly int xPitch, yPitch, zPitch;

        /// <summary>Invoked on the main thread once this primitive has been removed from its world and the raytracing threads no longer use its voxel data. This is invoked by Update, or by RemovePrimitive if this primitive was never sent to the raytracing threads.</summary>
        public Action OnRemoved;

        // Keep a delegate for native callbacks
        static readonly UnsafeVoxelRemovedCallback removedCallback = OnRemovedNative;
        static readonly IntPtr removedCallbackPtr = Marshal.GetFunctionPointerForDelegate(removedCallback);

        GCHandle handle;
        int pendingRemovedCallbacks;
        bool destroyed;

        /// <summary>Create a new voxel primitive that uses the same memory layout as VoxelPrimitive: (x * height + y) * depth + z</summary>
        /// <exception cref="ArgumentException">Thrown if data is null, or width, height, depth or stride are &lt;= 0</exception>
        public UnsafeVoxelPrimitive(void* data, int width, int height, int depth, int stride)
            : this(data, width, height, depth, stride, height * depth, depth, 1)
        {
        }

        /// <summary>Create a new voxel primitive with a custom memory layout</summary>
        public UnsafeVoxelPrimitive(void* data, int width, int height, int depth, int stride, int xPitch, int yPitch, int zPitch)
        {
            native = UnsafeVoxelPrimitiveBindings.CreateWithPitch((IntPtr)data, width, height, depth, stride, xPitch, yPitch, zPitch);

            if (native == IntPtr.Zero)
                throw new ArgumentException($"Invalid UnsafeVoxelPrimitive arguments: data=0x{(ulong)data:X}, width={width}, height={height}, depth={depth}, stride={stride}, xPitch={xPitch}, yPitch={yPitch}, zPitch={zPitch}");

            owns = true;

            this.data = data;
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.stride = stride;
            this.xPitch = xPitch;
            this.yPitch = yPitch;
            this.zPitch = zPitch;

            handle = GCHandle.Alloc(this);
            UnsafeVoxelPrimitiveBindings.SetRemovedCallback(native, removedCallbackPtr, GCHandle.ToIntPtr(handle)).ThrowIfError();
        }

        /// <summary>Scale of the voxels</summary>
        public float scale
        {
            get => UnsafeVoxelPrimitiveBindings.GetScale(native);
            set => UnsafeVoxelPrimitiveBindings.SetScale(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *UnsafeVoxelPrimitiveBindings.GetTransform(native);
            set => UnsafeVoxelPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        /// <summary>Returns the material of the voxel at (x, y, z), using the world's UnsafeVoxelMaterialMap</summary>
        public MaterialType GetMaterial(int x, int y, int z) => UnsafeVoxelPrimitiveBindings.GetVoxel(native, x, y, z);

        /// <summary>Returns true if the voxel at (x, y, z) is solid, using the world's UnsafeVoxelMaterialMap</summary>
        public bool IsSolid(int x, int y, int z) => UnsafeVoxelPrimitiveBindings.IsSolid(native, x, y, z);

        /// <summary>Call this after editing voxel data</summary>
        public void SetDataDirty() => UnsafeVoxelPrimitiveBindings.SetDataDirty(native).ThrowIfError();

        // A primitive can be added and removed from a world multiple times, so ensure we invoke OnRemoved the correct amount of times
        internal override void OnAddedToWorld() => pendingRemovedCallbacks++;

        static void OnRemovedNative(IntPtr userData)
        {
            var primitive = (UnsafeVoxelPrimitive)GCHandle.FromIntPtr(userData).Target;
            primitive.pendingRemovedCallbacks--;

            // Exceptions must not propagate into native code
            try
            {
                primitive.OnRemoved?.Invoke();
            }
            catch (Exception e)
            {
                LogSettings.Error($"The OnRemoved callback for an UnsafeVoxelPrimitive threw this exception: {e}");
            }

            primitive.FreeHandleIfUnused();
        }

        void FreeHandleIfUnused()
        {
            if (destroyed && pendingRemovedCallbacks == 0 && handle.IsAllocated)
                handle.Free();
        }

        protected override VAResult DestroyNative(IntPtr native)
        {
            var result = UnsafeVoxelPrimitiveBindings.Destroy(native);

            if (result == VAResult.Success)
            {
                destroyed = true;
                FreeHandleIfUnused();
            }

            return result;
        }

        protected override string DebugInfo => $"material={material}, width={width}, height={height}, depth={depth}, stride={stride}, scale={scale}";
    }
}
