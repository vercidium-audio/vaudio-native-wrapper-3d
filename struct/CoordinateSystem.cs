namespace vaudionativewrapper
{
    /// <summary>
    /// Coordinate system used when calculating listener-relative reverb directionality
    /// (<see cref="managed.World.CalculateListenerRelativePan"/>). Must match vaudio.CoordinateSystem (C#) and
    /// VACoordinateSystem (native C).<br/>
    /// Internally, this SDK always computes in a left-handed space with Y+ up, X+ right and Z+ forward, same as Unity and Direct3D.
    /// </summary>
    public enum CoordinateSystem
    {
        /// <summary>
        /// Left-handed. Up = Y+, Forward = Z+, Right = X+. Identical to this SDK's internal space (no conversion applied).<br/>
        /// Yaw 0 looks down Z+ and positive yaw turns right (towards X+).
        /// </summary>
        Default = 0,

        /// <summary>
        /// Right-handed. Up = Z+, Forward = Y+, Right = X+. Matches Blender's Front (Numpad-1) view, which looks down Y+.<br/>
        /// Yaw 0 looks down Y+ and positive yaw turns left (counter-clockwise around Z+, same as Blender's Z rotation).
        /// </summary>
        Blender,

        /// <summary>
        /// Right-handed. Up = Y+, Forward = Z-, Right = X+. Matches Godot's camera/light forward
        /// convention (not the separate +Z "model front" convention Godot uses for imported assets). Same as OpenGL/glTF.<br/>
        /// Yaw 0 looks down Z- and positive yaw turns left (counter-clockwise around Y+, same as Godot's Y rotation).
        /// </summary>
        Godot,

        /// <summary>
        /// Left-handed. Up = Y+, Forward = Z+, Right = X+. Mathematically identical to <see cref="Default"/>.<br/>
        /// Yaw 0 looks down Z+ and positive yaw turns right (towards X+).
        /// </summary>
        Unity,

        /// <summary>
        /// Left-handed. Up = Z+, Forward = X+, Right = Y+.<br/>
        /// Yaw 0 looks down X+ and positive yaw turns right (towards Y+, same as FRotator).
        /// </summary>
        Unreal,

        /// <summary>
        /// Right-handed. Up = Y+, Forward = Z-, Right = X+. Same as OpenGL, glTF and Three.js. Mathematically identical to <see cref="Godot"/>.<br/>
        /// Yaw 0 looks down Z- and positive yaw turns left (counter-clockwise around Y+).
        /// </summary>
        OpenGL,
    }
}
