namespace TMInterfaceQuickScript.Components
{
    public struct Command
    {
        public string Name { get; }
        public int? Delay { get; }
        public int? Duration { get; }
        public int? SteerStrength { get; } // Type == "s"

        public Command(string qsCommand, int? delay, int? duration, int? steerStrength)
        {
            Name = qsCommand;
            Delay = delay;
            Duration = duration;
            SteerStrength = steerStrength;
        }
    }
}
