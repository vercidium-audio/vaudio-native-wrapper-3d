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

        protected override VAResult DestroyNative(IntPtr native) => SpherePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, center={center}, radius={radius}";
    }
}
