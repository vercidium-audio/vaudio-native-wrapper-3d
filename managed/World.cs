using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    /// <summary>A standalone world with its own primitives, emitters, materials and settings</summary>
    public unsafe class World
    {
        public IntPtr native;
        private readonly bool owns;

        /// <summary>Create a new world</summary>
        public World()
        {
            native = WorldBindings.Create();
            owns = true;
        }

        public World(IntPtr native)
        {
            this.native = native;
        }

        /// <summary>Waits for background thread to complete, then disposes everything. After calling this method, this world cannot be reused.</summary>
        public void Dispose()
        {
            if (native == IntPtr.Zero)
                return;

            WorldBindings.Destroy(native).ThrowIfError();
            native = IntPtr.Zero;
        }

        ~World()
        {
            if (owns && native != IntPtr.Zero)
                LogSettings.Warn("World was garbage collected without calling Dispose() first.");
        }

        /// <summary>Updates the raytracing simulation. Call this method regularly to process raytracing results and submit new work. This method does nothing if background raytracing threads are still running. When threads are idle, it performs the following operations: - Handles the last raytracing results, updating reverb objects and invoking OnRaytracedByAnotherEmitter callbacks - Applies new settings and resizes memory buffers if needed (e.g. if ray counts were changed) - Processes new, modified, and removed primitives - Starts raytracing again on background threads This method must be called from the main thread. Calling this more frequently is safe and can reduce latency for emitter updates.</summary>
        public VAResult Update()
        {
            var result = WorldBindings.Update(native);
            result.ThrowIfError();
            return result;
        }

        /// <summary>Blocks the calling thread until all background raytracing threads complete, then handles the results (updates reverb objects and invokes OnRaytracingComplete and OnRaytracedByAnotherEmitter callbacks for each emitter).</summary>
        public VAResult Wait()
        {
            var result = WorldBindings.Wait(native);
            result.ThrowIfError();
            return result;
        }

        /// <summary>Exports all world settings, materials, primitives, and emitters to a binary file (dev build only).</summary>
        public void Export(string fileName)
        {
            WorldBindings.Export(native, fileName).ThrowIfError();
        }

        /// <summary>Imports all world settings, materials, primitives, and emitters from a binary file (dev build only)</summary>
        public void Import(string fileName, out Emitter[] emitters)
        {
            IntPtr* emittersPtr = null;
            int emitterCount = 0;

            WorldBindings.Import(native, fileName, &emittersPtr, &emitterCount).ThrowIfError();

            emitters = new Emitter[emitterCount];
            for (int i = 0; i < emitterCount; i++)
                emitters[i] = new Emitter(emittersPtr[i]);

            WorldBindings.ImportFreeEmitters(emittersPtr).ThrowIfError();
        }

        /// <summary>Create a material with default settings.</summary>
        public MaterialProperties CreateMaterial(MaterialType type)
        {
            var id = (int)type;

            WorldBindings.CreateMaterial(native, id).ThrowIfError();

            return new MaterialProperties(native, id);
        }

        /// <summary>Gets the debug rendering colour for a specific material type (dev build only). No effect on raytracing.</summary>
        public Color GetMaterialColor(int materialId) => WorldBindings.GetMaterialColor(native, materialId);

        /// <summary>Sets the debug rendering colour for a specific material type (dev build only). No effect on raytracing.</summary>
        public void SetMaterialColor(int materialId, Color color) => WorldBindings.SetMaterialColor(native, materialId, color).ThrowIfError();

        /// <summary>Adds a 3D object to the raytracing scene. This method is thread-safe and will not affect the current raytracing threads. Primitives completely outside the world bounds will be ignored during raytracing.</summary>
        public void AddPrimitive(Primitive primitive)
        {
            WorldBindings.AddPrimitive(native, primitive.native).ThrowIfError();
        }

        /// <summary>Removes a 3D object from the raytracing scene. This method is thread-safe and will not affect the current raytracing threads.</summary>
        public void RemovePrimitive(Primitive primitive)
        {
            WorldBindings.RemovePrimitive(native, primitive.native).ThrowIfError();
        }

        /// <summary>Add an Emitter to the world. This method is thread-safe and will not affect the current raytracing threads.</summary>
        public void AddEmitter(Emitter emitter)
        {
            WorldBindings.AddEmitter(native, emitter.native).ThrowIfError();
        }

        /// <summary>Checks if the world has an emitter. Returns VA_INVALID_POINTER if emitter is NULL, VA_PENDING_REMOVAL if the emitter will be removed once its reverb tail has finished playing, VA_TRUE if it has the emitter, VA_FALSE if it does not have the emitter</summary>
        public VAResult HasEmitter(Emitter emitter)
        {
            return WorldBindings.HasEmitter(native, emitter.native);
        }

        /// <summary>Remove an Emitter from the world. This method is thread-safe and will not affect the current raytracing threads. This emitter's OnRaytracingComplete callback will not be invoked.</summary>
        public void RemoveEmitter(Emitter emitter)
        {
            WorldBindings.RemoveEmitter(native, emitter.native).ThrowIfError();
        }

        public int GetEmitterCount()
        {
            return WorldBindings.GetEmitterCount(native);
        }

        /// <summary>The size of the world. Emitters outside the world will not be raytraced, and Primitives that are fully outside these bounds will be ignored</summary>
        public Vector Size
        {
            get => WorldBindings.GetSize(native);
            set => WorldBindings.SetSize(native, value).ThrowIfError();
        }

        /// <summary>The minimum bounds of the world. Emitters outside the world will not be raytraced, and Primitives that are fully outside these bounds will not affect raytracing.</summary>
        public Vector Position
        {
            get => WorldBindings.GetPosition(native);
            set => WorldBindings.SetPosition(native, value).ThrowIfError();
        }

        /// <summary>The maximum bounds of the world (Position + Size)</summary>
        public Vector MaxBounds
        {
            get => WorldBindings.GetMaximumBounds(native);
            set => WorldBindings.SetMaximumBounds(native, value).ThrowIfError();
        }

        public void UpdateWorldPosition(Vector position)
        {
            WorldBindings.SetPosition(native, position).ThrowIfError();
        }

        public void UpdateWorldSize(Vector size)
        {
            WorldBindings.SetSize(native, size).ThrowIfError();
        }

        public void UpdateWorldBounds(Vector position, Vector size)
        {
            UpdateWorldPosition(position);
            UpdateWorldSize(size);
        }

        /// <summary>The average time (in milliseconds) spent by Update on the main thread. This includes time spent handling previous raytracing results, applying new settings, processing primitive updates and submitting work to background threads. Use this metric to monitor main thread performance impact.</summary>
        public double MainThreadTime => WorldBindings.GetMainThreadTime(native);
        
        /// <summary>The average time (in milliseconds) spent in the preparation thread before the raytracing threads begin. This phase updates the BVH (Bounding Volume Hierarchy) acceleration structure with new, modified, and removed primitives. Higher values are a result of complex scene changes.</summary>
        public double PreparationTime => WorldBindings.GetPreparationTime(native);
        
        /// <summary>The average time (in milliseconds) spent in the raytracing threads. This is the real-world elapsed time it takes for raytracing to complete, not the sum of all thread times. This value is automatically adjusted based on how many threads perform raytracing. Use this to monitor raytracing performance and adjust ray counts if needed.</summary>
        public double RaytracingTime => WorldBindings.GetRaytracingTime(native);
        
        /// <summary>The average time (in milliseconds) spent in the analysing thread after the raytracing threads complete. This phase runs after raytracing completes and calculate reverb properties. Use this to monitor raytracing performance and adjust ray counts if needed.</summary>
        public double AnalysisTime => WorldBindings.GetAnalysisTime(native);

        /// <summary>List of grouped EAX reverb properties for all emitters. Contains parameters compatible with EAX reverb effects.</summary>
        public List<EAXReverb> GroupedEAX
        {
            get
            {
                List<EAXReverb> results = new List<EAXReverb>();

                var listPtr = WorldBindings.GetGroupedEAX(native);
                var count = WorldBindings.GetCurrentGroupedEAXCount(native);

                for (int i = 0; i < count; i++)
                    results.Add(new EAXReverb(listPtr[i]));

                return results;
            }
        }

        /// <summary>Whether Emitters outside the world have 0 occlusion/permeation energy (true) or maximum energy (false).</summary>
        public bool EmittersOutsideTheWorldAreMuffled
        {
            get => WorldBindings.GetEmittersOutsideTheWorldAreMuffled(native);
            set => WorldBindings.SetEmittersOutsideTheWorldAreMuffled(native, value).ThrowIfError();
        }

        /// <summary>Whether the entire world is considered indoors or outdoors. When false, reverb rays stop accumulating energy after hitting the world edge. Defaults to false.</summary>
        public bool WorldIsIndoors
        {
            get => WorldBindings.GetWorldIsIndoors(native);
            set => WorldBindings.SetWorldIsIndoors(native, value).ThrowIfError();
        }

        /// <summary>True until raytracing has run at least once</summary>
        public bool Initialising => WorldBindings.GetInitialising(native);

        /// <summary>The number of rays cast this frame.</summary>
        public int RaysCastThisFrame => WorldBindings.GetRaysCastThisFrame(native);

        /// <summary>The maximum number of grouped EAX reverb properties shared across all emitters that affect grouped EAX. Higher values increase accuracy but are more expensive to run.</summary>
        public int MaximumGroupedEAXCount
        {
            get => WorldBindings.GetMaximumGroupedEAXCount(native);
            set => WorldBindings.SetMaximumGroupedEAXCount(native, value).ThrowIfError();
        }

        /// <summary>The number of work items to split trails across for load balancing. A higher value helps evenly distribute work across all threads.</summary>
        public int WorkItemCount
        {
            get => WorldBindings.GetWorkItemCount(native);
            set => WorldBindings.SetWorkItemCount(native, value).ThrowIfError();
        }

        /// <summary>The maximum amount of threads that can run in parallel. On WASM all worlds share a single thread pool, so this is a global setting via MaximumConcurrencyLevel. The maximum amount of threads that can run in parallel for this world</summary>
        public int MaximumConcurrencyLevel
        {
            get => WorldBindings.GetMaximumConcurrencyLevel(native);
            set => WorldBindings.SetMaximumConcurrencyLevel(native, value).ThrowIfError();
        }

        /// <summary>Get meters per world unit. Affects air absorption and reverb calculation.</summary>
        public float MetersPerUnit
        {
            get => WorldBindings.GetMetersPerUnit(native);
            set => WorldBindings.SetMetersPerUnit(native, value).ThrowIfError();
        }

        /// <summary>Inverse speed of sound in seconds per meter. Defaults to 1.0f / 343.0f. Affects reverb calculation</summary>
        public float InverseSpeedOfSound
        {
            get => WorldBindings.GetInverseSpeedOfSound(native);
            set => WorldBindings.SetInverseSpeedOfSound(native, value).ThrowIfError();
        }

        /// <summary>Low-frequency reference (Hz) for air absorption, reverb, and material scattering</summary>
        public float ReferenceFrequencyLF
        {
            get => WorldBindings.GetReferenceFrequencyLF(native);
            set => WorldBindings.SetReferenceFrequencyLF(native, value).ThrowIfError();
        }

        /// <summary>High-frequency reference (Hz) for air absorption, reverb, and material scattering</summary>
        public float ReferenceFrequencyHF
        {
            get => WorldBindings.GetReferenceFrequencyHF(native);
            set => WorldBindings.SetReferenceFrequencyHF(native, value).ThrowIfError();
        }

        /// <summary>The epsilon value used for raytracing and primitive intersections. Defaults to 0.01f</summary>
        public float Epsilon
        {
            get => WorldBindings.GetEpsilon(native);
            set => WorldBindings.SetEpsilon(native, value).ThrowIfError();
        }

        /// <summary>The average time (in milliseconds) between when Update is invoked, and when OnReverbUpdated is invoked.</summary>
        public double Latency => WorldBindings.GetLatency(native);

        public IntPtr UserData
        {
            get => WorldBindings.GetUserData(native);
            set => WorldBindings.SetUserData(native, value).ThrowIfError();
        }

        /// <summary>Air absorption settings. Set to null to disable air absorption.</summary>
        public AirAbsorptionSettings AirAbsorption
        {
            get => new AirAbsorptionSettings(WorldBindings.GetAirAbsorption(native));
            set => WorldBindings.SetAirAbsorption(native, value.native).ThrowIfError();
        }

        private GCHandle[] _customEAXFormulaHandles;

        public CustomEAXFormulaCallbacks UpdateCustomEAXFormulas(managed.CustomEAXFormulas formulas)
        {
            var callbacks = CustomEAXFormulaHelper.CreateCustomEAXFormulaCallbacks(formulas);

            var nativeFormulas = CustomEAXFormulasBindings.Create();

            nativeFormulas->initialise = Marshal.GetFunctionPointerForDelegate(callbacks.Initialise);
            nativeFormulas->calculateDiffusion = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateDiffusion);
            nativeFormulas->calculateDensity = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateDensity);
            nativeFormulas->calculateReflectionsDelay = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateReflectionsDelay);
            nativeFormulas->calculateLateReverbDelay = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateLateReverbDelay);
            nativeFormulas->calculateFrequencyGains = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateFrequencyGains);
            nativeFormulas->calculateReflectionsAndLateReverbGain = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateReflectionsAndLateReverbGain);
            nativeFormulas->calculateRT60 = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateRT60);

            WorldBindings.SetCustomEAXFormulas(native, nativeFormulas).ThrowIfError();

            // Native holds raw function pointers into these 8 delegates, invoked from native worker
            // threads. A managed reference via the returned CustomEAXFormulaCallbacks isn't a reliable
            // guarantee against collection for that pattern (see AirAbsorptionSettings), so pin each
            // delegate explicitly for as long as native might call back into it.
            if (_customEAXFormulaHandles != null)
                foreach (var handle in _customEAXFormulaHandles)
                    if (handle.IsAllocated)
                        handle.Free();

            _customEAXFormulaHandles = new[]
            {
                GCHandle.Alloc(callbacks.Initialise),
                GCHandle.Alloc(callbacks.CalculateDiffusion),
                GCHandle.Alloc(callbacks.CalculateDensity),
                GCHandle.Alloc(callbacks.CalculateReflectionsDelay),
                GCHandle.Alloc(callbacks.CalculateLateReverbDelay),
                GCHandle.Alloc(callbacks.CalculateFrequencyGains),
                GCHandle.Alloc(callbacks.CalculateReflectionsAndLateReverbGain),
                GCHandle.Alloc(callbacks.CalculateRT60),
            };

            return callbacks;
        }

        /// <summary>Get properties for a specific material.</summary>
        public MaterialProperties GetMaterial(MaterialType type)
        {
            return new MaterialProperties(native, (int)type);
        }

        private GCHandle _onReverbUpdatedHandle;
        private GCHandle _logCallbackHandle;

        /// <summary>This callback is invoked after EAX reverb results are updated. This gives you a chance to update your EAX effects, so they can be applied to an emitter in it's OnRaytracingComplete callback. After this, each emitter's callback are invoked, and then OnRaytracingResultsHandled will be invoked next.</summary>
        public Action OnReverbUpdated
        {
            set
            {
                if (_onReverbUpdatedHandle.IsAllocated)
                    _onReverbUpdatedHandle.Free();

                if (value != null)
                {
                    OnReverbUpdatedFn fn = () => value();
                    _onReverbUpdatedHandle = GCHandle.Alloc(fn);

                    WorldBindings.SetOnReverbUpdated(native, Marshal.GetFunctionPointerForDelegate(fn)).ThrowIfError();
                }
                else
                {
                    WorldBindings.SetOnReverbUpdated(native, IntPtr.Zero).ThrowIfError();
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
                    LogCallbackFn fn = (msg) => value(msg);
                    _logCallbackHandle = GCHandle.Alloc(fn);

                    WorldBindings.SetLogCallback(native, Marshal.GetFunctionPointerForDelegate(fn)).ThrowIfError();
                }
                else
                {
                    WorldBindings.SetLogCallback(native, IntPtr.Zero).ThrowIfError();
                }
            }
        }

        /// <summary>Whether to log memory allocation warnings</summary>
        public bool LogMemoryAllocationWarnings
        {
            get => WorldBindings.GetLogMemoryAllocationWarnings(native);
            set => WorldBindings.SetLogMemoryAllocationWarnings(native, value).ThrowIfError();
        }

        /// <summary>Coordinate system used in the debug window and when calculating listener-relative reverb directionality</summary>
        public CoordinateSystem CoordinateSystem
        {
            get => WorldBindings.GetCoordinateSystem(native);
            set => WorldBindings.SetCoordinateSystem(native, value).ThrowIfError();
        }

        public Vector CalculateListenerRelativePan(Vector worldVector, float listenerPitch, float listenerYaw)
        {
            return WorldBindings.CalculateListenerRelativePan(native, worldVector, listenerPitch, listenerYaw);
        }

        /// <summary>When set to true, Update will stop submitting work to background threads. When ThreadsRunning becomes false, it is safe to call Dispose.</summary>
        public bool PendingShutdown
        {
            get => WorldBindings.GetPendingShutdown(native);
            set => WorldBindings.SetPendingShutdown(native, value).ThrowIfError();
        }

        public bool ThreadsRunning => WorldBindings.GetThreadsRunning(native);

