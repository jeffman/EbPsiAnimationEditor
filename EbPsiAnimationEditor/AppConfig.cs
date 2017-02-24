using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;

namespace EbPsiAnimationEditor
{
    public sealed class AppConfig
    {
        [JsonProperty]
        public bool FastCompress { get; set; }

        [JsonProperty]
        public bool Multithreaded { get; set; }

        [JsonProperty]
        public bool Grid { get; set; }

        [JsonProperty]
        public Color GridColor { get; set; }

        [JsonProperty(PropertyName = "SelectionColor")]
        public Color TileColor { get; set; }

        [JsonProperty]
        public int Zoom { get; set; }

        private AppConfig()
        {

        }

        public static AppConfig GenerateDefaults()
        {
            var config = new AppConfig();

            config.FastCompress = true;
            config.Multithreaded = true;
            config.Grid = true;
            config.GridColor = Color.FromArgb(170, 140, 50);
            config.TileColor = Color.FromArgb(128, 150, 130, 40);
            config.Zoom = 2;

            return config;
        }

        public static AppConfig FromFile(string configPath)
        {
            string jsonString = File.ReadAllText(configPath);
            var settings = new JsonSerializerSettings();

            AppConfig config = JsonConvert.DeserializeObject<AppConfig>(jsonString, settings);

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

        private static bool Verify(AppConfig appConfig)
        {
            // Basic properties don't need to be verified
            return true;
        }

        public AppConfig CreateCopy()
        {
            var config = new AppConfig();
            
            config.FastCompress = this.FastCompress;
            config.Multithreaded = this.Multithreaded;
            config.Grid = this.Grid;
            config.GridColor = this.GridColor;
            config.TileColor = this.TileColor;
            config.Zoom = this.Zoom;

            return config;
        }
    }
}
