using System.Collections;

namespace Ming
{
    public class MingYielders
    {
        public static IEnumerator WaitUntil(float endTime, bool useRealTime = false)
        {
            if (useRealTime)
            {
                while (MingTime.UnscaledTime < endTime)
                    yield return null;
            }
            else
            {
                while (MingTime.Time < endTime)
                    yield return null;
            }
        }

        public static IEnumerator WaitForSeconds(float sec, bool useRealTime = false)
        {
            float endTime = useRealTime ? MingTime.UnscaledTime + sec : MingTime.Time + sec;
            yield return WaitUntil(endTime, useRealTime);
        }
    }
}
