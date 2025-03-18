using Newtonsoft.Json;

namespace APBConfigInstaller.Models;

public class FileCopyMod : ModBase
{
    [JsonProperty(PropertyName = "sourceFolder")]
    public string SourceFolder { get; set; }

    [JsonProperty(PropertyName = "destinationFolder")]
    public string DestinationFolder { get; set; }
}