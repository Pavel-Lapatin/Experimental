
namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class TwoPathViewModel : PathViewModel
    {
        private string secondPath;
        public string SecondPath
        {
            get { return secondPath; }
            set
            {
                ValidateInputPath(value, nameof(SecondPath));
                secondPath = value;
            }
        }
        public TwoPathViewModel(string path, string secondPath) : base(path)
        {
            SecondPath = secondPath;
        }

    }
}
