using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class UnsafeVoxelPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveCreate")]
        public static extern IntPtr Create(IntPtr data, int width, int height, int depth, int stride);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveCreateWithPitch")]
        public static extern IntPtr CreateWithPitch(IntPtr data, int width, int height, int depth, int stride, int xPitch, int yPitch, int zPitch);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveGetScale")]
        public static extern float GetScale(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveSetScale")]
        public static extern VAResult SetScale(IntPtr primitive, float scale);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveSetTransform")]
        public static extern VAResult SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveGetSize")]
        public static extern VAResult GetSize(IntPtr primitive, out int width, out int height, out int depth);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveGetPitch")]
        public static extern VAResult GetPitch(IntPtr primitive, out int xPitch, out int yPitch, out int zPitch);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveGetStride")]
        public static extern int GetStride(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveGetData")]
        public static extern IntPtr GetData(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveGetVoxel")]
        public static extern MaterialType GetVoxel(IntPtr primitive, int x, int y, int z);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveIsSolid")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool IsSolid(IntPtr primitive, int x, int y, int z);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaUnsafeVoxelPrimitiveSetDataDirty")]
        public static extern VAResult SetDataDirty(IntPtr primitive);
    }
}
