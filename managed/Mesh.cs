using System;
using System.Collections.Generic;

namespace vaudionativewrapper.managed
{
    /// <summary>A triangulated mesh that is built once and can be shared across multiple MeshPrimitive instances</summary>
    public unsafe class Mesh
    {
        internal IntPtr native;

        /// <summary>Create a mesh from an array of vertices</summary>
        public Mesh(Vector[] vertices, Vector minBounds, Vector maxBounds)
        {
            IntPtr outMesh;

            fixed (Vector* ptr = vertices)
                MeshBindings.Create(ptr, vertices.Length, minBounds, maxBounds, &outMesh).ThrowIfError();

            native = outMesh;
        }

        /// <summary>Create a mesh from a list of vertices</summary>
        public Mesh(List<Vector> vertices, Vector minBounds, Vector maxBounds)
        {
            Vector[] copy = vertices.ToArray();
            IntPtr outMesh;

            fixed (Vector* ptr = copy)
                MeshBindings.Create(ptr, copy.Length, minBounds, maxBounds, &outMesh).ThrowIfError();

            native = outMesh;
        }

        public VAResult Destroy()
        {
            var result = MeshBindings.Destroy(native);
            native = IntPtr.Zero;
            return result;
        }

        ~Mesh()
        {
            if (native != IntPtr.Zero)
                LogSettings.Warn("Mesh was garbage collected without calling Destroy() first.");
        }
    }
}
