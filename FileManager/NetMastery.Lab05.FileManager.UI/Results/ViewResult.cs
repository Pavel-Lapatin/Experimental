using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Results
{
    class ViewResult : ActionResult
    {
        public ViewModel ViewModel { get; set; }
        public ViewResult(ViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
