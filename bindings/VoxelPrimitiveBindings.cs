using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class VoxelPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveCreate")]
        public static extern IntPtr Create(int width, int height, int depth);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveGetScale")]
        public static extern float GetScale(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveSetScale")]
        public static extern VAResult SetScale(IntPtr primitive, float scale);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveSetTransform")]
        public static extern VAResult SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveGetSize")]
        public static extern VAResult GetSize(IntPtr primitive, out int width, out int height, out int depth);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveGetVoxel")]
        public static extern MaterialType GetVoxel(IntPtr primitive, int x, int y, int z);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveSetVoxel")]
        public static extern VAResult SetVoxel(IntPtr primitive, int x, int y, int z, MaterialType material);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaVoxelPrimitiveSetDataDirty")]
        public static extern VAResult SetDataDirty(IntPtr primitive);
    }
}
