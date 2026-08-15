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

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => MeshPrimitiveBindings.GetMaterial(native);
            set => MeshPrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Can contain scale, rotation and translation components</summary>
        public Matrix transform
        {
            get => *MeshPrimitiveBindings.GetTransform(native);
            set => MeshPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        /// <summary>If this is a watertight mesh, permeation rays will lose energy based on how far the ray travels through this mesh. If not, set this field to false and permeation rays will lose a flat percentage of energy when they touch this mesh (controlled by PlaneTransmissionLF and PlaneTransmissionHF).</summary>
        public bool Supports3DPermeation
        {
            get => MeshPrimitiveBindings.GetSupports3DPermeation(native);
            set => MeshPrimitiveBindings.SetSupports3DPermeation(native, value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => MeshPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, Supports3DPermeation={Supports3DPermeation}";
    }
}
