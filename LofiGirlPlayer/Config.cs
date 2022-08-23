using System.IO;
using Newtonsoft.Json;

namespace LofiGirlPlayer
{
	public class Config
	{
		[JsonProperty("version")]
		public int Version { get; set; }

		[JsonProperty("disablegpu")]
		public bool DisableGPU { get; set; }
		[JsonProperty("disableautoplay")]
		public bool DisableAutoPlay { get; set; }
		[JsonProperty("ytquality")]
		public string YTQuality { get; set; } // 240p: small, 360p: medium, 480p: large, 720p: hd720, 1080p: hd1080 
		[JsonProperty("ytvolume")]
		public string YTVolume { get; set; }
	}

	public static class ConfigManager
	{
		public const string CONFIGFILE = "./Config.json";
		public static Config LoadConfig()
		{
			if (File.Exists(CONFIGFILE))
			{
				string json = File.ReadAllText(CONFIGFILE);
				try
				{
					Config config = JsonConvert.DeserializeObject<Config>(json);
					return config;
				}
				catch
				{
					return null;
				}
			}
			else
			{
				Config config = new Config();
				config.Version = App.VERSION_CODE;
				config.DisableGPU = false;
				config.DisableAutoPlay = false;
				config.YTQuality = "small";
				config.YTVolume = "1.0";
				SaveConfig(config);
				return config;
			}
		}
		public static bool SaveConfig(Config config)
		{
			try
			{
				string json = JsonConvert.SerializeObject(config, Formatting.Indented);
				File.WriteAllText(CONFIGFILE, json);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}


}
