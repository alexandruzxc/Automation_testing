
using System.Threading;


namespace CreditCards.UITests
{
     internal static class DemoHelper
    {
        ////<summary>
        ///Brief delay to slow down browser interaction for
        ///demo video recording purposes
        ///</summay>

        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }

    }
}
