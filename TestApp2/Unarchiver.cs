using Aspose.Zip.Gzip;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TestApp2
{
    public class Unarchiver
    {
        private readonly char separator = ',';
        private readonly string fileName = "OnlyGSM.csv";

        public async Task DownloadFileAsync(string uri, string outputPath)
        {
            using HttpClient httpClient = new HttpClient();
            Stream stream = await httpClient.GetStreamAsync(uri);

            if (!Directory.Exists(outputPath))
            {
                Console.WriteLine("This path is not exist");
                return;
            }

            string savePath = Path.Combine(outputPath, fileName);

            using (var archive = new GZipStream(stream, CompressionMode.Decompress))
            {          
                using (var reader = new StreamReader(archive))
                {
                    using (var extracted = File.CreateText(savePath))
                    {
                        string? line;
                        var resultLine = new StringBuilder();

                        while ((line = reader.ReadLine()) != null)
                        {
                            Edit(resultLine, line);
                            extracted.Write(resultLine);
                        }                           
                    }
                }
            }
        }

        private void Edit(StringBuilder outLine, string line)
        {
            outLine.Clear();
            var lineSpan = line.AsSpan();

            if (!(lineSpan[0] == 'G' && lineSpan[1] == 'S' && lineSpan[2] == 'M'))
                return;

            var index = line.IndexOf(separator);
            if (index == -1)
                return;

            var nextIndex = NextIndex(index, line);
            if (nextIndex == -1)
                return;

            ushort mcc;
            ushort.TryParse(lineSpan.Slice(index + 1, nextIndex - index - 1), out mcc);

            index = nextIndex;
            nextIndex = NextIndex(index, line);
            if (nextIndex == -1)
                return;

            byte mnc;
            byte.TryParse(lineSpan.Slice(index + 1, nextIndex - index - 1), out mnc);


            index = nextIndex;
            nextIndex = NextIndex(index, line);
            if (nextIndex == -1)
                return;

            ushort lac;
            ushort.TryParse(lineSpan.Slice(index + 1, nextIndex - index - 1), out lac);

            index = nextIndex;
            nextIndex = NextIndex(index, line);
            if (nextIndex == -1)
                return;

            uint cellId;
            uint.TryParse(lineSpan.Slice(index + 1, nextIndex - index - 1), out cellId);

            index = nextIndex;
            nextIndex = NextIndex(index, line);
            if (nextIndex == -1)
                return;

            index = nextIndex;
            nextIndex = NextIndex(index, line);
            if (nextIndex == -1)
                return;

            double lon;
            double.TryParse(lineSpan.Slice(index + 1, nextIndex - index - 1), NumberStyles.Float, CultureInfo.InvariantCulture, out lon);

            index = nextIndex;
            nextIndex = NextIndex(index, line);
            if (nextIndex == -1)
                return;

            double lat;
            double.TryParse(lineSpan.Slice(index + 1, nextIndex - index - 1), NumberStyles.Float, CultureInfo.InvariantCulture, out lat);


            outLine.Append(mcc).Append(',');
            outLine.Append(mnc).Append(',');
            outLine.Append(lac).Append(',');
            outLine.Append(cellId).Append(',');
            outLine.Append(lon.ToString(CultureInfo.InvariantCulture)).Append(',');
            outLine.Append(lat.ToString(CultureInfo.InvariantCulture)).AppendLine();
        }

        private int NextIndex(int index, string line)
        {
            return line.IndexOf(separator, index + 1);
        }
    }
}
