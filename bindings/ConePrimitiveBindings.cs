using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class ConePrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaConePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaConePrimitiveSetRadius")]
        public static extern VAResult SetRadius(IntPtr primitive, float radius);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaConePrimitiveSetHeight")]
        public static extern VAResult SetHeight(IntPtr primitive, float height);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaConePrimitiveSetTransform")]
        public static extern VAResult SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaConePrimitiveGetRadius")]
        public static extern float GetRadius(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaConePrimitiveGetHeight")]
        public static extern float GetHeight(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaConePrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaConePrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);
    }
}
