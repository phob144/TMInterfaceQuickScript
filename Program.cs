using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TMInterfaceQuickScript
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var config = Config.ReadConfig();

            string qsFolderPath = Path.Combine(config.ScriptFolderPath, "qs");

            if (!Directory.Exists(qsFolderPath))
                Directory.CreateDirectory(qsFolderPath);

            // create qs file
            if (args.Length == 1)
            {
                string inputPath = Path.Combine(qsFolderPath, $"{args[0]}.qs");

                if (!File.Exists(inputPath))
                    File.Create(inputPath);
            }

            var watcher = new FileSystemWatcher(qsFolderPath, "*.qs")
            {
                EnableRaisingEvents = true
            };

            watcher.Changed += (sender, e) =>
            {
                string inputPath = e.FullPath;
                string outputPath = Path.Combine(config.ScriptFolderPath, Path.GetFileNameWithoutExtension(e.FullPath));

                FileChanged(inputPath, outputPath, config);
            };

            Console.WriteLine("READY");

            Thread.Sleep(-1);
        }

        private static void FileChanged(string inputPath, string outputPath, Config config)
        {
            bool success = false;

            while (!success)
            {
                try
                {
                    var qs = new QuickScript(File.ReadAllLines(inputPath), config);
                    File.WriteAllText(outputPath, qs.ParseToTmi());

                    success = true;
                }
                catch (IOException)
                {
                    Console.WriteLine($"{Path.GetFileName(inputPath)} IS IN USE. RETRYING IN 500MS");

                    Thread.Sleep(500);
                }
            }

            Console.WriteLine($"COMPILED {Path.GetFileName(inputPath)}");
        }
    }
}
