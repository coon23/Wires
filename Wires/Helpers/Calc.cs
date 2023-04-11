using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wires.Helpers
{
    internal static class Calc
    {
        /// <summary>
        /// Generates all pairs combinations of numbers from 0 - length-1
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static List<Tuple<int,int>> GetAllPairs(int length)
        {
            List<Tuple<int,int>> pairs = new List<Tuple<int,int>>();

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    pairs.Add(new Tuple<int,int>(i,j));
                }
            }

            return pairs;
        }

    }
}
