using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using SciChart.Charting;
using SciChart.Charting3D;
using SciChart.Charting3D.RenderableSeries;
using ScottPlot;
using ScottPlot.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Optimization.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private bool isDark = false;
        [ObservableProperty]
        private string? theme = "Light";
        public void ThemeChange(ScottPlot.WPF.WpfPlotBase plot, ScottPlot.Panels.ColorBar? bar, SciChart3DSurface chart)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            string lightColor = "#f1f1f1";
            string darkColor = "#323232";
            string lightDarkColor = "#8f8f8f";
            string darkLightColor = "#858585";
            if (isDark)
            {
                theme.SetLightTheme();
                isDark = false;
                Theme = "Light";
                
                plot.Plot.FigureBackground.Color = ScottPlot.Color.FromHex(lightColor);
                plot!.Plot.Axes.Color(Color.FromHex(darkColor));
                plot.Plot.Grid.MajorLineColor = Color.FromHex(lightDarkColor);
                if (bar is not null)
                    bar.Axis.TickLabelStyle.ForeColor = ScottPlot.Color.FromHex(darkColor);
                ThemeManager.SetTheme(chart, "BrightSpark");
            }
            else
            {
                theme.SetDarkTheme();
                isDark = true;
                Theme = "Dark";
                plot.Plot.FigureBackground.Color = ScottPlot.Color.FromHex(darkColor);
                plot!.Plot.Axes.Color(Color.FromHex(lightColor));
                plot.Plot.Grid.MajorLineColor = Color.FromHex(darkLightColor);
                if (bar is not null)
                    bar.Axis.TickLabelStyle.ForeColor = ScottPlot.Color.FromHex(lightColor);
                ThemeManager.SetTheme(chart, "ExpressionDark");
            }
            paletteHelper.SetTheme(theme);
        }
    }
}
