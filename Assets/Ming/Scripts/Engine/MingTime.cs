namespace Ming.Engine
{
    public static class MingTime
    {
        public static float Time { get; private set; }
        public static float DeltaTime { get; private set; }
        public static float UnscaledTime { get; private set; }

        internal static void Update(float newTime, float newDeltaTime, float newUnscaledTime)
        {
            Time = newTime;
            DeltaTime = newDeltaTime;
            UnscaledTime = newUnscaledTime;
        }
    }
}
