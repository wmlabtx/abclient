namespace ABClient.Neuro
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    internal class NeuroBase
    {
        /*private const string FileBaseName = "abneuro.dat";*/

        private readonly int ConstNumDigits = 5;
        private static readonly List<NeuroVector> listVectors = new List<NeuroVector>();
        private readonly List<double[]> listMatrix;
        private readonly double[] arrayDistances;
        /*private static readonly string[] arrayVotes = new string[5];*/
        private static readonly StringBuilder gyp = new StringBuilder();
        private static long elapsedTime;

        internal NeuroBase()
        {
            if (ConstNumDigits == 0)
            {
                ConstNumDigits = Helpers.Dice.Make(4, 6);
            }

            listMatrix = new List<double[]>(ConstNumDigits);
            arrayDistances = new double[ConstNumDigits];
        }

        internal static string Gyp()
        {
            return gyp.ToString();
        }

        internal static int NumNodes()
        {
            return listVectors.Count;
        }

        internal static long ElapsedTime()
        {
            return elapsedTime;
        }

        /*
        internal static string Votes()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 5; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(arrayVotes[i]);
            }

            return sb.ToString();
        }
         */ 

        internal void Calculate(Bitmap bitmapSource)
        {
            var startProcess = DateTime.Now.Ticks;
            var graymin = 255 * 3;
            var graymax = 0;
            var width = bitmapSource.Size.Width - 2;
            var height = bitmapSource.Size.Height - 2;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var color = bitmapSource.GetPixel(x + 1, y + 1);
                    var gray = color.R + color.G + color.B;
                    if (gray < graymin)
                    {
                        graymin = gray;
                    }

                    if (gray > graymax)
                    {
                        graymax = gray;
                    }
                }
            }

            if (graymin < graymax)
            {
                var grayMiddle = (graymax + graymin) / 2;
                var arrayTops = new int[width];
                var arrayBottoms = new int[width];
                for (var i = 0; i < width; i++)
                {
                    arrayTops[i] = -1;
                    arrayBottoms[i] = -1;
                }

                var xleft = -1;
                var xright = -1;

                using (var bitmapGray = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                {
                    for (var x = 0; x < width; x++)
                    {
                        for (var y = 0; y < height; y++)
                        {
                            var color = bitmapSource.GetPixel(x + 1, y + 1);
                            var gray = color.R + color.G + color.B;
                            var graynorm = (255 * (gray - graymin)) / (graymax - graymin);
                            bitmapGray.SetPixel(x, y, Color.FromArgb(graynorm, graynorm, graynorm));

                            var isBlack = gray < grayMiddle;
                            if (isBlack)
                            {
                                if (xleft == -1)
                                {
                                    xleft = x;
                                }

                                xright = x;

                                if (arrayTops[x] == -1)
                                {
                                    arrayTops[x] = y;
                                }

                                arrayBottoms[x] = y;
                            }
                        }
                    }

                    listMatrix.Clear();
                    gyp.Length = 0;
                    var realwidth = xright - xleft;
                    for (var numchar = 0; numchar < ConstNumDigits; numchar++)
                    {
                        var xcleft = ((numchar * realwidth) / ConstNumDigits) + xleft;
                        var xcright = (((numchar + 1) * realwidth) / ConstNumDigits) + xleft;
                        var widthChar = xcright - xcleft;

                        var ybtop = -1;
                        var ybbottom = -1;
                        for (var xb = xcleft; xb < xcright; xb++)
                        {
                            if ((arrayTops[xb] != -1 && ybtop == -1) ||
                                (arrayTops[xb] != -1 && ybtop != -1 && arrayTops[xb] < ybtop))
                            {
                                ybtop = arrayTops[xb];
                            }

                            if ((arrayBottoms[xb] != -1 && ybbottom == -1) ||
                                (arrayBottoms[xb] != -1 && ybbottom != -1 && arrayBottoms[xb] > ybbottom))
                            {
                                ybbottom = arrayBottoms[xb];
                            }
                        }

                        if (ybtop != -1 && ybbottom != -1)
                        {
                            var section = new Rectangle(xcleft, ybtop, widthChar, ybbottom - ybtop + 1);
                            using (
                                var bitmapChar = new Bitmap(section.Width, section.Height, PixelFormat.Format24bppRgb))
                            {
                                using (var graphChar = Graphics.FromImage(bitmapChar))
                                {
                                    graphChar.DrawImage(bitmapGray, 0, 0, section, GraphicsUnit.Pixel);
                                    var myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                                    using (var image10x10 = bitmapChar.GetThumbnailImage(10, 10, myCallback, IntPtr.Zero))
                                    {
                                        using (var bitmap10x10 = new Bitmap(image10x10))
                                        {
                                            var arrayMatrix = new double[100];
                                            var index = 0;
                                            for (var y = 0; y < 10; y++)
                                            {
                                                for (var x = 0; x < 10; x++)
                                                {
                                                    var color = bitmap10x10.GetPixel(x, y);
                                                    arrayMatrix[index++] = (double) color.R / 255;
                                                }
                                            }

                                            listMatrix.Add(arrayMatrix);
                                            gyp.Append(FindVector(numchar, arrayMatrix));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            elapsedTime = DateTime.Now.Ticks - startProcess;
        }

        /*
        internal static void Train(string train)
        {
            for (var i = 0; i < 5; i++)
            {
                if (listVectors.Count > 0 && arrayDistances[i] < 0.01)
                {
                    continue;
                }

                listVectors.Add(new NeuroVector(train[i], listMatrix[i]));
            }

            SaveToFile();
        }

        internal static void LoadFromFile()
        {
            listVectors.Clear();
            if (!File.Exists(FileBaseName))
            {
                return;    
            }

            var arrayPacked = File.ReadAllBytes(FileBaseName);
            var arrayStream = UnpackArray(arrayPacked);
            using (var inner = new MemoryStream(arrayStream))
            {
                using (var br = new BinaryReader(inner))
                {
                    var count = br.ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        listVectors.Add(new NeuroVector(br));
                    }
                }
            }
        }
         */ 

        internal static void LoadFromArray(byte[] arrayPacked)
        {
            listVectors.Clear();
            var arrayStream = UnpackArray(arrayPacked);
            using (var inner = new MemoryStream(arrayStream))
            {
                using (var br = new BinaryReader(inner))
                {
                    var count = br.ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        listVectors.Add(new NeuroVector(br));
                    }
                }
            }
        }

        private char FindVector(int index, double[] matrix)
        {
            if (listVectors.Count == 0)
            {
                return '?';
            }

            /*var listVotes = new List<NeuroVote>(10);*/
            var minDistance = double.MaxValue;
            var bestToken = '?';
            for (var i = 0; i < listVectors.Count; i++)
            {
                var distance = listVectors[i].Distance(matrix);
                /*
                if (listVotes.Count < 10 || distance < listVotes[listVotes.Count - 1].Distance)
                {
                    if (listVotes.Count == 10)
                    {
                        listVotes.RemoveAt(9);
                    }

                    var neuroVote = new NeuroVote { Token = listVectors[i].Token(), Distance = distance };
                    if (listVotes.Count == 0)
                    {
                        listVotes.Add(neuroVote);
                    }
                    else
                    {
                        var j = 0;
                        while (j < listVotes.Count)
                        {
                            if (distance < listVotes[j].Distance)
                            {
                                listVotes.Insert(j, neuroVote);
                                break;
                            }

                            j++;
                        }

                        if (j == listVotes.Count)
                        {
                            listVotes.Add(neuroVote);
                        }
                    }
                }
                */

                if (distance > minDistance)
                {
                    continue;
                }

                minDistance = distance;
                bestToken = listVectors[i].Token();
            }

            arrayDistances[index] = minDistance;
            return bestToken;

            /*
            var sb = new StringBuilder(16);
            for (var j = 0; j < listVotes.Count; j++)
            {
                sb.Append(listVotes[j].Token);
            }

            arrayVotes[index] = sb.ToString();
            var win 
            
            arrayDistances[index] = listVotes[0].Distance;
            return listVotes[0].Token;
             */ 
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }

        /*
        private static void SaveToFile()
        {
            using (var inner = new MemoryStream())
            {
                using (var bw = new BinaryWriter(inner))
                {
                    bw.Write(listVectors.Count);
                    for (var i = 0; i < listVectors.Count; i++)
                    {
                        listVectors[i].SaveToStream(bw);
                    }
                }

                var arrayPacked = PackArray(inner.ToArray());
                File.WriteAllBytes(FileBaseName, arrayPacked);
            }
        }
         
        private static byte[] PackArray(byte[] writeData)
        {
            using (var inner = new MemoryStream())
            {
                using (var stream2 = new GZipStream(inner, CompressionMode.Compress))
                {
                    stream2.Write(writeData, 0, writeData.Length);
                }

                return inner.ToArray();
            }
        }
         */ 

        private static byte[] UnpackArray(byte[] compressedData)
        {
            using (var inner = new MemoryStream(compressedData))
            {
                using (var stream2 = new MemoryStream())
                {
                    using (var stream3 = new GZipStream(inner, CompressionMode.Decompress))
                    {
                        var buffer = new byte[0x8000];
                        int count;
                        while ((count = stream3.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            stream2.Write(buffer, 0, count);
                        }
                    }

                    return stream2.ToArray();
                }
            }
        }
    }
}
