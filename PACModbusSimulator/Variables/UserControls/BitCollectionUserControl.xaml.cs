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
    /// Interaction logic for BitCollectionUserControl.xaml
    /// </summary>
    public partial class BitCollectionUserControl : UserControl
    {
        public BitCollectionUserControl()
        {
            InitializeComponent();
        }

        private BitCollectionVariable _variable;
        public BitCollectionVariable Variable
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


        public void Init(BitCollectionVariable variable)
        {
            this.Variable = variable;
        }

        private void BitCheckbox0_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(0, true);
        }

        private void BitCheckbox0_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(0, false);
        }

        private void BitCheckbox1_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(1, true);
        }

        private void BitCheckbox1_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(1, false);
        }

        private void BitCheckbox2_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(2, true);
        }

        private void BitCheckbox2_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(2, false);
        }

        private void BitCheckbox3_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(3, true);
        }

        private void BitCheckbox3_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(3, false);
        }

        private void BitCheckbox4_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(4, true);
        }

        private void BitCheckbox4_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(4, false);
        }

        private void BitCheckbox5_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(5, true);
        }

        private void BitCheckbox5_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(5, false);
        }

        private void BitCheckbox6_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(6, true);
        }

        private void BitCheckbox6_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(6, false);
        }

        private void BitCheckbox7_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(7, true);
        }

        private void BitCheckbox7_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(7, false);
        }

        private void BitCheckbox8_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(8, true);
        }

        private void BitCheckbox8_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(8, false);
        }

        private void BitCheckbox9_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(9, true);
        }

        private void BitCheckbox9_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(9, false);
        }

        private void BitCheckbox10_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(10, true);
        }

        private void BitCheckbox10_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(10, false);
        }

        private void BitCheckbox11_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(11, true);
        }

        private void BitCheckbox11_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(11, false);
        }

        private void BitCheckbox12_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(12, true);
        }

        private void BitCheckbox12_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(12, false);
        }

        private void BitCheckbox13_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(13, true);
        }

        private void BitCheckbox13_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(13, false);
        }

        private void BitCheckbox14_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(14, true);
        }

        private void BitCheckbox14_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(14, false);
        }

        private void BitCheckbox15_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(15, true);
        }

        private void BitCheckbox15_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(15, false);
        }

        private void BitCheckbox16_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(16, true);
        }

        private void BitCheckbox16_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(16, false);
        }

        private void BitCheckbox17_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(17, true);
        }

        private void BitCheckbox17_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(17, false);
        }

        private void BitCheckbox18_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(18, true);
        }

        private void BitCheckbox18_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(18, false);
        }

        private void BitCheckbox19_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(19, true);
        }

        private void BitCheckbox19_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(19, false);
        }

        private void BitCheckbox20_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(20, true);
        }

        private void BitCheckbox20_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(20, false);
        }

        private void BitCheckbox21_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(21, true);
        }

        private void BitCheckbox21_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(21, false);
        }

        private void BitCheckbox22_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(22, true);
        }

        private void BitCheckbox22_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(22, false);
        }

        private void BitCheckbox23_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(23, true);
        }

        private void BitCheckbox23_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(23, false);
        }

        private void BitCheckbox24_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(24, true);
        }

        private void BitCheckbox24_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(24, false);
        }

        private void BitCheckbox25_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(25, true);
        }

        private void BitCheckbox25_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(25, false);
        }

        private void BitCheckbox26_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(26, true);
        }

        private void BitCheckbox26_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(26, false);
        }

        private void BitCheckbox27_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(27, true);
        }

        private void BitCheckbox27_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(27, false);
        }

        private void BitCheckbox28_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(28, true);
        }

        private void BitCheckbox28_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(28, false);
        }

        private void BitCheckbox29_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(29, true);
        }

        private void BitCheckbox29_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(29, false);
        }

        private void BitCheckbox30_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(30, true);
        }

        private void BitCheckbox30_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(30, false);
        }

        private void BitCheckbox31_Checked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(31, true);
        }

        private void BitCheckbox31_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Variable.SetBitValue(31, false);
        }
    }
}

