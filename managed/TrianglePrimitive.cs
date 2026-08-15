using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A triangle primitive</summary>
    public class TrianglePrimitive : Primitive
    {
        public TrianglePrimitive()
        {
            native = TrianglePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => TrianglePrimitiveBindings.GetMaterial(native);
            set => TrianglePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>First vertex of the triangle</summary>
        public Vector position0
        {
            get => TrianglePrimitiveBindings.GetPosition0(native);
            set => TrianglePrimitiveBindings.SetPosition0(native, value).ThrowIfError();
        }

        /// <summary>Second vertex of the triangle</summary>
        public Vector position1
        {
            get => TrianglePrimitiveBindings.GetPosition1(native);
            set => TrianglePrimitiveBindings.SetPosition1(native, value).ThrowIfError();
        }

        /// <summary>Third vertex of the triangle</summary>
        public Vector position2
        {
            get => TrianglePrimitiveBindings.GetPosition2(native);
            set => TrianglePrimitiveBindings.SetPosition2(native, value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => TrianglePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, position0={position0}, position1={position1}, position2={position2}";
    }
}
