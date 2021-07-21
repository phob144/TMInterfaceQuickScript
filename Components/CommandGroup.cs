using System.Collections.Generic;
using System.Linq;

namespace TMInterfaceQuickScript.Components
{
    public struct CommandGroup
    {
        public List<Command> Commands { get; }

        public int? TotalTime => Commands.Max(x => (x.Delay ?? 0) + (x.Duration ?? 0));

        public CommandGroup(List<Command> commands)
        {
            Commands = commands;
        }
    }
}
