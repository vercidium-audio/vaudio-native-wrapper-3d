using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    /// <summary>A 3D position that casts rays and is discovered by other Emitters</summary>
    public unsafe class Emitter
    {
        public IntPtr native;
        private readonly bool owns;

#region Functions
        public Emitter(IntPtr native)
        {
            this.native = native;
        }

        /// <summary>Create a new Emitter with default settings</summary>
        public Emitter()
        {
            native = EmitterBindings.Create();
            owns = true;
        }

        /// <summary>Free the emitter. Throws if the emitter is still added to a world.</summary>
        public void Destroy()
        {
            EmitterBindings.Destroy(native).ThrowIfError();
            native = IntPtr.Zero;
        }

        ~Emitter()
        {
            if (owns && native != IntPtr.Zero)
            {
                string name;
                try { name = Name; } catch { name = "<unknown>"; }

                LogSettings.Warn($"Emitter '{name}' was garbage collected without calling Destroy() first.");
            }
        }

        /// <summary>Adds an emitter to this emitter's target list</summary>
        public void AddTarget(Emitter target) => EmitterBindings.AddTarget(native, target.native);

        /// <summary>Removes an emitter from this emitter's target list</summary>
        public void RemoveTarget(Emitter target) => EmitterBindings.RemoveTarget(native, target.native).ThrowIfError();

        /// <summary>Returns whether the target emitter is in this emitter's target list</summary>
        public bool HasTarget(Emitter target) => EmitterBindings.HasTarget(native, target.native);

        /// <summary>Returns whether a target emitter has been raytraced. If true, it is safe to check the target's filter via GetTargetFilter</summary>
        public bool HasRaytracedTarget(Emitter target) => EmitterBindings.HasRaytracedTarget(native, target.native);

        /// <summary>Get an object containing the LF and HF gain for a target emitter, to be applied to a low pass filter. Only access when HasRaytracedTarget(Emitter) is true.</summary>
        public LowPassFilter* GetTargetFilter(Emitter target) => EmitterBindings.GetTargetFilter(native, target.native);

        /// <summary>Call this function to invalidate the ray cache - all memory will be cleaned up and all rays will be re-cast</summary>
        public void ResetTrails() => EmitterBindings.ResetTrails(native).ThrowIfError();
#endregion

#region Properties
        /// <summary>The position of this Emitter. Can be a Vector3F or IPosition</summary>
        public Vector Position
        {
            get => EmitterBindings.GetPosition(native);
            set => EmitterBindings.SetPosition(native, value).ThrowIfError();
        }

        /// <summary>True if this emitter hasn't cast its own rays yet.</summary>
        public bool Initialising
        {
            get => EmitterBindings.GetInitialising(native);
        }

        /// <summary>Whether this emitter has been marked for removal when its reverb tail has finished playing</summary>
        public bool PendingRemoval
        {
            get => EmitterBindings.GetPendingRemoval(native);
        }

        /// <summary>Controls whether this Emitter's EAX is blended to produced grouped EAX. Set this to false for listener emitters</summary>
        public bool AffectsGroupedEAX
        {
            get => EmitterBindings.GetAffectsGroupedEAX(native);
            set => EmitterBindings.SetAffectsGroupedEAX(native, value).ThrowIfError();
        }

        /// <summary>Whether this emitter is used as a reference point for calculating relative reverb gain and direction</summary>
        public bool HasRelativeReverb
        {
            get => EmitterBindings.GetHasRelativeReverb(native);
            set => EmitterBindings.SetHasRelativeReverb(native, value).ThrowIfError();
        }

        /// <summary>The lower bound of the relative reverb blend range. This affects the directional reverb that is heard by this emitter.</summary>
        public float RelativeReverbInnerThreshold
        {
            get => EmitterBindings.GetRelativeReverbInnerThreshold(native);
            set => EmitterBindings.SetRelativeReverbInnerThreshold(native, value).ThrowIfError();
        }

        /// <summary>The upper bound of the relative reverb blend range. This affects the directional reverb that is heard by this emitter.</summary>
        public float RelativeReverbOuterThreshold
        {
            get => EmitterBindings.GetRelativeReverbOuterThreshold(native);
            set => EmitterBindings.SetRelativeReverbOuterThreshold(native, value).ThrowIfError();
        }

        /// <summary>Whether to clamp this emitter's position to the world bounds, to prevent it from going out of bounds</summary>
        public bool ClampPosition
        {
            get => EmitterBindings.GetClampPosition(native);
            set => EmitterBindings.SetClampPosition(native, value).ThrowIfError();
        }

        /// <summary>User-defined name of this emitter</summary>
        public string Name
        {
            get => EmitterBindings.GetName(native);
            set => EmitterBindings.SetName(native, value).ThrowIfError();
        }

        /// <summary>User-defined type for this emitter</summary>
        public int Type
        {
            get => EmitterBindings.GetType(native);
            set => EmitterBindings.SetType(native, value).ThrowIfError();
        }

        public IntPtr UserData
        {
            get => EmitterBindings.GetUserData(native);
            set => EmitterBindings.SetUserData(native, value).ThrowIfError();
        }

        /// <summary>Number of reverb rays cast</summary>
        public int ReverbRayCount
        {
            get => EmitterBindings.GetReverbRayCount(native);
            set => EmitterBindings.SetReverbRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per reverb ray</summary>
        public int ReverbBounceCount
        {
            get => EmitterBindings.GetReverbBounceCount(native);
            set => EmitterBindings.SetReverbBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of occlusion rays cast</summary>
        public int OcclusionRayCount
        {
            get => EmitterBindings.GetOcclusionRayCount(native);
            set => EmitterBindings.SetOcclusionRayCount(native, value).ThrowIfError();
        }

        /// <summary>Maximum number of bounces per occlusion ray</summary>
        public int OcclusionBounceCount
        {
            get => EmitterBindings.GetOcclusionBounceCount(native);
            set => EmitterBindings.SetOcclusionBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of permeation rays cast</summary>
        public int PermeationRayCount
        {
            get => EmitterBindings.GetPermeationRayCount(native);
            set => EmitterBindings.SetPermeationRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per permeation ray</summary>
        public int PermeationBounceCount
        {
            get => EmitterBindings.GetPermeationBounceCount(native);
            set => EmitterBindings.SetPermeationBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of ambient occlusion rays cast</summary>
        public int AmbientOcclusionRayCount
        {
            get => EmitterBindings.GetAmbientOcclusionRayCount(native);
            set => EmitterBindings.SetAmbientOcclusionRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per ambient occlusion ray</summary>
        public int AmbientOcclusionBounceCount
        {
            get => EmitterBindings.GetAmbientOcclusionBounceCount(native);
            set => EmitterBindings.SetAmbientOcclusionBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of ambient permeation rays cast</summary>
        public int AmbientPermeationRayCount
        {
            get => EmitterBindings.GetAmbientPermeationRayCount(native);
            set => EmitterBindings.SetAmbientPermeationRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per ambient permeation ray</summary>
        public int AmbientPermeationBounceCount
        {
            get => EmitterBindings.GetAmbientPermeationBounceCount(native);
            set => EmitterBindings.SetAmbientPermeationBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of visualisation rays cast</summary>
        public int VisualisationRayCount
        {
            get => EmitterBindings.GetVisualisationRayCount(native);
            set => EmitterBindings.SetVisualisationRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per visualisation ray</summary>
        public int VisualisationBounceCount
        {
            get => EmitterBindings.GetVisualisationBounceCount(native);
            set => EmitterBindings.SetVisualisationBounceCount(native, value).ThrowIfError();
        }

        /// <summary>How often to cast visualisation rays (milliseconds)</summary>
        public int VisualisationUpdateFrequency
        {
            get => EmitterBindings.GetVisualisationUpdateFrequency(native);
            set => EmitterBindings.SetVisualisationUpdateFrequency(native, value).ThrowIfError();
        }

        /// <summary>How long (in milliseconds) the echogram records data for. Returning reverb rays after this period will be ignored</summary>
        public int MaxEchogramTime
        {
            get => EmitterBindings.GetMaxEchogramTime(native);
            set => EmitterBindings.SetMaxEchogramTime(native, value).ThrowIfError();
        }

        /// <summary>The length (in milliseconds) of each entry in the echogram</summary>
        public int EchogramGranularity
        {
            get => EmitterBindings.GetEchogramGranularity(native);
            set => EmitterBindings.SetEchogramGranularity(native, value).ThrowIfError();
        }

        /// <summary>The number of trails that are rebuilt from scratch each frame to prevent staleness when the listener moves. Clamped to minimum of 0.</summary>
        public int RefreshRayCount
        {
            get => EmitterBindings.GetRefreshRayCount(native);
            set => EmitterBindings.SetRefreshRayCount(native, value).ThrowIfError();
        }

        /// <summary>A ray trail will be re-created if an old ray bounce position is too far away from the new ray bounce position. This setting controls the allowed distance between old and new ray bounce positions. Defaults to 1.0f. Clamped to minimum of 0.</summary>
        public float RefreshDistanceThreshold
        {
            get => EmitterBindings.GetRefreshDistanceThreshold(native);
            set => EmitterBindings.SetRefreshDistanceThreshold(native, value).ThrowIfError();
        }

        /// <summary>The percentage of returning energy required for reverb to be at maximum volume. Defaults to 15%.</summary>
        public float ReverbEnergyCap
        {
            get => EmitterBindings.GetReverbEnergyCap(native);
            set => EmitterBindings.SetReverbEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>The percentage of occlusion energy required for this emitter to be at full volume. Defaults to 15% of the other emitter's OcclusionRayCount.</summary>
        public float OcclusionEnergyCap
        {
            get => EmitterBindings.GetOcclusionEnergyCap(native);
            set => EmitterBindings.SetOcclusionEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>The percentage of permeation energy required for this emitter to be at full volume. Defaults to 15% of the other emitter's PermeationRayCount * PermeationBounceCount.</summary>
        public float PermeationEnergyCap
        {
            get => EmitterBindings.GetPermeationEnergyCap(native);
            set => EmitterBindings.SetPermeationEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>The percentage of occlusion energy required for the emitter to be at full volume. Defaults to 15% of this emitter's AmbientOcclusionRayCount.</summary>
        public float AmbientOcclusionEnergyCap
        {
            get => EmitterBindings.GetAmbientOcclusionEnergyCap(native);
            set => EmitterBindings.SetAmbientOcclusionEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>The percentage of permeation energy required for the emitter to be at full volume. Defaults to 15% of this emitter's AmbientPermeationRayCount * AmbientPermeationBounceCount.</summary>
        public float AmbientPermeationEnergyCap
        {
            get => EmitterBindings.GetAmbientPermeationEnergyCap(native);
            set => EmitterBindings.SetAmbientPermeationEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>The loudest linear volume (0–1) this emitter's dry source will ever be played at by the consuming application. Used to estimate how long the emitter's reverb tail stays audible in GetEffectiveTailSeconds - a quieter source reaches an inaudible reverb tail sooner. Defaults to 1 (full volume)</summary>
        public float MaxVolume
        {
            get => EmitterBindings.GetMaxVolume(native);
            set => EmitterBindings.SetMaxVolume(native, value).ThrowIfError();
        }

        /// <summary>The threshold below which permeation rays are cancelled to prevent unnecessary traversal. Clamped to minimum of 0</summary>
        public float MinimumPermeationEnergy
        {
            get => EmitterBindings.GetMinimumPermeationEnergy(native);
            set => EmitterBindings.SetMinimumPermeationEnergy(native, value).ThrowIfError();
        }

        /// <summary>A seed used to randomise scattering vectors</summary>
        public int ScatteringSeed
        {
            get => EmitterBindings.GetScatteringSeed(native);
            set => EmitterBindings.SetScatteringSeed(native, value).ThrowIfError();
        }

        /// <summary>Whether to render each trail a different color (dev build only)</summary>
        public bool RandomTrailColor
        {
            get => EmitterBindings.GetRandomTrailColor(native);
            set => EmitterBindings.SetRandomTrailColor(native, value).ThrowIfError();
        }

        /// <summary>The color of ray trails in the debug window (dev build only)</summary>
        public Color TrailColor
        {
            get => EmitterBindings.GetTrailColor(native);
            set => EmitterBindings.SetTrailColor(native, value).ThrowIfError();
        }

        /// <summary>The color of reverb rays in the debug window (dev build only)</summary>
        public Color ReverbColor
        {
            get => EmitterBindings.GetReverbColor(native);
            set => EmitterBindings.SetReverbColor(native, value).ThrowIfError();
        }

        /// <summary>The color of occlusion rays in the debug window (dev build only)</summary>
        public Color OcclusionColor
        {
            get => EmitterBindings.GetOcclusionColor(native);
            set => EmitterBindings.SetOcclusionColor(native, value).ThrowIfError();
        }

        /// <summary>The color of permeation rays in the debug window (dev build only)</summary>
        public Color PermeationColor
        {
            get => EmitterBindings.GetPermeationColor(native);
            set => EmitterBindings.SetPermeationColor(native, value).ThrowIfError();
        }

        /// <summary>The color of ambient permeation rays in the debug window (dev build only)</summary>
        public Color AmbientPermeationColor
        {
            get => EmitterBindings.GetAmbientPermeationColor(native);
            set => EmitterBindings.SetAmbientPermeationColor(native, value).ThrowIfError();
        }
#endregion

#region ReadOnly
        /// <summary>EAX reverb properties for the listener. Contains parameters compatible with EAX reverb effects</summary>
        public EAXReverb EAX => new EAXReverb(EmitterBindings.GetEAX(native));
        /// <summary>Contains reverb data that has been transformed into more usable parameters</summary>
        public ProcessedReverb ProcessedReverb => new ProcessedReverb(EmitterBindings.GetProcessedReverb(native));
        /// <summary>This object contains the LF and HF gain for ambient sounds, to be applied to a low pass filter</summary>
        public LowPassFilter* AmbientFilter => EmitterBindings.GetAmbientFilter(native);

        /// <summary>Whether this emitter casts rays. False if all ray counts and/or bounce counts are set to 0.</summary>
        public bool CastsRays => EmitterBindings.CastsAnyRays(native);
        /// <summary>Emitters outside the world bounds will not be raytraced. Set ClampPosition to true to keep this emitter inside the world bounds</summary>
        public bool WithinWorldBounds => EmitterBindings.WithinWorldBounds(native);
        /// <summary>Read-only index indicating which GroupedEAX reverb effect should be used for this emitter</summary>
        public int GroupedEAXIndex => EmitterBindings.GetGroupedEAXIndex(native);
        /// <summary>The percentage of ambient occlusion rays that reached the edge of the world. Ranges from 0.0 to 1.0</summary>
        public float OutsidePercent => EmitterBindings.GetOutsidePercent(native);

        /// <summary>Number of trails this emitter will create</summary>
        public int TrailCount => EmitterBindings.GetTrailCount(native);
        /// <summary>Number of bounces per trail</summary>
        public int TrailBounceCount => EmitterBindings.GetTrailBounceCount(native);

        /// <summary>True if both ReverbRayCount and ReverbBounceCount are greater than zero</summary>
        public bool ReverbEnabled => EmitterBindings.ReverbEnabled(native);
        /// <summary>True if both OcclusionRayCount and OcclusionBounceCount are greater than zero</summary>
        public bool OcclusionEnabled => EmitterBindings.OcclusionEnabled(native);
        /// <summary>True if both PermeationRayCount and PermeationBounceCount are greater than zero</summary>
        public bool PermeationEnabled => EmitterBindings.PermeationEnabled(native);
        /// <summary>True if both AmbientOcclusionRayCount and AmbientOcclusionBounceCount are greater than zero</summary>
        public bool AmbientOcclusionEnabled => EmitterBindings.AmbientOcclusionEnabled(native);
        /// <summary>True if both AmbientPermeationRayCount and AmbientPermeationBounceCount are greater than zero</summary>
        public bool AmbientPermeationEnabled => EmitterBindings.AmbientPermeationEnabled(native);
        /// <summary>True if both VisualisationRayCount and VisualisationBounceCount are greater than zero</summary>
        public bool VisualisationEnabled => EmitterBindings.VisualisationEnabled(native);
#endregion

#region WriteOnly
        /// <summary>When defined, rays will be cast out of these positions rather than the default Position</summary>
        public Vector[] OverridePositions
        {
            set
            {
                fixed (Vector* ptr = value)
                {
                    EmitterBindings.SetOverridePositions(native, ptr, value.Length).ThrowIfError();
                }
            }
        }

        /// <summary>When defined, rays will be cast in these directions rather than the default spherical ray distribution</summary>
        public Vector[] OverrideRayDirections
        {
            set
            {
                fixed (Vector* ptr = value)
                {
                    EmitterBindings.SetOverrideRayDirections(native, ptr, value.Length).ThrowIfError();
                }
            }
        }
#endregion

    #region Callbacks
        private GCHandle _onRaytracingCompleteHandle;
        private GCHandle _onRaytracedByAnotherEmitterHandle;
        private GCHandle _onRemovedHandle;
        private GCHandle _visualisationCallbackHandle;
        private GCHandle _gainFormulaHandle;
        private GCHandle _ambientGainFormulaHandle;
        private GCHandle _logCallbackHandle;
        private GCHandle _logErrorCallbackHandle;
        
        /// <summary>A callback that is invoked after this emitter casts its rays for the first time.</summary>
        public Action OnRaytracingComplete
        {
            set
            {
                if (_onRaytracingCompleteHandle.IsAllocated)
                    _onRaytracingCompleteHandle.Free();

                if (value != null)
                {
                    OnRaytracingCompleteFn callback = (emitter) => value.Invoke();

                    _onRaytracingCompleteHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetOnRaytracingCompleteCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetOnRaytracingCompleteCallback(native, null).ThrowIfError();
                }
            }
        }

        /// <summary>A callback that is invoked when another emitter raytraces this emitter for the first time. The first argument is the other emitter that raytraced this emitter.</summary>
        public Action<Emitter> OnRaytracedByAnotherEmitter
        {
            set
            {
                if (_onRaytracedByAnotherEmitterHandle.IsAllocated)
                    _onRaytracedByAnotherEmitterHandle.Free();

                if (value != null)
                {
                    vaudionativewrapper.OnRaytracedByAnotherEmitterFn callback = (source, target) => value.Invoke(source == IntPtr.Zero ? null : new Emitter(source));
                    _onRaytracedByAnotherEmitterHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetOnRaytracedByAnotherEmitterCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetOnRaytracedByAnotherEmitterCallback(native, null).ThrowIfError();
                }
            }
        }

        /// <summary>A callback that is invoked when this emitter is actually removed from the World. If it casts reverb rays and affects grouped EAX, it won't be removed until its reverb tail finishes playing.</summary>
        public Action OnRemoved
        {
            set
            {
                if (_onRemovedHandle.IsAllocated)
                    _onRemovedHandle.Free();

                if (value != null)
                {
                    OnRemovedFn callback = () => value.Invoke();

                    _onRemovedHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetOnRemovedCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetOnRemovedCallback(native, null).ThrowIfError();
                }
            }
        }

        /// <summary>Callback that is invoked with VisualisationData for each bounce produced by an emitter's visualisation rays. Do not modify the array or access it outside this callback.</summary>
        public Action<VisualisationData[]> VisualisationCallback
        {
            set
            {
                if (_visualisationCallbackHandle.IsAllocated)
                    _visualisationCallbackHandle.Free();

                if (value != null)
                {
                    VisualisationCallbackFn callback = (emitter, dataPtr, count) =>
                    {
                        var arr = new VisualisationData[count];

                        for (int i = 0; i < count; i++)
                            arr[i] = new VisualisationData { position = dataPtr[i].position, normal = dataPtr[i].normal };

                        value.Invoke(arr);
                    };

                    _visualisationCallbackHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetVisualisationCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetVisualisationCallback(native, null).ThrowIfError();
                }
            }
        }

        public Func<bool, int, int, int, float, float, float> GainFormula
        {
            set
            {
                if (_gainFormulaHandle.IsAllocated)
                    _gainFormulaHandle.Free();

                if (value != null)
                {
                    GainFormulaDelegate callback = (lf, ocRay, permRay, permBounce, ocEnergy, permEnergy) =>
                        value(lf, ocRay, permRay, permBounce, ocEnergy, permEnergy);
                    _gainFormulaHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetGainFormula(native, Marshal.GetFunctionPointerForDelegate(callback)).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetGainFormula(native, IntPtr.Zero).ThrowIfError();
                }
            }
        }

        public Func<bool, int, int, int, float, float, float> AmbientGainFormula
        {
            set
            {
                if (_ambientGainFormulaHandle.IsAllocated)
                    _ambientGainFormulaHandle.Free();

                if (value != null)
                {
                    GainFormulaDelegate callback = (lf, ocRay, permRay, permBounce, ocEnergy, permEnergy) =>
                        value(lf, ocRay, permRay, permBounce, ocEnergy, permEnergy);
                    _ambientGainFormulaHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetAmbientGainFormula(native, Marshal.GetFunctionPointerForDelegate(callback)).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetAmbientGainFormula(native, IntPtr.Zero).ThrowIfError();
                }
            }
        }

        /// <summary>A custom logging callback. Defaults to WriteLine()</summary>
        public Action<string> LogCallback
        {
            set
            {
                if (_logCallbackHandle.IsAllocated)
                    _logCallbackHandle.Free();

                if (value != null)
                {
                    LogCallbackFn callback = (msg) => value(msg);
                    _logCallbackHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetLogCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetLogCallback(native, null).ThrowIfError();
                }
            }
        }

        /// <summary>A custom logging callback. Defaults to Error.WriteLine()</summary>
        public Action<string> LogErrorCallback
        {
            set
            {
                if (_logErrorCallbackHandle.IsAllocated)
                    _logErrorCallbackHandle.Free();

                if (value != null)
                {
                    LogCallbackFn callback = (msg) => value(msg);
                    _logErrorCallbackHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetLogErrorCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetLogErrorCallback(native, null).ThrowIfError();
                }
            }
        }
#endregion
    }
}