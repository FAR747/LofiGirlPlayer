using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LofiGirlPlayer
{
	public class SourceList
	{
		[JsonProperty("version")]
		public long Version { get; set; }

		[JsonProperty("sources")]
		public List<SourceListElement> Sources { get; set; }
	}
	public partial class SourceListElement
	{
		[JsonProperty("Name")]
		public string Name { get; set; }

		[JsonProperty("YTID")]
		public string Ytid { get; set; }
	}
}
