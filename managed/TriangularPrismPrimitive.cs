using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A triangular prism primitive</summary>
    public unsafe class TriangularPrismPrimitive : Primitive
    {
        public TriangularPrismPrimitive()
        {
            native = TriangularPrismPrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => TriangularPrismPrimitiveBindings.GetMaterial(native);
            set => TriangularPrismPrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Circumradius of the triangular cross-section</summary>
        public float radius
        {
            get => TriangularPrismPrimitiveBindings.GetRadius(native);
            set => TriangularPrismPrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Length of the prism along its axis</summary>
        public float length
        {
            get => TriangularPrismPrimitiveBindings.GetLength(native);
            set => TriangularPrismPrimitiveBindings.SetLength(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *TriangularPrismPrimitiveBindings.GetTransform(native);
            set => TriangularPrismPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        public void Destroy()
        {
            TriangularPrismPrimitiveBindings.Destroy(native).ThrowIfError();
            native = IntPtr.Zero;
        }

        protected override string DebugInfo => $"material={material}, radius={radius}, length={length}";
    }
}
