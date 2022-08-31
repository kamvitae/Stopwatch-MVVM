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
using HF_16_MVVM_Stopwatch.ViewModel;

namespace HF_16_MVVM_Stopwatch.View
{
    /// <summary>
    /// Interaction logic for BasicStopwatch.xaml
    /// </summary>
    public partial class BasicStopwatch : UserControl
    {
        public StopwatchViewModel viewModel;
        public BasicStopwatch()
        {
            InitializeComponent();
            viewModel = FindResource("viewModel") as StopwatchViewModel;
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