#region Rendering

        /// <summary>Whether to render the raytracing scene in a separate window (dev build only)</summary>
        public bool RenderingEnabled
        {
            get => WorldBindings.GetRenderingEnabled(native);
            set => WorldBindings.SetRenderingEnabled(native, value).ThrowIfError();
        }

        /// <summary>The position of the camera in the debug window (dev build only)</summary>
        public Vector CameraPosition
        {
            get => WorldBindings.GetCameraPosition(native);
            set => WorldBindings.SetCameraPosition(native, value).ThrowIfError();
        }

        /// <summary>The pitch of the camera in the debug window (dev build only)</summary>
        public float CameraPitch
        {
            get => WorldBindings.GetCameraPitch(native);
            set => WorldBindings.SetCameraPitch(native, value).ThrowIfError();
        }

        /// <summary>The yaw of the camera in the debug window (dev build only)</summary>
        public float CameraYaw
        {
            get => WorldBindings.GetCameraYaw(native);
            set => WorldBindings.SetCameraYaw(native, value).ThrowIfError();
        }

        /// <summary>The field of view (in radians) of the camera in the debug window (dev build only)</summary>
        public float FieldOfView
        {
            get => WorldBindings.GetFieldOfView(native);
            set => WorldBindings.SetFieldOfView(native, value).ThrowIfError();
        }

        /// <summary>The speed of the camera in the debug window (dev build only)</summary>
        public float CameraSpeed
        {
            get => WorldBindings.GetCameraSpeed(native);
            set => WorldBindings.SetCameraSpeed(native, value).ThrowIfError();
        }

        /// <summary>The time spent (in milliseconds) rendering the debug window (dev build only)</summary>
        public double RenderTime
        {
            get => WorldBindings.GetRenderTime(native);
        }

        /// <summary>Whether the debug window can independently move its camera around (dev build only)</summary>
        public bool ManualCamera
        {
            get => WorldBindings.GetManualCamera(native);
            set => WorldBindings.SetManualCamera(native, value).ThrowIfError();
        }

        /// <summary>Whether to render rays in the debug window (dev build only)</summary>
        public bool ShouldRenderRays
        {
            get => WorldBindings.GetShouldRenderRays(native);
            set => WorldBindings.SetShouldRenderRays(native, value).ThrowIfError();
        }

        /// <summary>Whether to render primitives in the debug window (dev build only)</summary>
        public bool ShouldRenderPrimitives
        {
            get => WorldBindings.GetShouldRenderPrimitives(native);
            set => WorldBindings.SetShouldRenderPrimitives(native, value).ThrowIfError();
        }

        /// <summary>The position of the debug window (dev build only)</summary>
        public (int x, int y) WindowPosition
        {
            get
            {
                WorldBindings.GetWindowPosition(native, out int x, out int y);
                return (x, y);
            }
            set
            {
                WorldBindings.SetWindowPosition(native, value.x, value.y).ThrowIfError();
            }
        }

        /// <summary>The size of the debug window (dev build only)</summary>
        public (int x, int y) WindowSize
        {
            get
            {
                WorldBindings.GetWindowSize(native, out int x, out int y);
                return (x, y);
            }
            set
            {
                WorldBindings.SetWindowSize(native, value.x, value.y).ThrowIfError();
            }
        }

        #endregion
    }
}