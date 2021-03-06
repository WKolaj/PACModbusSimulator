﻿using System;
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
    /// Interaction logic for FloatVariableUserControl.xaml
    /// </summary>
    public partial class UInt32VariableUserControl : UserControl
    {
        private UInt32Variable _variable;
        public UInt32Variable Variable
        {
            get
            {
                return _variable;
            }

            set
            {
                _variable = value;
                this.DataContext = value;
            }
        }

        public UInt32VariableUserControl()
        {
            InitializeComponent();
        }

        public void Init(UInt32Variable variable)
        {
            this.Variable = variable;
        }
    }
}
