namespace Ming.Util
{
    public static class MingIntToStrLut
    {
        public static int CutoffValue => NumberLut.Length;

        public static void SetCutoffValue(int n)
        {
            NumberLut = new string[n];
            OverflowValue = $">{n - 1}";

            for (int i = 0; i < NumberLut.Length; ++i)
            {
                NumberLut[i] = i.ToString();
            }
        }

        public static string GetString(int n)
        {
            return n > NumberLut.Length - 1 ? OverflowValue : NumberLut[n];
        }

        private static string[] NumberLut;
        private static string OverflowValue;

        static MingIntToStrLut()
        {
            SetCutoffValue(1000);
        }
    }
}
