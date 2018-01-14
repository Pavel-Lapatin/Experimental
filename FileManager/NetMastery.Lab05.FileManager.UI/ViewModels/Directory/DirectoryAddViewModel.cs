
namespace NetMastery.Lab05.FileManager.UI.ViewModels.Directory
{
    public class DirectoryAddViewModel : PathViewModel
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
                else if (!IsNameValid(value))
                {
                    AddError(nameof(Name), "The characters: /,|,:,*,<,>,\\,~\" are not allowed");
                }
                name = value;
            }
        }

        public DirectoryAddViewModel(string path, string name) : base(path)
        {
            Name = name;
        }
    }
}
