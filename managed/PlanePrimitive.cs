using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A flat plane audio primitive</summary>
    public unsafe class PlanePrimitive : Primitive
    {
        /// <summary>Create a plane primitive</summary>
        public PlanePrimitive()
        {
            native = PlanePrimitiveBindings.Create();
            owns = true;
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

        protected override VAResult DestroyNative(IntPtr native) => PlanePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, width={width}, height={height}";
    }
}
