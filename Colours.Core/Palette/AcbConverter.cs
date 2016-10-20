using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Converts to and from <see cref="Palette"/> and Photoshop
    /// color books. (ACB files)
    /// </summary>
    /// <remarks>
    /// See Adobe's documentation: 
    /// http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1066780
    /// </remarks>
    public static class AcbConverter
    {
        /// <summary>
        /// Strings can have escape sequences for IP markings.
        /// </summary>
        /// <param name="raw">The string to unescape.</param>
        /// <returns>The unescaped string.</returns>
        static string UnescapeString(string raw)
        {
            return raw.Replace('\u00ae', '®').Replace('\u00a9', '©');
        }

        static string GetValue(string raw)
        {
            return raw.Split('=')[1];
        }

        /// <summary>
        /// Creates a palette from a Photoshop color book file.
        /// </summary>
        /// <param name="file">The file to convert from.</param>
        /// <returns>The new palette.</returns>
        public static Palette FromAcb(byte[] file)
        {
            using (var ms = new MemoryStream(file))
            {
                using (var sr = new BinaryReader(ms))
                {
                    if (new string(sr.ReadChars(4)) != "8BCB")
                        throw new PaletteException("Not a valid color book.");

                    var p = new Palette();

                    var version = sr.ReadUInt16BE();
                    var id = sr.ReadUInt16BE();

                    System.Diagnostics.Debug.WriteLine("ID {0}", id);

                    var title = GetValue(sr.ReadStringBE(false));
                    var pre = GetValue(sr.ReadStringBE(false));
                    var post = GetValue(sr.ReadStringBE(false));
                    var desc = UnescapeString(GetValue(sr.ReadStringBE(false)));
                    System.Diagnostics.Debug.WriteLine(title);
                    System.Diagnostics.Debug.WriteLine(pre);
                    System.Diagnostics.Debug.WriteLine(post);
                    System.Diagnostics.Debug.WriteLine(desc);

                    var count = sr.ReadUInt16BE();
                    var perPage = sr.ReadUInt16BE();
                    p.Columns = perPage;
                    var defaultSelection = sr.ReadUInt16BE();
                    var space = (AdobeColorSpace)sr.ReadUInt16BE();

                    for (int i = 0; i < count; i++)
                    {
                        var name = sr.ReadStringBE(true);

                        var cid = sr.ReadBytes(6);
                        RgbColor color;
                        switch (space)
                        {
                            case AdobeColorSpace.Rgb:
                                color = new RgbColor(sr.ReadByte(),
                                    sr.ReadByte(), sr.ReadByte());
                                break;
                            // For lab and cmyk, should we add 0.5?
                            case AdobeColorSpace.Lab:
                                var l = sr.ReadByte() / 2.55d + 0.5d;
                                var a = sr.ReadByte() - 128;
                                var b = sr.ReadByte() - 128;
                                color = new LabColor(l, a, b).ToXyz().ToRgb();
                                break;
                            case AdobeColorSpace.Cmyk:
                                var c = 1 - (sr.ReadByte() / 255d);
                                var m = 1 - sr.ReadByte() / 255d;
                                var y = 1 - sr.ReadByte() / 255d;
                                var k = 1 - sr.ReadByte() / 255d;
                                color = new CmykColor(c, m, y, k).ToRgb();
                                break;
                            default:
                                throw new PaletteException(string.Format(
                                    "Invalid colorspace. {0}", space));
                        }

                        p.Colors.Add(new PaletteColor(color, name));
                    }

                    if (ms.Length > ms.Position)
                        System.Diagnostics.Debug.WriteLine(new string(sr.ReadChars(8)));

                    return p;
                }
            }
        }
    }
}
