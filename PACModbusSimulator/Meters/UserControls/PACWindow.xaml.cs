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
using System.Windows.Shapes;

namespace PACModbusSimulator
{
    /// <summary>
    /// Interaction logic for PAC3200Window.xaml
    /// </summary>
    public partial class PACWindow : Window
    {
        private MeterBase _meter;
        public MeterBase Meter
        {
            get
            {
                return _meter;
            }

            private set
            {
                this._meter = value;
            }
        }

        private UserControl _meterUserControl;
        public UserControl MeterUserControl
        {
            get
            {
                return _meterUserControl;
            }

            private set
            {
                this._meterUserControl = value;
            }
        }

        public PACWindow()
        {
            InitializeComponent();
        }

        public void Init(MeterBase meter)
        {
            this.Meter = meter;

            MeterUserControl = this.Meter.GetUserControl();
            this.mainBorder.Child = MeterUserControl;

            this.DataContext = this.Meter;
        }
    }
}
