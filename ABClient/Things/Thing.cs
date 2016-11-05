namespace ABClient.Things
{
    using System;
    using System.Collections.Generic;

    internal class Thing
    {
        internal string Img { get; set; }
        internal string Name { get; set; }
        internal string Description { get; set; }

        internal string[] reqkeys;
        internal string[] reqvals;
        internal string[] bonkeys;
        internal string[] bonvals;
        
        internal void SetReq(string req)
        {
            if (req == null) throw new ArgumentNullException("req");
            var sp = req.Split('|');
            var sk = new List<string>();
            var sv = new List<string>();
            for (var i = 0; i < sp.Length; i++)
            {
                var spp = sp[i].Split(new[] {": "}, StringSplitOptions.None);
                if (spp.Length != 2) continue;
                sk.Add(spp[0]);
                sv.Add(spp[1]);
            }

            reqkeys = sk.ToArray();
            reqvals = sv.ToArray();
        }

        internal void SetBon(string bon)
        {
            if (bon == null) throw new ArgumentNullException("bon");
            var sp = bon.Split('|');
            var sk = new List<string>();
            var sv = new List<string>();
            for (var i = 0; i < sp.Length; i++)
            {
                var spp = sp[i].Split(new[] { ": " }, StringSplitOptions.None);
                if (spp.Length != 2) continue;
                sk.Add(spp[0]);
                sv.Add(spp[1].TrimEnd(new[] { '%' }));
            }

            bonkeys = sk.ToArray();
            bonvals = sv.ToArray();
        }
    }
}