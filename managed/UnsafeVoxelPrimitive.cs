using System;

namespace vaudionativewrapper.managed
{
    /// <summary>
    /// A 3D grid of voxels that reads directly from unmanaged memory owned by the caller, rather than copying it.<br/>
    /// Each voxel is converted to a material by the world's <see cref="World.UnsafeVoxelMaterialMap"/>, which must be set before adding this primitive to a world.<br/>
    /// The memory is read from background threads, and must not be freed until this primitive is removed from its world.
    /// </summary>
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

        /// <summary>Create a new voxel primitive that uses the same memory layout as VoxelPrimitive: (x * height + y) * depth + z</summary>
        /// <exception cref="ArgumentException">Thrown if data is null, or width, height, depth or stride are &lt;= 0</exception>
        public UnsafeVoxelPrimitive(void* data, int width, int height, int depth, int stride)
            : this(data, width, height, depth, stride, height * depth, depth, 1)
        {
        }

        /// <summary>Create a new voxel primitive with a custom memory layout</summary>
        /// <exception cref="ArgumentException">Thrown if data is null, or width, height, depth, stride or a pitch are &lt;= 0</exception>
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

        /// <summary>Returns the material of the voxel at (x, y, z), using the world's <see cref="World.UnsafeVoxelMaterialMap"/>. Returns <see cref="MaterialType.Air"/> if this primitive hasn't been added to a world</summary>
        public MaterialType GetMaterial(int x, int y, int z) => UnsafeVoxelPrimitiveBindings.GetVoxel(native, x, y, z);

        /// <summary>Returns true if the voxel at (x, y, z) is solid, i.e. its material is not <see cref="MaterialType.Air"/></summary>
        public bool IsSolid(int x, int y, int z) => UnsafeVoxelPrimitiveBindings.IsSolid(native, x, y, z);

        /// <summary>Call this after editing voxel data</summary>
        public void SetDataDirty() => UnsafeVoxelPrimitiveBindings.SetDataDirty(native).ThrowIfError();

        protected override VAResult DestroyNative(IntPtr native) => UnsafeVoxelPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, width={width}, height={height}, depth={depth}, stride={stride}, scale={scale}";
    }
}
