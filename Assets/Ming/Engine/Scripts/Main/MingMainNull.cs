using System;

namespace Ming
{
    public class MingMainNull : IMingMain
    {
        public static MingMainNull Instance = new ();

        private MingMainNull() { }

        private static string NullErrorMessage() =>
            throw new InvalidOperationException($"{nameof(MingMain)} is null, make sure you have {nameof(MingMain)} in the scane and that it is set before Default in Project Settings/Script Execution Order");

        public IMingUpdater MingUpdater => throw new InvalidOperationException(NullErrorMessage());
    }
}
