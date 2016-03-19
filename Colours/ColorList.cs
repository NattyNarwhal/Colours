using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colours
{
    /// <summary>
    /// This is an internal class to serialize a int array.
    /// </summary>
    [Serializable]
    public class ColorList : List<int>
    {
        public ColorList()
        {

        }

        public ColorList(int[] win32colors)
        {
            AddRange(win32colors);
        }
    }
}
