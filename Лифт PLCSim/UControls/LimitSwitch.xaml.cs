using S7PROSIMLib;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Лифт_PLCSim.Model;

namespace Лифт_PLCSim.UControls
{
    /// <summary>
    /// Логика взаимодействия для LimitSwitch.xaml
    /// </summary>
    public partial class LimitSwitch : UserControl, INotifyPropertyChanged
    {
        //Что концевик активирован
        public double Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                Update();
            }
        }
        double position;

        private int Bit_CNT;
        private int Byte_CNT;

        public double PositionActive
        {
            get
            {
                return positionActive;
            }
            set
            {
                positionActive = value;
                OnPropertyChanged("PositionActive");
            }

        }
        double positionActive;

        public double Delta
        {
            get
            {
                return delta;
            }
            set
            {
                delta = value;
                OnPropertyChanged("Delta");
            }

        }
        double delta = 10;

        public bool State
        {
            set
            {
                if (value == true)
                {
                    Fill = TRUE_Fill;
                    WritePLCSim(true);
                }
                else
                {
                    Fill = FALSE_Fill;
                    WritePLCSim(false);
                }
            }

        }

        public Brush TRUE_Fill
        {
            get
            {
                return tfill;
            }
            set
            {
                tfill = value;
                OnPropertyChanged("TRUE_Fill");
            }
        }
        Brush tfill = Brushes.Green;

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
        Brush fill = Brushes.Transparent;

        public Brush FALSE_Fill
        {
            get
            {
                return ffill;
            }
            set
            {
                ffill = value;
                OnPropertyChanged("FALSE_Fill");
            }
        }
        Brush ffill = Brushes.Transparent;

        public Brush Color
        {
            get
            {
                return textcolor;
            }
            set
            {
                textcolor = value;
                OnPropertyChanged("Color");
            }
        }
        Brush textcolor = Brushes.White;

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
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
                
            }
        }
        string address = "****";

        public LimitSwitch()
        {
            InitializeComponent();
            DataContext = this;
        }

        void Update()
        {
            //Статус концевого выключателя
            object status = false;

            //Проверяем пазицию
            if (Position > PositionActive - Delta && Position < PositionActive + Delta)
            {
                Fill = TRUE_Fill;
                WritePLCSim(true);
            }
            else
            {
                Fill = FALSE_Fill;
                WritePLCSim(false);
            }
        }

        //Запись в симулятор
        void WritePLCSim(bool value)
        {
            //Записываем позиции в симулятор
            SIMULATOR.WriteInputBit(Byte_CNT, Bit_CNT, value);
        }

        private static void CurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var position = ((LimitSwitch)d).Position;
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
