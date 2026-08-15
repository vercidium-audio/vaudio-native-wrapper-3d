using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A cylindrical audio primitive</summary>
    public unsafe class CylinderPrimitive : Primitive
    {
        public CylinderPrimitive()
        {
            native = CylinderPrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => CylinderPrimitiveBindings.GetMaterial(native);
            set => CylinderPrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Radius of the cylinder</summary>
        public float radius
        {
            get => CylinderPrimitiveBindings.GetRadius(native);
            set => CylinderPrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Length of the cylinder along its axis</summary>
        public float length
        {
            get => CylinderPrimitiveBindings.GetLength(native);
            set => CylinderPrimitiveBindings.SetLength(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *CylinderPrimitiveBindings.GetTransform(native);
            set => CylinderPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => CylinderPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, radius={radius}, length={length}";
    }
}
