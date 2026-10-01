namespace vaudionativewrapper.managed
{
    /// <summary>Converts a voxel in a UnsafeVoxelPrimitive to a MaterialType. Create a class that inherits from this class, then assign it to UnsafeVoxelMaterialMap before adding an UnsafeVoxelPrimitive to the world. GetMaterial is called from background threads and must be thread-safe. If the mapping changes, assign it to UnsafeVoxelMaterialMap again.</summary>
    public abstract unsafe class UnsafeVoxelMaterialMap
    {
        /// <summary>Converts a voxel to a MaterialType. Return Air to treat the voxel as empty.</summary>
        public abstract MaterialType GetMaterial(void* voxel);

        /// <summary>Returns true if a voxel is solid (not air).</summary>
        public abstract bool IsSolid(void* voxel);
    }
}
