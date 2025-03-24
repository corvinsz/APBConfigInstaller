using APBConfigInstaller.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace APBConfigInstaller.Data;

public class ModBaseConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(ModBase).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        // Load the JSON object
        JObject jObject = JObject.Load(reader);
        ModBase target;

        // Inspect properties to determine type
        if (jObject["FilePath"] is not null)
        {
            target = new LineReplaceMod();
        }
        else if (jObject["SourceFolder"] is not null)
        {
            target = new FileCopyMod();
        }
        else
        {
            throw new Exception("Unknown mod type");
        }

        // Populate the target object
        serializer.Populate(jObject.CreateReader(), target);
        return target;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        // You can implement this if you need serialization too.
        throw new NotImplementedException();
    }
}