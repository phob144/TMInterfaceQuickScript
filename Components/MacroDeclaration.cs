using System.Collections.Generic;
using System.Linq;

namespace TMInterfaceQuickScript.Components
{
    public struct MacroDeclaration
    {
        public string Name { get; }
        public List<CommandGroup> CommandGroups { get; }

        private int? _totalTime;
        public int? TotalTime
        {
            get
            {
                if (_totalTime != null)
                    return _totalTime;

                _totalTime = CommandGroups.Sum(x => x.TotalTime);

                return _totalTime;
            }
        }

        public MacroDeclaration(List<CommandGroup> commandGroups, string name)
        {
            CommandGroups = commandGroups;
            Name = name;

            // init
            _totalTime = null;
        }
    }
}
