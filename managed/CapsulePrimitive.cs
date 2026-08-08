using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A capsule (cylinder with hemispherical caps) audio primitive</summary>
    public unsafe class CapsulePrimitive : Primitive
    {
        public CapsulePrimitive()
        {
            native = CapsulePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => CapsulePrimitiveBindings.GetMaterial(native);
            set => CapsulePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Radius of the capsule</summary>
        public float radius
        {
            get => CapsulePrimitiveBindings.GetRadius(native);
            set => CapsulePrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Length of the cylindrical body between the hemispherical caps</summary>
        public float length
        {
            get => CapsulePrimitiveBindings.GetLength(native);
            set => CapsulePrimitiveBindings.SetLength(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *CapsulePrimitiveBindings.GetTransform(native);
            set => CapsulePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        public void Destroy()
        {
            CapsulePrimitiveBindings.Destroy(native).ThrowIfError();
            native = IntPtr.Zero;
        }

        protected override string DebugInfo => $"material={material}, radius={radius}, length={length}";
    }
}
