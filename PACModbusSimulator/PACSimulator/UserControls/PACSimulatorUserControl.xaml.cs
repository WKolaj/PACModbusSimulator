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
    /// Interaction logic for PAC3200SimulatorUserControl.xaml
    /// </summary>
    public partial class PACSimulatorUserControl : UserControl
    {
        private PACSimulator _simualtor;
        /// <summary>
        /// Simulator object
        /// </summary>
        public PACSimulator Simulator
        {
            get
            {
                return _simualtor;
            }

            private set
            {
                this._simualtor = value;
                this.DataContext = value;
            }
        }

        /// <summary>
        /// User control for representing PAC simulator
        /// </summary>
        public PACSimulatorUserControl()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Method for initialzing user control
        /// </summary>
        private void Init()
        {
            this.Simulator = new PACSimulator();
            this.Simulator.Init();
        }

        /// <summary>
        /// Method invoked when Add button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                AddPACWindow window = new AddPACWindow();
                window.Init(this.Simulator);

                if ((Boolean)window.ShowDialog())
                {
                    CreateMeter(window.SelectedPACType, window.PACName, window.PortNumber, window.NominalCurrent, window.NominalPowerFactor);
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("Error while adding", err.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method for creating new meter based on addPACWindow parameters
        /// </summary>
        /// <param name="type">Type of device to be added</param>
        /// <param name="name">Name of device</param>
        /// <param name="portNumber">Port number of device</param>
        /// <param name="nominalCurrent">Nominal current of device</param>
        /// <param name="nominalPowerFactor">Nominal power factor of device</param>
        private void CreateMeter(String type, String name, Int32 portNumber, Single nominalCurrent, Single nominalPowerFactor)
        {
            switch (type)
            {
                case PACSimulator.pac3200String:
                    {
                        Simulator.CreatePAC3200Meter(name, portNumber, nominalCurrent, nominalPowerFactor);
                        break;
                    }
                case PACSimulator.pac3220String:
                    {
                        Simulator.CreatePAC3220Meter(name, portNumber, nominalCurrent, nominalPowerFactor);
                        break;
                    }
                case PACSimulator.pac4200String:
                    {
                        Simulator.CreatePAC4200Meter(name, portNumber, nominalCurrent, nominalPowerFactor);
                        break;
                    }
            }
        }

        /// <summary>
        ///  Method invoked when remove button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Simulator.CurrentDevice != null)
                {
                    Simulator.RemoveDevice(Simulator.CurrentDevice);
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("Error while removing", err.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method invoked when start button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = (Button)sender;

                var meter = (MeterBase)button.DataContext;

                meter.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error while starting", err.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method invoked when stop button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = (Button)sender;

                var meter = (MeterBase)button.DataContext;

                meter.Stop();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error while starting", err.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method invokes when list box id doubleclicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CenterListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Simulator.CurrentDevice != null)
                {
                    PACWindow window = new PACWindow();
                    window.Init(Simulator.CurrentDevice);

                    window.ShowDialog();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error while opening window", err.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
