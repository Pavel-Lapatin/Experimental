using Autofac;


namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.DirectoryCommands
{
    class DirectoryCommand : CommandLineCommand
    {
        public DirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.DirectoryCommand;
            this.Commands.Add(new AddDirectoryCommand(container));
            this.Commands.Add(new ChangeWorkDirectoryCommand(container));
            this.Commands.Add(new MoveDirectoryCommand(container));
            this.Commands.Add(new RemoveDirectoryCommand(container));
            this.Commands.Add(new InfoDirectoryCommand(container));
            this.Commands.Add(new SearchDirectoryCommand(container));
            this.Commands.Add(new ListDirectoryCommand(container));
        }
    }
}
