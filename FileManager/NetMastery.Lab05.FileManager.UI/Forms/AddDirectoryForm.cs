using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Forms
{
    public class AddDirectoryForm : OnePathForm
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
                if(!CheckValidCharactersInName(name))
                {
                    AddError(nameof(Name), "The characters: /,|,:,*,<,>,\\,~\" are not allowed");
                }
                name = value;
            }
        }

        public AddDirectoryForm(string destinationPath, string name) : base(destinationPath)
        {
        }
    }
}
