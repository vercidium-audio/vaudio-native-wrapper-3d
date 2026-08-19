using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static partial class WorldBindings
    {
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
    }
}
