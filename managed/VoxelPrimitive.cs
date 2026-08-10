namespace vaudionativewrapper.managed
{
    /// <summary>A 3D grid of voxels</summary>
    public unsafe class VoxelPrimitive : Primitive
    {
        public readonly int width;
        public readonly int height;
        public readonly int depth;

        /// <summary>Create a new voxel primitive with the specified grid size</summary>
        public VoxelPrimitive(int width, int height, int depth)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;

            native = VoxelPrimitiveBindings.Create(width, height, depth);
            owns = true;
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => VoxelPrimitiveBindings.GetMaterial(native);
            set => VoxelPrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Scale of the voxels</summary>
        public float scale
        {
            get => VoxelPrimitiveBindings.GetScale(native);
            set => VoxelPrimitiveBindings.SetScale(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *VoxelPrimitiveBindings.GetTransform(native);
            set => VoxelPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        /// <summary>Helper overload for accessing and editing voxel data</summary>
        public MaterialType this[int x, int y, int z]
        {
            get => VoxelPrimitiveBindings.GetVoxel(native, x, y, z);
            set => VoxelPrimitiveBindings.SetVoxel(native, x, y, z, value).ThrowIfError();
        }

        /// <summary>Call this after editing voxel data</summary>
        public void SetDataDirty() => VoxelPrimitiveBindings.SetDataDirty(native).ThrowIfError();

        public void Destroy()
        {
            VoxelPrimitiveBindings.Destroy(native).ThrowIfError();
            native = System.IntPtr.Zero;
        }

        protected override string DebugInfo => $"material={material}, width={width}, height={height}, depth={depth}, scale={scale}";
    }
}