using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A hemispherical audio primitive</summary>
    public unsafe class HalfSpherePrimitive : Primitive
    {
        public HalfSpherePrimitive()
        {
            native = HalfSpherePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => HalfSpherePrimitiveBindings.GetMaterial(native);
            set => HalfSpherePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Radius of the hemisphere</summary>
        public float radius
        {
            get => HalfSpherePrimitiveBindings.GetRadius(native);
            set => HalfSpherePrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *HalfSpherePrimitiveBindings.GetTransform(native);
            set => HalfSpherePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => HalfSpherePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, radius={radius}";
    }
}
