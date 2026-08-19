using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class RectangularConePrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveSetWidth")]
        public static extern VAResult SetWidth(IntPtr primitive, float width);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveSetLength")]
        public static extern VAResult SetLength(IntPtr primitive, float length);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveSetHeight")]
        public static extern VAResult SetHeight(IntPtr primitive, float height);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveSetTransform")]
        public static extern VAResult SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveGetWidth")]
        public static extern float GetWidth(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveGetLength")]
        public static extern float GetLength(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveGetHeight")]
        public static extern float GetHeight(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);
    }
}
