using S7PROSIMLib;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Лифт_PLCSim.Model;

namespace Лифт_PLCSim.UControls
{
    /// <summary>
    /// Логика взаимодействия для FC_Signal.xaml
    /// </summary>
    public partial class FC_Signal : UserControl, INotifyPropertyChanged
    {

        string address = "***";
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
               
                //Парсим адрес
                try
                {
                    string adr = value.Replace("M", "").Replace("I", "").Replace("Q", "").Replace(" ", "");

                    var adr_arr = adr.Split('.');
                    Byte_CNT = int.Parse(adr_arr[0]);
                    Bit_CNT = int.Parse(adr_arr[1]);

                    address = value;
                    OnPropertyChanged("Address");
                }
                catch
                {

                }

                OnPropertyChanged("Address");
            }
        }

        string text = "***";
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged("Text");
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
            get
            {
                return ps;
            }
            set
            {
                ps = value;
            }
        }
        S7ProSim ps;

        bool state;
        public bool State
        {
            get
            {
                return state;
            }
            set
            {
                if (state != value)
                {
                    if (StateChange != null)
                    {
                        state = value;
                        StateChange();
                    }
                }
            }
        }

        private int Bit_CNT;
        private int Byte_CNT;

        //Событие - изменился статус
        public event Action StateChange;

        //Таймер
        DispatcherTimer Timer;

        public FC_Signal()
        {
            InitializeComponent();

            //Запуск таймера
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Timer.Tick += Timer_Tick;
            Timer.Start();

        }

        private bool BitState = false;

        //Тик таймера
        private void Timer_Tick(object sender, EventArgs e)
        {
            #region ЧТЕНИЕ КОМАНД ИЗ СИМУЛЯТОРА

            bool cBitState = SIMULATOR.ReadOutputBit(Byte_CNT, Bit_CNT);

            #endregion

            #region УПРАВЛЕНИЕ ЦВЕТОМ СТРЕЛКИ

            if (cBitState == true)
            {
                State = true;

                //Зеленый
                Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x0, 0x80, 0x0));
                Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0x0, 0x80, 0x0));
                Fill.Opacity = 0.2;
            }
            else
            {
                State = false;

                //Красный
                Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x0, 0x0));
                Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x0, 0x0));
                Fill.Opacity = 0.2;
            }

            if (cBitState != BitState)
            {
                this.StateChange?.Invoke();
            }

            BitState = cBitState;



            #endregion

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
