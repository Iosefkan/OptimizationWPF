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
using Optimization.Methods;
using Optimization.ViewModels;
using SciChart.Charting.Model.DataSeries.Heatmap2DArrayDataSeries;
using SciChart.Charting.Model.DataSeries;
using ScottPlot;
using System.Data;
using Optimization.Tasks;
using MaterialDesignThemes.Wpf;
using ScottPlot.Panels;
using SciChart.Charting3D.Model;
using System.Drawing.Imaging;
using SciChart.Charting3D.RenderableSeries;
using ScottPlot.Statistics;
using SciChart.Charting3D;
using SciChart.Charting;

namespace Optimization.Pages
{
    /// <summary>
    /// Interaction logic for MethodPage.xaml
    /// </summary>
    public partial class MethodPage : Page
    {
        private Action Move {  get; set; }
        private ScottPlot.Plottables.Heatmap? Heatmap { get; set; }
        public ScottPlot.Panels.ColorBar? ColorBar { get; set; }
        private ScottPlot.Plottables.Scatter? Scatter { get; set; }
        public SurfaceMeshRenderableSeries3D? Series3D { get; set; }
        public MethodPage(Action moveBack)
        {
            InitializeComponent();
            Move = moveBack;
            DataContext = new MethodPageViewModel();
            Solve.IsEnabled = false;
            MethodParams.IsEnabled = false;
            MethodRes.IsEnabled = false;
            ErrorNotice.Visibility = Visibility.Hidden;
            HeatPlot.Plot.Axes.Bottom.Label.Text = "T2, °C";
            HeatPlot.Plot.Axes.Left.Label.Text = "T1, °C";
            HeatPlot.Plot.Axes.Title.Label.Text = "График поверхности целевой функции";
            HeatPlot.Plot.FigureBackground.Color = ScottPlot.Color.FromHex("#f1f1f1");
            ThemeManager.SetTheme(sciChart, "BrightSpark");
        }
        private void ToTaskButton_Click(object sender, RoutedEventArgs e)
        {
            HeatPlot.Plot.Clear();
            HeatPlot.Plot.Remove(Heatmap);
            HeatPlot.Plot.Remove(ColorBar);
            HeatPlot.Plot.Remove(Scatter);
            HeatPlot.Refresh();
            sciChart.RenderableSeries.Remove(Series3D);
            ((MethodPageViewModel)DataContext).ClearResult();
            Move();
        }

        private void MethodsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MethodParams.IsEnabled = true;
        }

        private ExtremumType GetEnum(int ind)
        {
            
            ExtremumType type;
            if (ind == 0)
                type = ExtremumType.Minimum;
            else
                type = ExtremumType.Maximum;
            return type;
        }
        private void ExtrComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((MethodPageViewModel)DataContext).BoxMethod.Extremum = GetEnum(ExtrComboBox.SelectedIndex);
            Solve.IsEnabled = true;
            MethodRes.IsEnabled = true;
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            if (!Validator.IsValid(this))
            {
                ErrorNotice.Visibility = Visibility.Visible;
                return;
            }
            ErrorNotice.Visibility = Visibility.Hidden;
            try
            {
                
                ((MethodPageViewModel)DataContext).ClickSolve();
                int pointCount = 500;
                (double[,] imageData, double[] T1Steps, double[] T2Steps, double[] xs, double[] ys) = ((MethodPageViewModel)DataContext).GetHeatmap(pointCount);
                AddHeatmap(imageData, ys, xs);
                AddSeries3D(pointCount, T1Steps, T2Steps, imageData);
            }
            catch
            {
                MessageBox.Show("Невозможно решить задачу с введенной кофигурацией", "Ошибка", MessageBoxButton.OK, icon: MessageBoxImage.Error);
            }
        }
        private void AddHeatmap(double[,] imageData, double[] ys, double[] xs)
        {
            HeatPlot.Plot.Clear();
            HeatPlot.Plot.Remove(Heatmap!);
            HeatPlot.Plot.Remove(ColorBar!);
            HeatPlot.Plot.Remove(Scatter!);
            Heatmap = HeatPlot.Plot.Add.Heatmap(imageData);
            ITask task = ((MethodPageViewModel)DataContext).BoxMethod.Task!;
            HeatPlot.Plot.Axes.SetLimits(task.T2From, task.T2To, task.T1From, task.T1To);
            Heatmap.Extent = new CoordinateRect(task.T2From, task.T2To, task.T1From, task.T1To);
            Heatmap.Colormap = new ScottPlot.Colormaps.Turbo();
            Heatmap.FlipVertically = true;
            ColorBar = HeatPlot.Plot.Add.ColorBar(Heatmap);
            HeatPlot.Refresh();
            Scatter = HeatPlot.Plot.Add.ScatterPoints(ys, xs);
            Scatter.MarkerSize = 10;
            Scatter.Color = ScottPlot.Colors.White;
            
        }
        private void AddSeries3D(int pointCount, double[] T1Steps, double[] T2Steps, double[,] imageData)
        {
            sciChart.RenderableSeries.Remove(Series3D);
            var meshDataSeries = new NonUniformGridDataSeries3D<double>(pointCount, pointCount, xIndex => T2Steps[xIndex], yIndex => T1Steps[yIndex])
            {
                SeriesName = "Nonuniform Surface Mesh",
            };
            for (int z = 0; z < pointCount; z++)
            {
                for (int x = 0; x < pointCount; x++)
                {
                    meshDataSeries[z, x] = imageData[z, x];
                }
            }
            Series3D = new SurfaceMeshRenderableSeries3D();
            Series3D.DrawMeshAs = DrawMeshAs.SolidWithContours;
            Series3D.Stroke = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#77228B22");
            Series3D.ContourStroke = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#77228B22");
            Series3D.StrokeThickness = 2;
            Series3D.Opacity = 0.9;
            Series3D.DrawSkirt = false;
            Series3D.DataSeries = meshDataSeries;
            Series3D.MeshColorPalette = new GradientColorPalette()
            {
                GradientStops = new System.Collections.ObjectModel.ObservableCollection<GradientStop>
                {
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#03045e"), 0),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#0077b6"), 0.1),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#00b4d8"), 0.2),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#90e0ef"), 0.3),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#b5e48c"), 0.4),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#d9ed92"), 0.5),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#ee9b00"), 0.6),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#ca6702"), 0.7),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#bb3e03"), 0.8),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#ae2012"), 0.9),
                    new GradientStop((System.Windows.Media.Color)ColorConverter.ConvertFromString("#9b2226"), 1),

                },
                IsStepped = false,
            };
            Series3D.MeshPaletteMode = MeshPaletteMode.HeightMapInterpolated;
            sciChart.RenderableSeries.Add(Series3D);
        }
    }
}
