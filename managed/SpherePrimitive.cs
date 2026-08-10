using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A spherical audio primitive</summary>
    public class SpherePrimitive : Primitive
    {
        public SpherePrimitive()
        {
            native = SpherePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => SpherePrimitiveBindings.GetMaterial(native);
            set => SpherePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Center position of the sphere in world space</summary>
        public Vector center
        {
            get => SpherePrimitiveBindings.GetCenter(native);
            set => SpherePrimitiveBindings.SetCenter(native, value).ThrowIfError();
        }

        /// <summary>Radius of the sphere</summary>
        public float radius
        {
            get => SpherePrimitiveBindings.GetRadius(native);
            set => SpherePrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        public void Destroy()
        {
            SpherePrimitiveBindings.Destroy(native).ThrowIfError();
            native = IntPtr.Zero;
        }

        protected override string DebugInfo => $"material={material}, center={center}, radius={radius}";
    }
}
