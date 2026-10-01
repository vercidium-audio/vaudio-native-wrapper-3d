using System;
using System.Collections.Generic;

namespace vaudionativewrapper.managed
{
    /// <summary>A triangulated mesh that is built once and can be shared across multiple MeshPrimitive instances</summary>
    public unsafe class Mesh
    {
        public IntPtr native;

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

        /// <summary>Destroys the native mesh. If a world's background threads may still be using it, it is freed automatically once they have finished. Returns <see cref="VAResult.ErrorInUse"/> if a MeshPrimitive using this mesh has not been destroyed yet, in which case Destroy can be called again later</summary>
        public VAResult Destroy()
        {
            if (native == IntPtr.Zero)
                return VAResult.Success;

            var result = MeshBindings.Destroy(native);

            if (result == VAResult.Success)
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
