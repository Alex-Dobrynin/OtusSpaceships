namespace OtusSpaceships.Commands
{
    public class MacroCommand : ICommand
    {
        private readonly List<ICommand> _commands;

        public MacroCommand(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToList();
        }

        public MacroCommand(params ICommand[] commands)
        {
            _commands = commands.ToList();
        }

        public void Execute()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
        }
    }
}
