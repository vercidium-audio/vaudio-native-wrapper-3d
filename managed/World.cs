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

        /// <summary>The vertical field of view (in radians) of the camera in the debug window (dev build only)</summary>
        public float FieldOfView
        {
            get => WorldBindings.GetFieldOfView(native);
            set => WorldBindings.SetFieldOfView(native, value).ThrowIfError();
        }

        /// <summary>Converts a world-space direction (e.g. <see cref="EAXReverb.GetRelativeDirection"/>) to a listener-space direction, for use as EFX reflections/late reverb pan. Same as <see cref="ConvertWorldToListenerDirection"/></summary>
        public Vector CalculateListenerRelativePan(Vector worldVector, float listenerPitch, float listenerYaw)
        {
            return WorldBindings.CalculateListenerRelativePan(native, worldVector, listenerPitch, listenerYaw);
        }

        /// <summary>
        /// Converts a world-space direction (in the world's <see cref="CoordinateSystem"/>) to a listener-space direction, based on the listener's pitch and yaw (in the world's <see cref="CoordinateSystem"/>).<br/>
        /// Listener space is always X+ right, Y+ up and Z+ forward relative to the listener, regardless of <see cref="CoordinateSystem"/>, matching the EFX reflections/late reverb pan convention.<br/>
        /// Inverse of <see cref="ConvertListenerToWorldDirection"/>.
        /// </summary>
        public Vector ConvertWorldToListenerDirection(Vector worldDirection, float listenerPitch, float listenerYaw)
        {
            return WorldBindings.ConvertWorldToListenerDirection(native, worldDirection, listenerPitch, listenerYaw);
        }

        /// <summary>
        /// Converts a listener-space direction (X+ right, Y+ up, Z+ forward relative to the listener) to a world-space direction (in the world's <see cref="CoordinateSystem"/>), based on the listener's pitch and yaw (in the world's <see cref="CoordinateSystem"/>).<br/>
        /// Inverse of <see cref="ConvertWorldToListenerDirection"/>.
        /// </summary>
        public Vector ConvertListenerToWorldDirection(Vector listenerDirection, float listenerPitch, float listenerYaw)
        {
            return WorldBindings.ConvertListenerToWorldDirection(native, listenerDirection, listenerPitch, listenerYaw);
        }

    }
}
