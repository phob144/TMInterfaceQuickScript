using System;
using System.Collections.Generic;
using System.Linq;

namespace TMInterfaceQuickScript.Components
{
    public class CommandParser
    {
        private readonly Dictionary<string, string> _commandTranslations;
        private readonly Dictionary<string, List<CommandGroup>> _macroCommandNames;

        public CommandParser(Config config)
        {
            _commandTranslations = config.CommandTranslations;

            _macroCommandNames = new Dictionary<string, List<CommandGroup>>();
        }

        public void AddMacroCommand(string name, string[] script)
        {
            _macroCommandNames.Add(name, script.Select(x => ParseFromQs(x)).ToList());
        }

        #region QS
        // [0] == QsCommand, [1] == Delay, [2] == Duration, [3] == SteerStrength
        public CommandGroup ParseFromQs(string qs)
        {
            return new CommandGroup(qs.Split(';')
                .Select(x => ParseSingleCommandFromQs(x))
                .ToList());
        }

        private Command ParseSingleCommandFromQs(string qs)
        {
            var args = qs.Split(',');

            string name = args.Length > 0 ? args[0] : null;
            int? delay = args.Length > 1 ? int.Parse(args[1]) : null;
            int? duration = args.Length > 2 ? int.Parse(args[2]) : null;
            int? steerStrength = args.Length > 3 ? int.Parse(args[3]) : null;

            return new Command(name, delay, duration, steerStrength);
        }
        #endregion

        #region TMI
        public string ParseToTmi(CommandGroup commandGroup, int currentTime)
        {
            return string.Join(Environment.NewLine, commandGroup.Commands.Select(x => ParseSingleCommandToTmi(x, currentTime)));
        }

        private string ParseSingleCommandToTmi(Command command, int currentTime)
        {
            if (_commandTranslations.ContainsKey(command.Name))
            {
                int beginTime = currentTime + command.Delay ?? 0;

                string result = "";

                result += beginTime; // <int>
                result += command.Duration == null ? "" : $"-{beginTime + command.Duration}"; // <int>-<int>
                result += $" {_commandTranslations[command.Name]}"; // <int>-<int> <string>
                result += command.SteerStrength == null ? "" : $" {command.SteerStrength}"; // <int>-<int> <string> <int>

                return result;
            }
            else if (_macroCommandNames.ContainsKey(command.Name))
            {
                int beginTime = currentTime + command.Delay ?? 0;

                return string.Join(Environment.NewLine, _macroCommandNames[command.Name].Select(x => ParseToTmi(x, beginTime)));
            }

            return "";
        }
        #endregion
    }
}
