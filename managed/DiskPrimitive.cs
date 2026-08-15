using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A circular disk audio primitive</summary>
    public unsafe class DiskPrimitive : Primitive
    {
        public DiskPrimitive()
        {
            native = DiskPrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => DiskPrimitiveBindings.GetMaterial(native);
            set => DiskPrimitiveBindings.SetMaterial(native, value).ThrowIfError();
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
