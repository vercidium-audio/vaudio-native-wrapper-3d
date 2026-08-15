using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A cone primitive with a triangular cross-section</summary>
    public unsafe class TriangularConePrimitive : Primitive
    {
        public TriangularConePrimitive()
        {
            native = TriangularConePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => TriangularConePrimitiveBindings.GetMaterial(native);
            set => TriangularConePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>The radius of the triangular base</summary>
        public float radius
        {
            get => TriangularConePrimitiveBindings.GetRadius(native);
            set => TriangularConePrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>The height of the cone</summary>
        public float height
        {
            get => TriangularConePrimitiveBindings.GetHeight(native);
            set => TriangularConePrimitiveBindings.SetHeight(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *TriangularConePrimitiveBindings.GetTransform(native);
            set => TriangularConePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => TriangularConePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, radius={radius}, height={height}";
    }
}
