using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void OnRaytracingCompleteFn(IntPtr emitter);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void OnReverbUpdatedFn();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void OnRaytracedByAnotherEmitterFn(IntPtr source, IntPtr target);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void OnRemovedFn();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void VisualisationCallbackFn(IntPtr emitter, VisualisationData* data, int count);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float GainFormulaDelegate(bool lowFrequency, int occlusionRayCount, int permeationRayCount, int permeationBounceCount, float occlusionEnergy, float permeationEnergy);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogCallbackFn([MarshalAs(UnmanagedType.LPStr)] string message);

    public static class EmitterBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterDestroy")]
        public static extern VAResult Destroy(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterAddTarget")]
        public static extern VAResult AddTarget(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterRemoveTarget")]
        public static extern VAResult RemoveTarget(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterHasTarget")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool HasTarget(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterHasRaytracedTarget")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool HasRaytracedTarget(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetTargetFilter")]
        public static extern unsafe LowPassFilter* GetTargetFilter(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterResetTrails")]
        public static extern VAResult ResetTrails(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetReverbRayCount")]
        public static extern int GetReverbRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetReverbRayCount")]
        public static extern VAResult SetReverbRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetReverbBounceCount")]
        public static extern int GetReverbBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetReverbBounceCount")]
        public static extern VAResult SetReverbBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOcclusionRayCount")]
        public static extern int GetOcclusionRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOcclusionRayCount")]
        public static extern VAResult SetOcclusionRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOcclusionBounceCount")]
        public static extern int GetOcclusionBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOcclusionBounceCount")]
        public static extern VAResult SetOcclusionBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPermeationRayCount")]
        public static extern int GetPermeationRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetPermeationRayCount")]
        public static extern VAResult SetPermeationRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPermeationBounceCount")]
        public static extern int GetPermeationBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetPermeationBounceCount")]
        public static extern VAResult SetPermeationBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientOcclusionRayCount")]
        public static extern int GetAmbientOcclusionRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientOcclusionRayCount")]
        public static extern VAResult SetAmbientOcclusionRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientOcclusionBounceCount")]
        public static extern int GetAmbientOcclusionBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientOcclusionBounceCount")]
        public static extern VAResult SetAmbientOcclusionBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientPermeationRayCount")]
        public static extern int GetAmbientPermeationRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientPermeationRayCount")]
        public static extern VAResult SetAmbientPermeationRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientPermeationBounceCount")]
        public static extern int GetAmbientPermeationBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientPermeationBounceCount")]
        public static extern VAResult SetAmbientPermeationBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetVisualisationRayCount")]
        public static extern int GetVisualisationRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetVisualisationRayCount")]
        public static extern VAResult SetVisualisationRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetVisualisationBounceCount")]
        public static extern int GetVisualisationBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetVisualisationBounceCount")]
        public static extern VAResult SetVisualisationBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetVisualisationUpdateFrequency")]
        public static extern int GetVisualisationUpdateFrequency(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetVisualisationUpdateFrequency")]
        public static extern VAResult SetVisualisationUpdateFrequency(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetTrailCount")]
        public static extern int GetTrailCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetTrailBounceCount")]
        public static extern int GetTrailBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetMaxEchogramTime")]
        public static extern int GetMaxEchogramTime(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetMaxEchogramTime")]
        public static extern VAResult SetMaxEchogramTime(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetEchogramGranularity")]
        public static extern int GetEchogramGranularity(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetEchogramGranularity")]
        public static extern VAResult SetEchogramGranularity(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetRefreshRayCount")]
        public static extern int GetRefreshRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetRefreshRayCount")]
        public static extern VAResult SetRefreshRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetRefreshDistanceThreshold")]
        public static extern float GetRefreshDistanceThreshold(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetRefreshDistanceThreshold")]
        public static extern VAResult SetRefreshDistanceThreshold(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetReverbEnergyCap")]
        public static extern float GetReverbEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetReverbEnergyCap")]
        public static extern VAResult SetReverbEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetMaxVolume")]
        public static extern float GetMaxVolume(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetMaxVolume")]
        public static extern VAResult SetMaxVolume(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOcclusionEnergyCap")]
        public static extern float GetOcclusionEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOcclusionEnergyCap")]
        public static extern VAResult SetOcclusionEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPermeationEnergyCap")]
        public static extern float GetPermeationEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetPermeationEnergyCap")]
        public static extern VAResult SetPermeationEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientOcclusionEnergyCap")]
        public static extern float GetAmbientOcclusionEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientOcclusionEnergyCap")]
        public static extern VAResult SetAmbientOcclusionEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientPermeationEnergyCap")]
        public static extern float GetAmbientPermeationEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientPermeationEnergyCap")]
        public static extern VAResult SetAmbientPermeationEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetMinimumPermeationEnergy")]
        public static extern float GetMinimumPermeationEnergy(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetMinimumPermeationEnergy")]
        public static extern VAResult SetMinimumPermeationEnergy(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetReverbEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool ReverbEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOcclusionEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool OcclusionEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPermeationEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool PermeationEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientOcclusionEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AmbientOcclusionEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientPermeationEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AmbientPermeationEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetVisualisationEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool VisualisationEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetCastsAnyRays")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool CastsAnyRays(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetWithinWorldBounds")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool WithinWorldBounds(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOverridePositions")]
        public static extern unsafe VAResult SetOverridePositions(IntPtr emitter, Vector* positions, int count);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOverrideRayDirections")]
        public static extern unsafe VAResult SetOverrideRayDirections(IntPtr emitter, Vector* directions, int count);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetGainFormula")]
        public static extern VAResult SetGainFormula(IntPtr emitter, IntPtr formula);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientGainFormula")]
        public static extern VAResult SetAmbientGainFormula(IntPtr emitter, IntPtr formula);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetLogCallback")]
        public static extern VAResult SetLogCallback(IntPtr emitter, LogCallbackFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetLogErrorCallback")]
        public static extern VAResult SetLogErrorCallback(IntPtr emitter, LogCallbackFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOnRaytracingCompleteCallback")]
        public static extern VAResult SetOnRaytracingCompleteCallback(IntPtr emitter, OnRaytracingCompleteFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOnRaytracedByAnotherEmitterCallback")]
        public static extern VAResult SetOnRaytracedByAnotherEmitterCallback(IntPtr emitter, OnRaytracedByAnotherEmitterFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOnRemovedCallback")]
        public static extern VAResult SetOnRemovedCallback(IntPtr emitter, OnRemovedFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetVisualisationCallback")]
        public static extern VAResult SetVisualisationCallback(IntPtr emitter, VisualisationCallbackFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetPosition")]
        public static extern VAResult SetPosition(IntPtr emitter, Vector position);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPosition")]
        public static extern Vector GetPosition(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetInitialising")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetInitialising(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPendingRemoval")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetPendingRemoval(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAffectsGroupedEAX")]
        public static extern VAResult SetAffectsGroupedEAX(IntPtr emitter, bool value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAffectsGroupedEAX")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetAffectsGroupedEAX(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetGroupedEAXIndex")]
        public static extern int GetGroupedEAXIndex(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOutsidePercent")]
        public static extern float GetOutsidePercent(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetHasRelativeReverb")]
        public static extern VAResult SetHasRelativeReverb(IntPtr emitter, bool value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetHasRelativeReverb")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetHasRelativeReverb(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetRelativeReverbInnerThreshold")]
        public static extern VAResult SetRelativeReverbInnerThreshold(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetRelativeReverbInnerThreshold")]
        public static extern float GetRelativeReverbInnerThreshold(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetRelativeReverbOuterThreshold")]
        public static extern VAResult SetRelativeReverbOuterThreshold(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetRelativeReverbOuterThreshold")]
        public static extern float GetRelativeReverbOuterThreshold(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetClampPosition")]
        public static extern VAResult SetClampPosition(IntPtr emitter, bool value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetClampPosition")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetClampPosition(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetScatteringSeed")]
        public static extern VAResult SetScatteringSeed(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetScatteringSeed")]
        public static extern int GetScatteringSeed(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetName")]
        public static extern VAResult SetName(IntPtr emitter, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetName")]
        private static extern IntPtr GetNameRaw(IntPtr emitter);
        public static string GetName(IntPtr emitter) => Marshal.PtrToStringAnsi(GetNameRaw(emitter));

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetType")]
        public static extern VAResult SetType(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetType")]
        public static extern int GetType(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetUserData")]
        public static extern VAResult SetUserData(IntPtr emitter, IntPtr userData);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetUserData")]
        public static extern IntPtr GetUserData(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetProcessedReverb")]
        public static extern unsafe ProcessedReverb* GetProcessedReverb(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetEAX")]
        public static extern unsafe EAXReverb* GetEAX(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientFilter")]
        public static extern unsafe LowPassFilter* GetAmbientFilter(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetRandomTrailColor")]
        public static extern VAResult SetRandomTrailColor(IntPtr emitter, bool value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetRandomTrailColor")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetRandomTrailColor(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetTrailColor")]
        public static extern VAResult SetTrailColor(IntPtr emitter, Color value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetTrailColor")]
        public static extern Color GetTrailColor(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetReverbColor")]
        public static extern VAResult SetReverbColor(IntPtr emitter, Color value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetReverbColor")]
        public static extern Color GetReverbColor(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOcclusionColor")]
        public static extern VAResult SetOcclusionColor(IntPtr emitter, Color value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOcclusionColor")]
        public static extern Color GetOcclusionColor(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetPermeationColor")]
        public static extern VAResult SetPermeationColor(IntPtr emitter, Color value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPermeationColor")]
        public static extern Color GetPermeationColor(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientPermeationColor")]
        public static extern VAResult SetAmbientPermeationColor(IntPtr emitter, Color value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientPermeationColor")]
        public static extern Color GetAmbientPermeationColor(IntPtr emitter);
    }
}
