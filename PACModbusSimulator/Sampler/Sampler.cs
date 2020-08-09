using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;

namespace PACModbusSimulator
{
    public class Sampler : INotifyPropertyChanged
    {
        /// <summary>
        /// Sampler for generating tick events for meters
        /// </summary>
        /// <param name="meter">Meter connected to sampler</param>
        /// <param name="samplingTime">Sample time</param>
        public Sampler(ModbusMeterBase meter, Double samplingTime = 1000)
        {
            this.MBDevice = meter;
            this.TickTimer = new Timer(samplingTime);
            this.TickTimer.Elapsed += TickTimer_Elapsed;
        }

        /// <summary>
        /// Method invoked when timer ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TickTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime dt1970 = new DateTime(1970, 1, 1);
            DateTime current = DateTime.Now;
            TimeSpan span = current - dt1970;
            
            MBDevice.Refresh(Convert.ToInt64(span.TotalMilliseconds));
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
        private ModbusMeterBase _meter = null;
        /// <summary>
        /// Meter associated with sampler
        /// </summary>
        public ModbusMeterBase MBDevice
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

        private Boolean _started = false;
        /// <summary>
        /// Is sampler started and ticking
        /// </summary>
        public Boolean Started
        {
            get
            {
                return _started;
            }

            private set
            {
                _started = value;
                OnPropertyChanged("Started");
            }
        }

        private Timer _tickTimer;
        /// <summary>
        /// Timer of sampler
        /// </summary>
        public Timer TickTimer
        {
            get
            {
                return _tickTimer;
            }

            private set
            {
                _tickTimer = value;
            }
        }

        /// <summary>
        /// Method for starting sampler
        /// </summary>
        public void Start()
        {
            TickTimer.Start();
        }

        /// <summary>
        /// Method for stopping sampler
        /// </summary>
        public void Stop()
        {
            TickTimer.Stop();
        }

    }
}
