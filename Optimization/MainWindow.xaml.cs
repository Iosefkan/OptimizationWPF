using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Optimization.Pages;
using Optimization.Tasks;
using Optimization.ViewModels;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Optimization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TaskPage TaskPage { get; }
        public MethodPage MethodPage { get; }
        public MainWindow()
        {
            DataContext = new MainPageViewModel();
            MethodPage = new MethodPage(() => { this.MainFrame.Content = TaskPage; });
            TaskPage = new TaskPage(() => { this.MainFrame.Content = MethodPage; });
            InitializeComponent();
            MainFrame.Content = TaskPage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainPageViewModel)DataContext).ThemeChange(MethodPage.HeatPlot, MethodPage.ColorBar, MethodPage.sciChart);
        }
    }
}