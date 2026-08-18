namespace vaudionativewrapper.managed
{
    /// <summary>A standalone world with its own primitives, emitters, materials and settings. Manages its own raytracing and multithreading</summary>
    public partial class World
    {
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

        /// <summary>Helper function that converts a world-space direction to a listener-space direction</summary>
        public Vector CalculateListenerRelativePan(Vector worldVector, float listenerPitch, float listenerYaw)
        {
            return WorldBindings.CalculateListenerRelativePan(native, worldVector, listenerPitch, listenerYaw);
        }

    }
}
