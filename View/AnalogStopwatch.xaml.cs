using System;
using System.Collections.Generic;
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

namespace HF_16_MVVM_Stopwatch.View
{
    /// <summary>
    /// Interaction logic for AnalogStopwatch.xaml
    /// </summary>
    public sealed partial class AnalogStopwatch : UserControl
    {
        ViewModel.StopwatchViewModel viewModel;
        public AnalogStopwatch()
        {
            InitializeComponent();
            viewModel = FindResource("viewModel") as ViewModel.StopwatchViewModel;
            AddMarkings();
        }

        private void AddMarkings()
        {
            for (int i = 0; i < 360; i += 3)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = (i % 30 == 0) ? 3 : 1;
                rectangle.Height = 12;
                rectangle.Fill = new SolidColorBrush(Colors.Black);
                rectangle.RenderTransformOrigin = new Point(0.5, 0.5);

                TransformGroup transforms = new TransformGroup();
                transforms.Children.Add(new TranslateTransform() { Y = -140 });
                transforms.Children.Add(new RotateTransform() { Angle = i });
                rectangle.RenderTransform = transforms;
                baseGrid.Children.Add(rectangle);

            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Stop();
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Reset();
        }
        private void LapButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Lap();
        }
    }
}
