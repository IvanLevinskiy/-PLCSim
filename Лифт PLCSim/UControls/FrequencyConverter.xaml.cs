using S7PROSIMLib;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Лифт_PLCSim.UControls
{
    /// <summary>
    /// Логика взаимодействия для FrequencyConverter.xaml
    /// </summary>
    public partial class FrequencyConverter : UserControl, INotifyPropertyChanged
    {
        bool state;

        public bool State
        {
            get
            {
                return state;
            }
            set
            {
                if (value == true)
                {
                    Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x0, 0x80, 0x0));
                    Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0x0, 0x80, 0x0));
                    Fill.Opacity = 0.2;
                }
                else
                {
                    Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x0, 0x0));
                    Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x0, 0x0));
                    Fill.Opacity = 0.2;
                }

                state = value;
                OnPropertyChanged("State");
            }
        }

        Brush fill = Brushes.Transparent;
        public Brush Fill
        {
            get
            {
                return fill;
            }
            set
            {
                fill = value;
                OnPropertyChanged("Fill");
            }
        }

        Brush stroke = Brushes.White;
        public Brush Stroke
        {
            get
            {
                return stroke;
            }
            set
            {
                stroke = value;
                OnPropertyChanged("Stroke");
            }
        }



        public S7ProSim PLCSim
        {
            set
            {
                S1.PLCSim = value;
                S1.StateChange += Signals_StateChange;

                S2.PLCSim = value;
                S2.StateChange += Signals_StateChange;

                S3.PLCSim = value;
                S3.StateChange += Signals_StateChange;

                S4.PLCSim = value;
                S4.StateChange += Signals_StateChange;

                S5.PLCSim = value;
                S5.StateChange += Signals_StateChange;

                S6.PLCSim = value;
                S6.StateChange += Signals_StateChange;

            }
        }

        private void Signals_StateChange()
        {
            State = S1.State && S2.State && S3.State && S4.State && S5.State && S6.State;

            if(State == false)
            {
                Stroke = Brushes.Red;
                return;
            }

            Stroke = Brushes.Green;

        }

        public FrequencyConverter()
        {
            InitializeComponent();

            S1.StateChange += Signals_StateChange;
            S2.StateChange += Signals_StateChange;
            S3.StateChange += Signals_StateChange;
            S4.StateChange += Signals_StateChange;
            S5.StateChange += Signals_StateChange;
            S6.StateChange += Signals_StateChange;

            Signals_StateChange();
        }


        #region Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
