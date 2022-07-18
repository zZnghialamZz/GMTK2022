using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2022.Utils
{
    public static class MathLib
    {
        /// <summary>
        /// Moves from "from" to "to" by the specified amount and returns the corresponding value
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="amount">Amount.</param>
        public static float Approach(float from, float to, float amount)
        {
            if (from < to)
            {
                from += amount;
                if (from > to)
                    return to;
            }
            else
            {
                from -= amount;
                if (from < to)
                    return to;
            }

            return from;
        }
    }
}