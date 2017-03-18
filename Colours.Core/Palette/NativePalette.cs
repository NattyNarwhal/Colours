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
    /// Represents the "native" palette format, that implementations all
    /// interfaces and nothing more.
    /// </summary>
    [Serializable]
    [DataContract]
    public class NativePalette : IPalette, IBucketedPalette, INamedPalette
    {
        /// <summary>
        /// Gets or sets the name of the palette.
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets how many columns will the palette have if it is
        /// being displayed in a grid. If the amount of columns are 0, then
        /// the grid's logic will choose how many columns it wants.
        /// </summary>
        [DataMember]
        public int BucketSize { get; set; }
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        [DataMember]
        public List<PaletteColor> Colors { get; set; }

        /// <summary>
        /// Creates a blank palette.
        /// </summary>
        public NativePalette()
        {
            Name = "Untitled";
            BucketSize = 0;
            Colors = new List<PaletteColor>();
        }

        /// <summary>
        /// Creates a new palette from an existing one.
        /// </summary>
        /// <param name="p">The palette to convert from.</param>
        public NativePalette(IPalette p) : this()
        {
            foreach (var pc in p.Colors)
                Colors.Add(pc);
            if (p is INamedPalette)
                Name = ((INamedPalette)p).Name;
            if (p is IBucketedPalette)
                BucketSize = ((IBucketedPalette)p).BucketSize;
        }

        /// <summary>
        /// Creates the palette from a string containing the file.
        /// </summary>
        /// <param name="file">The palette to convert from.</param>
        public static NativePalette CreateFromFile(string file)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(file)))
            {
                var s = new DataContractSerializer(typeof(NativePalette));
                return (NativePalette)s.ReadObject(ms);
            }
        }

        /// <summary>
        /// Gets the palette as a palette file.
        /// </summary>
        /// <returns>The contents of the file as a byte array.</returns>
        public byte[] ToFile()
        {
            using (var ms = new MemoryStream())
            {
                var s = new DataContractSerializer(typeof(NativePalette));
                s.WriteObject(ms, this);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    return Encoding.UTF8.GetBytes(sr.ReadToEnd());
                }
            }
        }

        /// <summary>
        /// Creates a new palette with properties identical to the old one.
        /// </summary>
        /// <remarks>
        /// This is intended for changing the properties of a palette, while
        /// preserving the old version's properties, due to changing the
        /// reference.
        /// </remarks>
        /// <returns>The new palette.</returns>
        public IPalette Clone()
        {
            var p = new NativePalette();
            p.Name = Name;
            p.BucketSize = BucketSize;
            p.Colors = new List<PaletteColor>();
            // simply copying the list doesn't make it deep but a fill
            // deep copy would make the colors different; not desirable
            foreach (var pc in Colors)
                p.Colors.Add(pc);
            return p;
        }
    }
}
