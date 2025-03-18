using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace APBConfigInstaller.Models;

public enum Category
{
    QualityOfLife,
    Visual,
    Audio
}
public class LineReplaceMod : ModBase
{

    [JsonProperty(PropertyName = "filePath")]
    public string FilePath { get; set; }

    [JsonProperty(PropertyName = "oldLine")]
    public string OldLine { get; set; }

    [JsonProperty(PropertyName = "newLine")]
    public string NewLine { get; set; }
}

