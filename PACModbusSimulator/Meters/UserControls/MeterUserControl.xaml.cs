using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PACModbusSimulator
{
    /// <summary>
    /// Interaction logic for PAC3200UserControl.xaml
    /// </summary>
    public partial class MeterUserControl : UserControl
    {
        public MeterUserControl()
        {
            InitializeComponent();
        }


        public void Init(StandardPAC meter)
        {
            this.Meter = meter;
            this.DataContext = Meter;
            foreach(var variable in Meter.Variables.Values)
            {
                var ucToAdd = variable.GetUserControl();
                ucToAdd.Margin = new Thickness(5);

                this.centerWrapPanel.Children.Add(ucToAdd);
            }
        }

        private StandardPAC _meter;
        public StandardPAC Meter
        {
            get
            {
                return _meter;
            }

            private set
            {
                _meter = value;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Meter.Start();
            }
            catch(Exception error)
            {
                MessageBox.Show("Error during starting", error.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Meter.Stop();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error during stopping", error.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
