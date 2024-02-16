namespace Ming
{
    /// <summary>
    /// Order of execution:
    ///  1) Ming passes up until (and including) JustBeforeUnityUpdate.
    ///  2) Unity's own Update()
    ///  3) Ming passes up until (and including) JustBeforeUnityLate.
    ///  4) Unity's own LateUpdate()
    /// </summary>
    public enum MingUpdatePass
    {
        MingEarly,
        MingDefault,
        JustBeforeUnityUpdate,
        MingLate,
        MingDrawMeshes,
        JustBeforeUnityLate,
    };
}
