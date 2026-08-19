using System;
using System.Collections.Generic;

namespace vaudionativewrapper.managed
{
    /// <summary>An audio primitive defined by an arbitrary triangle mesh</summary>
    public unsafe class MeshPrimitive : Primitive
    {
        /// <summary>Create a mesh primitive from a list of vertices</summary>
        public MeshPrimitive(MaterialType material, List<Vector> vertices, Vector minBounds, Vector maxBounds, Matrix transform)
        {
            Vector[] copy = vertices.ToArray();
            IntPtr outPrimitive;

            fixed (Vector* ptr = copy)
            {
                MeshPrimitiveBindings.Create(material, ptr, copy.Length, minBounds, maxBounds, ref transform, &outPrimitive).ThrowIfError();
            }

            native = outPrimitive;
            owns = true;
        }

        /// <summary>Create a mesh primitive from an array of vertices</summary>
        public MeshPrimitive(MaterialType material, Vector[] vertices, Vector minBounds, Vector maxBounds, Matrix transform)
        {
            IntPtr outPrimitive;

            fixed (Vector* ptr = vertices)
            {
                MeshPrimitiveBindings.Create(material, ptr, vertices.Length, minBounds, maxBounds, ref transform, &outPrimitive).ThrowIfError();
            }

            native = outPrimitive;
            owns = true;
        }

        /// <summary>Create a mesh primitive that shares geometry with a Mesh. The BVH is built once in the Mesh and reused by every instance.</summary>
        public MeshPrimitive(MaterialType material, Mesh mesh, Matrix transform)
        {
            IntPtr outPrimitive;
            MeshPrimitiveBindings.CreatePrimitiveFromMesh(material, mesh.native, ref transform, &outPrimitive).ThrowIfError();

            native = outPrimitive;
            owns = true;
        }

        /// <summary>Can contain scale, rotation and translation components</summary>
        public Matrix transform
        {
            get => *MeshPrimitiveBindings.GetTransform(native);
            set => MeshPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        /// <summary>Whether rays lose a flat percentage of energy (FlatTransmissionLF and FlatTransmissionHF) the moment they touch this primitive, instead of calculating how long the ray spent inside it. Defaults to false (depth-based transmission using TransmissionLF and TransmissionHF). Zero-thickness primitives (e.g. DiskPrimitive, PlanePrimitive, TrianglePrimitive, LinePrimitive) have no meaningful interior to travel through, so they force this to true and throw if set to false.</summary>
        public bool UseFlatTransmission
        {
            get => MeshPrimitiveBindings.GetUseFlatTransmission(native);
            set => MeshPrimitiveBindings.SetUseFlatTransmission(native, value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => MeshPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, UseFlatTransmission={UseFlatTransmission}";
    }
}
