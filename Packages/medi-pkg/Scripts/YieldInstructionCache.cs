using System.Collections.Generic;
using UnityEngine;

namespace Medi
{
    public class YieldInstructionCache
    {
        private static Dictionary<float, WaitForSeconds> waitForSecondsLookup = new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds WaitForSeconds(float seconds)
        {
            if (!waitForSecondsLookup.ContainsKey(seconds))
            {
                waitForSecondsLookup[seconds] = new WaitForSeconds(seconds);
            }
            return waitForSecondsLookup[seconds];
        }
    }
}