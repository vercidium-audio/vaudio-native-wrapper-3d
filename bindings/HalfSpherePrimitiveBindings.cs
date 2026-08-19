using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class HalfSpherePrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHalfSpherePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHalfSpherePrimitiveSetRadius")]
        public static extern VAResult SetRadius(IntPtr primitive, float radius);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHalfSpherePrimitiveSetTransform")]
        public static extern VAResult SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHalfSpherePrimitiveGetRadius")]
        public static extern float GetRadius(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHalfSpherePrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHalfSpherePrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);
    }
}
