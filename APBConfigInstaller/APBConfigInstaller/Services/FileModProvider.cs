using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using APBConfigInstaller.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APBConfigInstaller.Services;

public class FileModProvider : IModProvider
{
    // ToDo: Implement the FileModProvider class
    private readonly string _file = @".\Data\config.json";
    public FileModProvider()
    {
    }
    public IList<ModBase> GetMods()
    {
        JObject jObj = JObject.Parse(File.ReadAllText(_file));
        return jObj["modifications"].ToObject<List<ModBase>>();
    }
}

public interface IModProvider
{
    public IList<ModBase> GetMods();
}