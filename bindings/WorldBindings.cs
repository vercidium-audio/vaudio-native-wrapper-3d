using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate MaterialType UnsafeVoxelMaterialCallback(void* voxel, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe delegate bool UnsafeVoxelIsSolidCallback(void* voxel, IntPtr userData);

    public static partial class WorldBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetUnsafeVoxelMaterialCallbacks")]
        public static extern VAResult SetUnsafeVoxelMaterialCallbacks(IntPtr world, IntPtr getMaterial, IntPtr isSolid, IntPtr userData);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetUnsafeVoxelMaterialCallback")]
        public static extern IntPtr GetUnsafeVoxelMaterialCallback(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetUnsafeVoxelIsSolidCallback")]
        public static extern IntPtr GetUnsafeVoxelIsSolidCallback(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetCameraPitch")]
        public static extern float GetCameraPitch(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetCameraPitch")]
        public static extern VAResult SetCameraPitch(IntPtr world, float pitch);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetCameraYaw")]
        public static extern float GetCameraYaw(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetCameraYaw")]
        public static extern VAResult SetCameraYaw(IntPtr world, float yaw);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetFieldOfView")]
        public static extern float GetFieldOfView(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetFieldOfView")]
        public static extern VAResult SetFieldOfView(IntPtr world, float fieldOfView);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldCalculateListenerRelativePan")]
        public static extern Vector CalculateListenerRelativePan(IntPtr ctx, Vector worldVector, float listenerPitch, float listenerYaw);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldConvertWorldToListenerDirection")]
        public static extern Vector ConvertWorldToListenerDirection(IntPtr ctx, Vector worldDirection, float listenerPitch, float listenerYaw);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldConvertListenerToWorldDirection")]
        public static extern Vector ConvertListenerToWorldDirection(IntPtr ctx, Vector listenerDirection, float listenerPitch, float listenerYaw);
    }
}
