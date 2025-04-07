using APBConfigInstaller.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller.Models;

[JsonConverter(typeof(ModBaseConverter))]
public abstract class ModBase
{
	public string? DisplayName { get; set; }
	public Category Category { get; set; }

	public abstract void Apply();
}
