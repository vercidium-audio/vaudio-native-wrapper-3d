using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A 3D primitive that rays collide with.</summary>
    public class Primitive
    {
        public IntPtr native;
        protected bool owns;

        public Primitive(IntPtr native)
        {
            this.native = native;
        }

        public Primitive() { }

        /// <summary>Unique properties logged in the ~Primitive finaliser to identify which primitive was leaked</summary>
        protected virtual string DebugInfo => "";

        ~Primitive()
        {
            if (owns && native != IntPtr.Zero)
                LogSettings.Warn($"{GetType().Name} was garbage collected without calling Destroy() first. {DebugInfo}");
        }
    }
}
