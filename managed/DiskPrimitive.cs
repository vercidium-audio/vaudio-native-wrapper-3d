using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A circular disk audio primitive</summary>
    public unsafe class DiskPrimitive : Primitive
    {
        /// <summary>Create a disk primitive</summary>
        public DiskPrimitive()
        {
            native = DiskPrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Radius of the disk</summary>
        public float radius
        {
            get => DiskPrimitiveBindings.GetRadius(native);
            set => DiskPrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *DiskPrimitiveBindings.GetTransform(native);
            set => DiskPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => DiskPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, radius={radius}";
    }
}
