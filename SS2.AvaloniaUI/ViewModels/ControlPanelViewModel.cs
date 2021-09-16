using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SS2.Core;
using SS2.Core.Model;
using SS2.Core.Resources;
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


        private string _actionButtonString = Resources.Start;
        public string ActionButtonString
        {
            get => _actionButtonString;
            set => this.RaiseAndSetIfChanged(ref _actionButtonString, value);
        }

        public ControlPanelViewModel()
        {
            Items = App.Controller.Responses;
            StartResetCommand = ReactiveCommand.Create(StartReset);
            App.Controller.SubscribeToGameState(OnGameStateChanged);
            // App.Controller.Responses;
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
        }

        public void OnResponesChanged(object? sender, EventArgs args)
        {
            List<string> responses = App.Controller.Responses
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
                ActionButtonString = Resources.Reset;
            } else
            {
                ActionButtonString = Resources.Start;
            }
        }
    }
}
