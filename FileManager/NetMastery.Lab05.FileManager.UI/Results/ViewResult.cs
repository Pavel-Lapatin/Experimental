using NetMastery.Lab05.FileManager.UI.ViewModels;
using NetMastery.Lab05.FileManager.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Results
{
    public class ViewResult : ActionResult
    {
        public View View { get; set; }
        public ViewResult(View view)
        {
            View = view;
        }
    }
}
