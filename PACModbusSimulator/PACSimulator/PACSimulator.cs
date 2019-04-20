using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace PACModbusSimulator
{
    public class PACSimulator : INotifyPropertyChanged
    {
        /// <summary>
        /// Class representing simulator
        /// </summary>
        public PACSimulator()
        {

        }

        /// <summary>
        /// XML settings file name
        /// </summary>
        public const String xmlFileName = "Settings.xml";

        /// <summary>
        /// String representing PAC3200 meter
        /// </summary>
        public const String pac3200String = "PAC3200";

        /// <summary>
        /// Method for initializing PACSimulator
        /// </summary>
        public void Init()
        {
            this.ReadXMLFile();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<MeterBase> _allMeters = new ObservableCollection<MeterBase>();
        /// <summary>
        /// All meters associated with Simulator
        /// </summary>
        public ObservableCollection<MeterBase> AllMeters
        {
            get
            {
                return _allMeters;
            }

            private set
            {
                this._allMeters = value;
                OnPropertyChanged("AllMeters");
            }
        }

        private MeterBase _currentMeter;
        /// <summary>
        /// Current meter selected by ListBox
        /// </summary>
        public MeterBase CurrentMeter
        {
            get
            {
                return _currentMeter;
            }

            set
            {
                this._currentMeter = value;
                OnPropertyChanged("CurrentMeter");
            }
        }

        /// <summary>
        /// Method for creating new PAC3200 meter and adding it to AllMeters
        /// </summary>
        /// <param name="name">Name of device</param>
        /// <param name="portNumber">Port number</param>
        /// <param name="nominalCurrent">Nominal current</param>
        /// <param name="nominialPowerFactor">Nominal power factor</param>
        public void CreatePAC3200Meter(string name, int portNumber, Single nominalCurrent, Single nominialPowerFactor)
        {
            AddMeter(new PAC3200(this,name,portNumber, nominalCurrent ,nominialPowerFactor));
        }

        /// <summary>
        /// Method for adding new meter to AllMeters
        /// </summary>
        /// <param name="meter">new meter to be added</param>
        public void AddMeter(MeterBase meter)
        {
            AllMeters.Add(meter);
            meter.MeterSettingsChanged += OnMeterSettingsChanged;
            SaveToXMLFile();
        }

        /// <summary>
        /// Method invoked on one of meters settings change
        /// </summary>
        private void OnMeterSettingsChanged()
        {
            SaveToXMLFile();
        }

        /// <summary>
        /// Method for saving all settings to XML file
        /// </summary>
        private void SaveToXMLFile()
        {
            try
            {
                XElement rootElement = GetXMLElement();
                if (rootElement != null)
                {
                    XDocument xDocument = new XDocument(rootElement);
                    xDocument.Save(xmlFileName);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error during saving XML file", error.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method for reading all data from XML file and setting Simulator according to XML file content
        /// </summary>
        private void ReadXMLFile()
        {
            try
            {
                if(File.Exists(xmlFileName))
                {
                    XDocument doc = XDocument.Load(xmlFileName);

                    this.SetFromXML(doc.Root);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error during reading XML file", error.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method for removing meter
        /// </summary>
        /// <param name="meter">
        /// Meter to remove
        /// </param>
        public void RemoveMeter(MeterBase meter)
        {
            //Stopping meter if started
            if(meter.IsRunning)
            {
                meter.Stop();
            }

            AllMeters.Remove(meter);
            meter.MeterSettingsChanged -= OnMeterSettingsChanged;
            SaveToXMLFile();
        }

        /// <summary>
        /// Method for checking if port number of new meter is uniq
        /// </summary>
        /// <param name="portNumber">portNumber to check</param>
        /// <returns>is portNumber uniq</returns>
        public Boolean CheckNewPortNumber(int portNumber)
        {
            foreach(var meter in AllMeters)
            {
                if(meter.PortNumber == portNumber)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Method for showing meter window
        /// </summary>
        /// <param name="meter"></param>
        public void ShowPacWindow(MeterBase meter)
        {
            PACWindow window = new PACWindow();
            window.Init(meter);
            window.Show();
        }

        /// <summary>
        /// Method for starting meter
        /// </summary>
        /// <param name="meter">
        /// Meter to start
        /// </param>
        public void StartMeter(MeterBase meter)
        {
                meter.Start();
        }

        /// <summary>
        /// Method for stopping meter
        /// </summary>
        /// <param name="meter"></param>
        public void StopMeter(MeterBase meter)
        {
            meter.Stop();
        }

        /// <summary>
        /// Method for setting simulator according to XML content
        /// </summary>
        /// <param name="mainElement">XML element</param>
        public void SetFromXML(XElement mainElement)
        {
            if (mainElement.Name != "PACSimulator")
                throw new Exception("Invalid name of XML file");

            //Clearing all possible selections and seleted elements
            this.AllMeters.Clear();
            this.CurrentMeter = null;

            foreach(var element in mainElement.Elements())
            {
                if(element.Name == PAC3200.TypeString)
                {
                    var newPAC3200 = new PAC3200(this);
                    newPAC3200.SetFromXML(element);

                    AddMeter(newPAC3200);
                }
            }
        }

        /// <summary>
        /// Method for generating content of XML File, storing all settings of simulator
        /// </summary>
        /// <returns>
        /// XML content for storing all settings of simulator
        /// </returns>
        public XElement GetXMLElement()
        {
            XElement mainElement = new XElement("PACSimulator");

            foreach(var meter in AllMeters)
            {
                mainElement.Add(meter.GetXMLElement());
            }

            return mainElement;
        }

    }

}
