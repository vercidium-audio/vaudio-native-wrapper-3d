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

        /// <summary>Radius of the triangle face</summary>
        public float radius
        {
            get => TriangularPrismPrimitiveBindings.GetRadius(native);
            set => TriangularPrismPrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Length of the prism along its body</summary>
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

        protected override VAResult DestroyNative(IntPtr native) => TriangularPrismPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, radius={radius}, length={length}";
    }
}
