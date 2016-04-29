using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Contains generic extension methods.
    /// </summary>
    public static class GenericFunctions
    {
        /// <summary>
        /// Performs a deep clone of an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="a">The object to clone.</param>
        /// <returns>The cloned object.</returns>
        public static T DeepClone<T>(this T a)
        {
            var dcs = new DataContractSerializer(typeof(T));
            using (var s = new MemoryStream())
            {
                dcs.WriteObject(s, a);
                s.Position = 0;
                return (T)dcs.ReadObject(s);
            }
        }
    }
}
