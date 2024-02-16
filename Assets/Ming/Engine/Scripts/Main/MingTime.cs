namespace Ming
{
    public static class MingTime
    {
        public static float Time { get; private set; }
        public static float DeltaTime { get; private set; }
        public static float UnscaledTime { get; private set; }
        public static float UnscaledDeltaTime { get; private set; }

        internal static void Update(float newTime, float newDeltaTime, float newUnscaledTime, float newUnscaledDeltaTime)
        {
            Time = newTime;
            DeltaTime = newDeltaTime;
            UnscaledTime = newUnscaledTime;
            UnscaledDeltaTime = newUnscaledDeltaTime;
        }
    }
}
