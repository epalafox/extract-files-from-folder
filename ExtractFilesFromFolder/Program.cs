using System;

namespace ExtractFilesFromFolder
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Usage:");
				Console.WriteLine("exe \"[path]\"");
			}
			else
			{
				foreach (var path in args)
				{
					if (System.IO.Directory.Exists(path))
					{
						var children = System.IO.Directory.GetDirectories(path);
						foreach(var child in children)
							RecursiveFolder(child, path);
					}
				}
			}
		}
		static void RecursiveFolder(string path, string parent)
		{
			var list = System.IO.Directory.GetDirectories(path);
			foreach (var file in list)
			{
				RecursiveFolder(file, parent);
			}
			foreach(var file in System.IO.Directory.GetFiles(path))
			{
				var fileName = System.IO.Path.GetFileName(file);
				var newFile = System.IO.Path.Combine(parent, fileName);
				var suffix = 0;
				while(System.IO.File.Exists(newFile))
				{
					newFile = $"{newFile.Split('.')[0]}_{suffix}.{newFile.Split('.')[1]}";
				}
				Console.WriteLine($"Moving: {file} to {newFile}");
				System.IO.File.Copy(file, newFile);
			}
		}
	}
}
