using System.Collections.Generic;
using System.IO;

namespace Wires.Helpers
{
    internal static class InputHelper
    {

        /// <summary>
        /// Returns array of double from text file.
        /// </summary>
        /// <param name="file">Text file path.</param>
        /// <returns></returns>
        public static double[] GetValuesFromTxt(string file)
        {
            List<double> values = new List<double>();
            foreach(string line in File.ReadAllLines(file))
            {
                if (double.TryParse(line, out double parsed))
                {
                    values.Add(parsed);
                }               
            }

            return values.ToArray();
        }
    }
}
