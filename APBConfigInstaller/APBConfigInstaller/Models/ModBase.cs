using APBConfigInstaller.Data;

using Newtonsoft.Json;

namespace APBConfigInstaller.Models;

[JsonConverter(typeof(ModBaseConverter))]
public abstract class ModBase
{
    public string DisplayName { get; set; }
    public Category Category { get; set; }

    public abstract void Apply();
}

