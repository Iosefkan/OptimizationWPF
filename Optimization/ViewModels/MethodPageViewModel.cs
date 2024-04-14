using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Optimization.Messeges;
using Optimization.Methods;
using Optimization.Tasks;
using SciChart.Core.Extensions;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Optimization.ViewModels
{
    public partial class MethodPageViewModel : ObservableObject, IRecipient<SelectedTaskMessage>
    {
        [ObservableProperty]
        private BoxMethod boxMethod = new();
        [ObservableProperty]
        private double t1 = 0;
        [ObservableProperty]
        private double t2 = 0;
        [ObservableProperty]
        private double res = 0;
        [ObservableProperty]
        private double compQuan = 0;
        public MethodPageViewModel()
        {
            WeakReferenceMessenger.Default.Register<SelectedTaskMessage>(this);
        }

        public void Receive(SelectedTaskMessage message)
        {
            BoxMethod.Task = message.Value;
        }
        public void ClearResult()
        {
            T1 = 0;
            T2 = 0;
            Res = 0;
            CompQuan = 0;
        }
        public void ClickSolve()
        {
            try
            {
                (T1, T2, CompQuan, Res) = BoxMethod.Solve();
            }
            catch
            {
                throw new Exception();
            }
        }
        public (double[,], double[], double[], double[], double[]) GetHeatmap(int n)
        {
            ITask task = BoxMethod.Task!;
            double[,] data = new double[n + 1, n + 1];
            double stepT1 = (task.T1To - task.T1From) / (n + 1);
            double stepT2 = (task.T2To - task.T2From) / (n + 1);
            double[] T1Heat = Generate.Consecutive(n + 1, stepT1, task.T1From);
            double[] T2Heat = Generate.Consecutive(n + 1, stepT2, task.T2From);
            for (int x = 0; x < (n + 1); x++)
            {
                for (int y = 0; y < (n + 1); y++)
                {
                    data[x, y] = task.Equation(T1Heat[x], T2Heat[y]);
                }
            }
            double[] xs = new double[] { T1 };
            double[] ys = new double[] { T2 };
            //using (FileStream fs = File.Create("scores.txt")) //Creates Scores.txt
            //{
            //    for (int x = 0; x < n + 1; x++)
            //    {
            //        for (int y = 0; y < n + 1; y++)
            //        {
            //            fs.Write(Encoding.ASCII.GetBytes(data[x, y].ToString() + "  "));
            //        }
            //        fs.Write(Encoding.ASCII.GetBytes("\n"));
            //    }
            //}
            return (data, T1Heat, T2Heat, xs, ys);
        }
    }
}
