using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TMInterfaceQuickScript.Components
{
    // TODO: Finish

    public static class MacroDeclarationProcessor
    {
        private static string GetName(string qs)
        {
            // illegal characters: ;,{}
            return Regex.Match(qs, @"[^\;\,\{\}]+(?={)").Value;
        }
    }
}
