using Newtonsoft.Json;

namespace APBConfigInstaller.Models;

public abstract class ModBase
{
    public string DisplayName { get; set; }
    public Category Category { get; set; }
}

