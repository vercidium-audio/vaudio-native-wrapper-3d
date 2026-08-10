using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A circular cone audio primitive</summary>
    public unsafe class ConePrimitive : Primitive
    {
        public ConePrimitive()
        {
            native = ConePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => ConePrimitiveBindings.GetMaterial(native);
            set => ConePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Radius of the cone base</summary>
        public float radius
        {
            get => ConePrimitiveBindings.GetRadius(native);
            set => ConePrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Height of the cone from base to apex</summary>
        public float height
        {
            get => ConePrimitiveBindings.GetHeight(native);
            set => ConePrimitiveBindings.SetHeight(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *ConePrimitiveBindings.GetTransform(native);
            set => ConePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        public void Destroy()
        {
            ConePrimitiveBindings.Destroy(native).ThrowIfError();
            native = IntPtr.Zero;
        }

        protected override string DebugInfo => $"material={material}, radius={radius}, height={height}";
    }
}
