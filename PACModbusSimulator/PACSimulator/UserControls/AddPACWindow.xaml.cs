using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PACModbusSimulator
{
    /// <summary>
    /// Interaction logic for AddPACWindow.xaml
    /// </summary>
    public partial class AddPACWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        
        private List<String> _allPACTypes;
        /// <summary>
        /// Collection of all meter types
        /// </summary>
        public List<String> AllPACTypes
        {
            get
            {
                return _allPACTypes;
            }

            private set
            {
                this._allPACTypes = value;
            }
        }

        private String _selectedPACType;
        /// <summary>
        /// Selected meter type
        /// </summary>
        public String SelectedPACType
        {
            get
            {
                return _selectedPACType;
            }

            set
            {
                this._selectedPACType = value;
                OnPropertyChanged("SelectedPACType");
                OnPropertyChanged("PACName");
            }
        }

        private PACSimulator _simulator;
        /// <summary>
        /// Simulator object
        /// </summary>
        public PACSimulator Simulator
        {
            get
            {
                return _simulator;
            }

            private set
            {
                _simulator = value;
            }
        }

        private String _name;
        /// <summary>
        /// Name of device to be added
        /// </summary>
        public String PACName
        {
            get
            {
                return _name;
            }

            set
            {
                this._name = value;
                OnPropertyChanged("PACName");
                OnPropertyChanged("Ready");
            }
        }

        private Int32 _portNumber;
        /// <summary>
        /// Port number of device to be added
        /// </summary>
        public Int32 PortNumber
        {
            get
            {
                return _portNumber;
            }

            set
            {
                this._portNumber = value;
                OnPropertyChanged("PortNumber");
                OnPropertyChanged("Ready");
            }
        }

        private Single _nominalCurrent;
        /// <summary>
        /// Nominal current of device to be added
        /// </summary>
        public Single NominalCurrent
        {
            get
            {
                return _nominalCurrent;
            }

            set
            {
                this._nominalCurrent = value;
                OnPropertyChanged("NominalCurrent");
                OnPropertyChanged("Ready");
            }
        }

        private Single _nominalPowerFactor;
        /// <summary>
        /// Nominal power factor of device
        /// </summary>
        public Single NominalPowerFactor
        {
            get
            {
                return _nominalPowerFactor;
            }

            set
            {
                this._nominalPowerFactor = value;
                OnPropertyChanged("NominalPowerFactor");
                OnPropertyChanged("Ready");
            }
        }

        /// <summary>
        /// Are settings ready to be added
        /// </summary>
        public Boolean Ready
        {
            get
            {
                return CheckValues();
            }
        }

        /// <summary>
        /// Window for adding new device
        /// </summary>
        public AddPACWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method for initializing window
        /// </summary>
        /// <param name="simulator">
        /// Simulator connected with window
        /// </param>
        public void Init(PACSimulator simulator)
        {
            this.Simulator = simulator;

            this.AllPACTypes = new List<string>()
            {
                PACSimulator.pac3200String,
                PACSimulator.pac3220String,
                PACSimulator.pac4200String
            };

            this.SelectedPACType = AllPACTypes.First();

            this.DataContext = this;
        }

        /// <summary>
        /// Method for checking if all values given by the user are correct
        /// </summary>
        /// <returns>
        /// Are all settings correct
        /// </returns>
        private Boolean CheckValues()
        {
            if (String.IsNullOrEmpty(PACName)) return false;
            if (PortNumber <= 0) return false;
            if (!Simulator.CheckNewPortNumber(PortNumber)) return false;
            if (NominalCurrent <= 0) return false;
            if (NominalPowerFactor <= 0) return false;

            return true;
        }

        /// <summary>
        /// Method invoked when cancel button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Method invoked when add button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Once again checking if everyhing is ok
            if (CheckValues())
            {
                DialogResult = true;
            }
        }
    }
}
