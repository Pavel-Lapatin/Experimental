using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.ViewModels.Directory
{
    public class DirectoryAddViewModel : DirectoryViewModel
    {
        private string name;

        public string Name
        {

            get { return name; }
            set
            {
                RemoveError(nameof(Name));
                if (string.IsNullOrEmpty(value))
                {
                    AddError(nameof(Name), "Directory name shouldn't be null or empty");
                }
                else if (!IsFolderOrFileNameIsValid(value))
                {
                    AddError(nameof(Name), "The characters: /,|,:,*,<,>,\\,~\" are not allowed");
                }
                name = value;
            }
        }

        public DirectoryAddViewModel(string currentPath, string destinationPath, string name) : base(currentPath, destinationPath)
        {
            Name = name;
        }

        public override void RenderViewModel()
        {
            base.RenderViewModel();
            Console.WriteLine("Directory added seccessfully");
        }
    }
}
