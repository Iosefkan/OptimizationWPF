using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Messaging;
using Optimization.Tasks;
using Optimization.ViewModels;

namespace Optimization.Pages
{
    /// <summary>
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        private Action Move;
        private readonly string[] _tasks = new string[]
        {
            "Объектом оптимизации является химический реактор, в котором происходит образование целевого компонента. Реактор  оборудован  двумя теплообменными устройствами: змеевиком и диффузором. Необходимо определить температурные условия технологического процесса, обеспечивающие минимальную себестоимость целевого компонента. Согласно эмпирической математической модели, количество  получаемого целевого компонента  S ( кг) связано с параметрами процесса следующим образом:\r\nS = α*G*((T2-β*A)^N+µ*exp(T1+T2)^N+∆*(T2-T1))\r\n",
            "Объектом оптимизации является химический реактор, в котором помимо целевого компонента образуется побочный (вредный) продукт. В силу этого необходимы существенные затраты на очистку реакционной массы. Известно, что количество побочного продукта С (кг) связано с условиями проведения процесса следующим выражением:\r\nC = α*(G*µ*((T2-T1)^V+(β *A-T1)^V))\r\nНеобходимо определить такие значения температуры хладагента в теплообменных устройствах реактора ( Т1 и Т2 ), при которых будут минимальны затраты на очистку реакционной массы от вредного продукта."
        };
        private readonly string[] _taskParam = new string[]
        {
            "Task1",
            "Task2"
        };
        public TaskPage(Action move)
        {
            InitializeComponent();
            
            DataContext = new TaskPageViewModel();
            ToMethodButton.IsEnabled = false;
            Move = move;
            ErrorNotice.Visibility = Visibility.Hidden;
        }

        private void TasksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToMethodButton.IsEnabled = true;
            int index = TasksComboBox.SelectedIndex;
            TaskAbout.Text = _tasks[index];
            TaskParam.Content = this.Resources[_taskParam[index]];
            if (Validator.IsValid(this))
            {
                ErrorNotice.Visibility = Visibility.Hidden;
            }
        }

        private void ToMethodButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksComboBox.SelectedIndex < 0 || !Validator.IsValid(this))
            {
                ErrorNotice.Visibility = Visibility.Visible;
                return;
            }
            ((TaskPageViewModel)DataContext).SendSelectedMethod(TasksComboBox);
            Move();
        }
    }
}
