using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A flat plane audio primitive</summary>
    public unsafe class PlanePrimitive : Primitive
    {
        public PlanePrimitive()
        {
            native = PlanePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => PlanePrimitiveBindings.GetMaterial(native);
            set => PlanePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Width of the plane</summary>
        public float width
        {
            get => PlanePrimitiveBindings.GetWidth(native);
            set => PlanePrimitiveBindings.SetWidth(native, value).ThrowIfError();
        }

        /// <summary>Height of the plane</summary>
        public float height
        {
            get => PlanePrimitiveBindings.GetHeight(native);
            set => PlanePrimitiveBindings.SetHeight(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *PlanePrimitiveBindings.GetTransform(native);
            set => PlanePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        public void Destroy()
        {
            PlanePrimitiveBindings.Destroy(native).ThrowIfError();
            native = IntPtr.Zero;
        }

        protected override string DebugInfo => $"material={material}, width={width}, height={height}";
    }
}
