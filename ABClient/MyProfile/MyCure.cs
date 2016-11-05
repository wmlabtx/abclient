namespace ABClient.MyProfile
{
    using System.Xml;

    internal class MyCure
    {
        private readonly int[] NVDefault = new[] { 10, 15, 25, 600 };
// ReSharper disable ConvertToConstant
        private bool Disabled04Default = true;
// ReSharper restore ConvertToConstant

        internal MyCure()
        {
            NV = new[] {10, 15, 25, 600};
            Ask = new[] {"Лечить легкую за [1]?", "Лечить среднюю за [2]?", "Лечить тяж за [3]?", "Лечить боевую за [4]?"};
            Adv = "Лечу [1]/[2]/[3], боевые - [4]";
            After = "Поздравляю, не болей";
            Boi = "Выйди из боя!";
            Enabled = new[] {true, true, true, true};
            Disabled04 = Disabled04Default;
        }

        internal int[] NV { get; set; }

        internal string[] Ask { get; set; }

        internal string Adv { get; set; }

        internal string After { get; set; }

        internal string Boi { get; set; }

        internal bool[] Enabled { get; set; }

        internal bool Disabled04 { get; set; }

        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("cure");
            for (var i = 0; i < 4; i++)
            {
                if (NV[i] == NVDefault[i]) continue;
                writer.WriteStartAttribute("nv" + (i + 1));
                writer.WriteValue(NV[i]);
                writer.WriteEndAttribute();
            }

            for (var i = 0; i < 4; i++)
            {
                if (string.IsNullOrEmpty(Ask[i])) continue;
                writer.WriteStartAttribute("cask" + (i + 1));
                writer.WriteString(Ask[i]);
                writer.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(Adv))
            {
                writer.WriteStartAttribute("cadv");
                writer.WriteString(Adv);
                writer.WriteEndAttribute();                
            }

            if (!string.IsNullOrEmpty(After))
            {
                writer.WriteStartAttribute("cafter");
                writer.WriteString(After);
                writer.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(Boi))
            {
                writer.WriteStartAttribute("cboi");
                writer.WriteString(Boi);
                writer.WriteEndAttribute();
            }

            for (var i = 0; i < 4; i++)
            {
                if (Enabled[i]) continue;
                writer.WriteStartAttribute("e" + (i + 1));
                writer.WriteValue(false);
                writer.WriteEndAttribute();
            }

            if (Disabled04 != Disabled04Default)
            {
                writer.WriteStartAttribute("d04");
                writer.WriteValue(Disabled04);
                writer.WriteEndAttribute();                
            }

            writer.WriteEndElement();
        }

        internal void Read(XmlReader reader)
        {
            for (var i = 0; i < 4; i++)
            {
                int nv;
                if (!int.TryParse(reader["nv" + (i + 1)], out nv))
                {
                    nv = NVDefault[i];
                }

                NV[i] = nv;

                if (reader["cask" + (i + 1)] != null)
                {
                    Ask[i] = reader["cask" + (i + 1)];
                }

                bool enabled;
                if (!bool.TryParse(reader["e" + (i + 1)], out enabled))
                {
                    enabled = true;
                }

                Enabled[i] = enabled;
            }

            if (reader["cadv"] != null)
            {
                Adv = reader["cadv"];
            }

            if (reader["cafter"] != null)
            {
                After = reader["cafter"];
            }

            if (reader["cboi"] != null)
            {
                Boi = reader["cboi"];
            }

            if (reader["d04"] != null)
            {
                bool disabled04;
                if (!bool.TryParse(reader["d04"], out disabled04))
                {
                    disabled04 = Disabled04Default;
                }

                Disabled04 = disabled04;
            }

            if (NV[0] < 5 || NV[0] > 50)
            {
                NV[0] = NVDefault[0];
            }

            if (NV[1] < 8 || NV[1] > 100)
            {
                NV[1] = NVDefault[1];
            }

            if (NV[2] < 11 || NV[2] > 150)
            {
                NV[2] = NVDefault[2];
            }

            if (NV[3] < 296 || NV[3] > 900)
            {
                NV[3] = NVDefault[3];
            }
        }
    }
}