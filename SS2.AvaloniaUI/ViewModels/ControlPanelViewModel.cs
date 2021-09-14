using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SS2.Core;
using SS2.Core.Model;
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
        public ICommand StartResetCommand { get; }


        private string _actionButtonString = "START";
        public string ActionButtonString
        {
            get => _actionButtonString;
            set => this.RaiseAndSetIfChanged(ref _actionButtonString, value);
        }

        public ControlPanelViewModel()
        {
            Items = new ObservableCollection<string>(App.Controller.GetResponses());
            StartResetCommand = ReactiveCommand.Create(StartReset);
            App.Controller.SubscribeToGameState(OnGameStateChanged);
            App.Controller.SubscribeToResponses(OnResponesChanged);
        }

        public ObservableCollection<string> Items { get; }

        public void StartReset()
        {
            Items.Clear();
            if (App.Controller.GameState == Core.Model.GameState.IDLE)
            {
                App.Controller.Start();
            } else
            {
                App.Controller.Reset();
            }
            List<string> responses = App.Controller.GetResponses();
        }

        public void OnResponesChanged(object? sender, EventArgs args)
        {
            List<string> responses = App.Controller.GetResponses()
                .Skip(Items.Count)
                .ToList();
            foreach(string res in responses)
            {
                Items.Add(res);
            }
            /* if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Avalonia.Controls.ScrollViewer viewer;
                viewer.ScrollToEnd();
                Dispatcher.UIThread.InvokeAsync(desktop.MainWindow.Scroll);
            } */
        }

        public void OnGameStateChanged(object? sender, GameState gameState)
        {
            if (gameState.Equals(GameState.STARTED))
            {
                ActionButtonString = "RESET";
            } else
            {
                ActionButtonString = "START";
            }
        }
    }
}
