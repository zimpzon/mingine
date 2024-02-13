namespace Ming.Engine
{
    /// <summary>
    /// Component passes can be used not only for order of execution, but also for
    /// performance improvements by ensuring all components of the same type run in
    /// sequence. This is beneficial if they operate on the same small(-ish) data set.
    /// New passes can be added to enum ComponentUpdatePass without further code changes.
    /// Order of execution:
    ///  1) All Ming passes except ComponentUpdatePass.Late
    ///  2) Unity Update()
    ///  3) ComponentUpdatePass.Late and all passes after
    ///  4) Unity LateUpdate()
    /// </summary>
    public enum MingUpdatePass
    {
        CollisionSetup,
        Early,
        Default,
        Physics,
        UnityUpdate,
        Late,
        MingDrawMeshes,
        UnityLate,
    };
}
