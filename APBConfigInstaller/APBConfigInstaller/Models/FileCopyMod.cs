using System.Diagnostics;
using System.IO;

using Newtonsoft.Json;

namespace APBConfigInstaller.Models;

public class FileCopyMod : ModBase
{
    public string SourceFolder { get; set; }

    public string DestinationFolder { get; set; }

    public override void Apply()
    {
        foreach (string file in Directory.GetFiles(SourceFolder))
        {
            string destination = Path.Combine(DestinationFolder, Path.GetFileName(file));
            File.Copy(file, destination, true);
        }
    }
}