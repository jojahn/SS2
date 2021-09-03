using ReactiveUI;
using SS2.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SS2.AvaloniaUI.ViewModels
{
    public class ControlPanelViewModel : ViewModelBase
    {

        public ControlPanelViewModel()
        {
            Items = new ObservableCollection<string>(App.Controller.GetLines());
            StartResetCommand = ReactiveCommand.Create(StartReset);
        }

        public ObservableCollection<string> Items { get; }

        public void StartReset()
        {
            App.Controller.Reset();
        }


        public ICommand StartResetCommand { get; }
    }
}
