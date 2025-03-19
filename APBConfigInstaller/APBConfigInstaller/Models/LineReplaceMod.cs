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
    public string FilePath { get; set; }

    public string OldLine { get; set; }

    public string NewLine { get; set; }
}

