using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMInterfaceQuickScript.Components;

namespace TMInterfaceQuickScript
{
    public class QuickScript
    {
        private readonly CommandParser _parser;

        private readonly string[] _script;
        private readonly List<CommandGroup> _commandGroups;

        public QuickScript(string[] script, Config config)
        {
            _parser = new CommandParser(config);

            script = RemoveWhitespaces(script);

            _script = script;
            _commandGroups = ParseFromQs();
        }

        private string[] RemoveWhitespaces(string[] script)
            => script.Select(x => Regex.Replace(x, @"\s+", "")).ToArray();

        private List<CommandGroup> ParseFromQs()
        {
            return _script.Select(x => _parser.ParseFromQs(x)).ToList();
        }

        public string ParseToTmi()
        {
            int time = 0;
            string result = "";

            foreach (var group in _commandGroups)
            {
                result += _parser.ParseToTmi(group, time) + Environment.NewLine;
                time += group.TotalTime.Value;
            }

            return result;
        }
    }
}
