using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EbPsiAnimationEditor.Tools;
using EbPsiAnimationEditor.Structures;
using Newtonsoft.Json;

namespace EbPsiAnimationEditor.EB
{
    public sealed class RomConfig
    {
        [JsonProperty(PropertyName = "Parameters")]
        public Dictionary<string, int> Parameters { get; private set; }

        [JsonProperty(PropertyName = "AnimationNames")]
        public string[] AnimationNames { get; private set; }

        [JsonProperty(PropertyName = "FreeRanges")]
        public RangeCollection FreeRanges { get; set; }

        private RomConfig()
        {

        }

        public static RomConfig GenerateDefaults()
        {
            var config = new RomConfig();

            config.Parameters = new Dictionary<string, int>();
            config.Parameters.Add("psi animation info", 0xCF04D);
            config.Parameters.Add("psi animation count", 34);
            config.Parameters.Add("psi animation arrangements", 0xCF58F);
            config.Parameters.Add("psi animation palettes", 0xCF47F);

            config.AnimationNames = new string[]
            {
                "Counter-PSI unit",
                "Brainshock alpha",
                "Brainshock omega",
                "HP-sucker",
                "Defense down alpha",
                "Defense down omega",
                "PSI Fire alpha",
                "PSI Fire beta",
                "PSI Fire gamma",
                "PSI Fire omega",
                "PSI Flash alpha",
                "PSI Flash beta",
                "PSI Flash gamma",
                "PSI Flash omega",
                "PSI Freeze alpha",
                "PSI Freeze beta",
                "PSI Freeze gamma",
                "PSI Freeze omega",
                "PSI Special alpha",
                "PSI Special beta",
                "PSI Special gamma",
                "PSI Special omega",
                "Paralysis alpha",
                "Paralysis omega",
                "PSI Magnet alpha",
                "PSI Magnet omega",
                "Shield killer",
                "Hypnosis alpha",
                "Hypnosis omega",
                "Hungry HP-sucker",
                "PSI Starstorm alpha",
                "PSI Starstorm beta",
                "PSI Thunder alpha/beta",
                "PSI Thunder gamma/omega"
            };

            var rc = new RangeCollection();
            rc.Add(new Range(0xC2E19, 0xC234));
            rc.Add(new Range(0xCF1E5, 0x29A));

            config.FreeRanges = rc;

            return config;
        }

        public static RomConfig FromFile(string configPath)
        {
            string jsonString = File.ReadAllText(configPath);
            var settings = new JsonSerializerSettings();

            RomConfig config = JsonConvert.DeserializeObject<RomConfig>(jsonString, settings);

            if (!Verify(config))
            {
                return null;
            }

            return config;
        }

        public void Save(string configPath)
        {
            var settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;

            string jsonString = JsonConvert.SerializeObject(this, settings);
            File.WriteAllText(configPath, jsonString);
        }

        private static bool Verify(RomConfig romConfig)
        {
            if (romConfig.Parameters == null)
                throw new Exception("Parameters not found");

            if (romConfig.AnimationNames == null)
                throw new Exception("Animation names not found");

            if (romConfig.FreeRanges == null)
                throw new Exception("Free ranges not defined");

            // Check that the parameters have all needed information
            var parameters = romConfig.Parameters;
            if (!parameters.ContainsKey("psi animation info") ||
                !parameters.ContainsKey("psi animation count") ||
                !parameters.ContainsKey("psi animation arrangements") ||
                !parameters.ContainsKey("psi animation palettes"))
                throw new Exception("Parameters incomplete");

            if (romConfig.AnimationNames.Length != parameters["psi animation count"])
                throw new Exception("PSI animation count and animation names do not match");

            // Check that the free ranges do not overlap, nor cross bank boundaries
            var rc = romConfig.FreeRanges.Ranges;
            for (int i = 0; i < rc.Count; i++)
            {
                for (int j = i + 1; j < rc.Count; j++)
                {
                    if (rc[i].Contains(rc[j].Start) || rc[i].Contains(rc[j].End)
                        || rc[j].Contains(rc[i].Start) || rc[j].Contains(rc[i].End))
                    {
                        throw new Exception("Free ranges overlap");
                    }
                }
            }

            return true;
        }

        public RomConfig CreateCopy()
        {
            var config = new RomConfig();

            config.AnimationNames = new string[this.AnimationNames.Length];
            for (int i = 0; i < this.AnimationNames.Length; i++)
                config.AnimationNames[i] = this.AnimationNames[i];

            config.Parameters = new Dictionary<string, int>();
            foreach(var kv in this.Parameters)
                config.Parameters.Add(kv.Key, kv.Value);

            config.FreeRanges = new RangeCollection(this.FreeRanges);

            return config;
        }
    }
}
