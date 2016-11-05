namespace ABClient.Neuro
{
    using System.IO;

    internal class NeuroVector
    {
        private readonly char token = '?';
        private readonly double[] vector = new double[100];

        internal NeuroVector(char myToken, double[] myVector)
        {
            token = myToken;
            vector = myVector;
        }

        internal NeuroVector(BinaryReader br)
        {
            token = br.ReadChar();
            for (var i = 0; i < 100; i++)
            {
                vector[i] = ((double) br.ReadByte()) / 255;
            }
        }

        internal void SaveToStream(BinaryWriter bw)
        {
            bw.Write(token);
            for (var i = 0; i < 100; i++)
            {
                bw.Write((byte)(vector[i] * 255));
            }
        }

        internal double Distance(double[] myVector)
        {
            var distance = 0.0;
            for (var i = 0; i < 100; i++)
            {
                var diff = vector[i] - myVector[i];
                distance += diff * diff;
            }

            return distance;
        }

        internal char Token()
        {
            return token;
        }
    }
}
