using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class MeshPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveCreate")]
        public static extern unsafe VAResult Create(MaterialType material, Vector* vertices, int vertexCount, Vector minBounds, Vector maxBounds, ref Matrix transform, IntPtr* outPrimitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveSetTransform")]
        public static extern VAResult SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveGetUseFlatTransmission")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetUseFlatTransmission(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveSetUseFlatTransmission")]
        public static extern VAResult SetUseFlatTransmission(IntPtr primitive, bool useFlatTransmission);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveCreateFromMesh")]
        public static extern unsafe VAResult CreatePrimitiveFromMesh(MaterialType material, IntPtr mesh, ref Matrix transform, IntPtr* outPrimitive);
    }
}
