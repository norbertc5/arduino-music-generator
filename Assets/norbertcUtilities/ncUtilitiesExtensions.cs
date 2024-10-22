using System;

namespace norbertcUtilities.Extensions
{
    public class ncUtilitiesExtensions
    {
        // this class storages independent functions

        /// <summary>
        /// Returns valToRound rounded to roundTo
        /// </summary>
        /// <param name="valToRound"></param>
        /// <param name="roundTo"></param>
        /// <returns></returns>
        public static int RoundTo(float valToRound, int roundTo = 10)
        {
            return ((int)Math.Round(valToRound / roundTo)) * roundTo;
        }
    }
}
