namespace vaudionativewrapper.managed
{
    /// <summary>
    /// Converts the voxels in a <see cref="UnsafeVoxelPrimitive"/> to a <see cref="MaterialType"/>. Inherit from this class and assign it to <see cref="World.UnsafeVoxelMaterialMap"/> before adding a <see cref="UnsafeVoxelPrimitive"/> to the world.<br/>
    /// This class is used by background threads and must be thread-safe.<br/>
    /// If the mapping changes, assign the map to <see cref="World.UnsafeVoxelMaterialMap"/> again so the changes are sent to the raytracing threads.
    /// </summary>
    public abstract unsafe class UnsafeVoxelMaterialMap
    {
        /// <summary>Convert a voxel pointer to a material. Return <see cref="MaterialType.Air"/> if the voxel is empty. Any other material must exist in the world</summary>
        public abstract MaterialType GetMaterial(void* voxel);

        /// <summary>Returns true if a voxel is solid (not air)</summary>
        public abstract bool IsSolid(void* voxel);
    }
}
