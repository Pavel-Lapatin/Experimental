using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.ViewModels;

namespace NetMastery.Lab05.FileManagerCompositionRoot.CommandLineCommands
{
    public class UploadFileCommand : CommandLineCommand
    {
        public UploadFileCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.FileCommand;

            Command(CommandLineNames.CreateOption, c =>
            {
                var arguments = c.Argument("path", EnglishLocalisation.FileUploadOptionNote, true);
                c.OnExecute(() =>
                {
                    using (var scope = _container.BeginLifetimeScope())
                    {
                        container.Resolve<FileController>()
                        .Upload(arguments.Values[0], arguments.Values[1]);
                    }
                    return 0;
                });
            });
        }
    }
}
