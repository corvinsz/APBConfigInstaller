using Newtonsoft.Json;

namespace APBConfigInstaller.Models;

public class FileCopyMod : ModBase
{
    public string SourceFolder { get; set; }

    public string DestinationFolder { get; set; }
}