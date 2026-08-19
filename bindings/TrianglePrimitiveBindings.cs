using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class TrianglePrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTrianglePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTrianglePrimitiveSetPosition0")]
        public static extern VAResult SetPosition0(IntPtr primitive, Vector pos0);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTrianglePrimitiveSetPosition1")]
        public static extern VAResult SetPosition1(IntPtr primitive, Vector pos1);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTrianglePrimitiveSetPosition2")]
        public static extern VAResult SetPosition2(IntPtr primitive, Vector pos2);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTrianglePrimitiveGetPosition0")]
        public static extern Vector GetPosition0(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTrianglePrimitiveGetPosition1")]
        public static extern Vector GetPosition1(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTrianglePrimitiveGetPosition2")]
        public static extern Vector GetPosition2(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTrianglePrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);
    }
}
