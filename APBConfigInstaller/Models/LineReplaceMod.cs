using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBConfigInstaller.Models;

public class LineReplaceMod : ModBase
{
	public string? FilePath { get; set; }

	public string? OldLine { get; set; }

	public string? NewLine { get; set; }

	protected override void InternalApply()
	{
		ReplaceText(FilePath, OldLine, NewLine);
	}
	private static void ReplaceText(string? file, string? oldText, string? newText)
	{
		string fileContent = File.ReadAllText(file);
		fileContent = fileContent.Replace(oldText, newText);
		File.WriteAllText(file, fileContent);
	}
}