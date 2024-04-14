using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Optimization.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Optimization.Messeges;

namespace Optimization.ViewModels
{
    internal partial class TaskPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string canMove = "Doing it";
        

        [ObservableProperty]
        private FirstTask firstvar = new();

        [ObservableProperty]
        private SecondTask secondvar = new();
        
        public void SendSelectedMethod(ComboBox box)
        {
            if (box.SelectedIndex == 0)
            {
                WeakReferenceMessenger.Default.Send(new SelectedTaskMessage(Firstvar));
            }
            if (box.SelectedIndex == 1)
            {
                WeakReferenceMessenger.Default.Send(new SelectedTaskMessage(Secondvar));
            }
        }
        
    }
}
