using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A rectangular cone audio primitive</summary>
    public unsafe class RectangularConePrimitive : Primitive
    {
        public RectangularConePrimitive()
        {
            native = RectangularConePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => RectangularConePrimitiveBindings.GetMaterial(native);
            set => RectangularConePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Width of the rectangular base</summary>
        public float width
        {
            get => RectangularConePrimitiveBindings.GetWidth(native);
            set => RectangularConePrimitiveBindings.SetWidth(native, value).ThrowIfError();
        }

        /// <summary>Length of the rectangular base</summary>
        public float length
        {
            get => RectangularConePrimitiveBindings.GetLength(native);
            set => RectangularConePrimitiveBindings.SetLength(native, value).ThrowIfError();
        }

        /// <summary>Height of the cone from base to apex</summary>
        public float height
        {
            get => RectangularConePrimitiveBindings.GetHeight(native);
            set => RectangularConePrimitiveBindings.SetHeight(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *RectangularConePrimitiveBindings.GetTransform(native);
            set => RectangularConePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        public void Destroy()
        {
            RectangularConePrimitiveBindings.Destroy(native).ThrowIfError();
            native = IntPtr.Zero;
        }

        protected override string DebugInfo => $"material={material}, width={width}, length={length}, height={height}";
    }
}
