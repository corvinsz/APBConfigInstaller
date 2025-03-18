using Newtonsoft.Json;

namespace APBConfigInstaller.Models;

public abstract class ModBase
{
    [JsonProperty(PropertyName = "displayName")]
    public string DisplayName { get; set; }

    [JsonProperty(PropertyName = "category")]
    public Category Category { get; set; }
}

