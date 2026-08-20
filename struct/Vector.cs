using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    /// <summary>A 3D vector with single-precision floating-point components</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector
    {
        /// <summary>Creates a vector with all components set to <paramref name="v"/></summary>
        public Vector(float v)
        {
            X = v;
            Y = v;
            Z = v;
        }

        /// <summary>Creates a vector with the specified components</summary>
        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>The X component</summary>
        public float X;

        /// <summary>The Y component</summary>
        public float Y;
    
        /// <summary>The Z component</summary>
        public float Z;

        /// <inheritdoc/>
        public override string ToString() => $"({X:F3}, {Y:F4}, {Z:F4})";
    }
}
