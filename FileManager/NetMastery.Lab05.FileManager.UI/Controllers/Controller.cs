using NetMastery.Lab05.FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public abstract class Controller
    {
        protected AppViewModel Model { get; set; }

        public Controller(AppViewModel model)
        {
           
            Model = model;
        }


        protected bool IsAuthenticate()
        {
            if (Model.AuthenticatedLogin != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected string CreatePath(string newPath)
        {
            string[] pathParts = newPath.Split('\\');
            var path = new StringBuilder();
            foreach (var partName in pathParts)
            {
                switch (partName)
                {
                    case "..":
                        if (path.Length == 0) path.Append(Model.CurrentPath);
                        var index = path.ToString().LastIndexOf("\\");
                        path = path.Remove(index, path.Length - index);
                        break;
                    case ".":
                        if (path.Length == 0) path.Append(Model.CurrentPath);
                        break;
                    default:
                        if (partName.Any(x => x == '/'
                        || x == ':'
                        || x == '*'
                        || x == '?'
                        || x == '<'
                        || x == '>'
                        || x == '\"'
                        || x == '|'
                        || x == '~'))
                        {
                            throw new ArgumentException("The characters: /,|,:,*,<,>,\\,~\" are not allowed");
                        }
                        if (!string.IsNullOrEmpty(partName))
                        {
                            path.Append(partName);
                        }
                        break;
                }
            }
            return path.ToString();
        }
    }
}
