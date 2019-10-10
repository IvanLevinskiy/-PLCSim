using S7PROSIMLib;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Лифт_PLCSim.Model;

namespace Лифт_PLCSim.UControls
{
    /// <summary>
    /// Логика взаимодействия для Door.xaml
    /// </summary>
    public partial class Door : UserControl, INotifyPropertyChanged
    {
        //Главный таймер
        DispatcherTimer timer = new DispatcherTimer();

        //Анимация
        DoubleAnimation da = new DoubleAnimation();

        //Тэги для приводов
        public PLCSimTag DRIVE_UP
        {
            get
            {
                return _DRIVE_UP;
            }
            set
            {
                _DRIVE_UP = value;
            }
        }
        PLCSimTag _DRIVE_UP;

        public PLCSimTag DRIVE_DOWN
        {
            get
            {
                return _DRIVE_DOWN;
            }
            set
            {
                _DRIVE_DOWN = value;
            }
        }
        PLCSimTag _DRIVE_DOWN;

        public string LS_UP_ADDR
        {
            get
            {
                return ls_up;
            }
            set
            {
                ls_up = value;
                LS_UP.Address = value;
            }
        }
        string ls_up;

        public string LS_DOWN_ADDR
        {
            get
            {
                return ls_down;
            }
            set
            {
                ls_down = value;
                LS_DOWN.Address = value;
            }
        }
        string ls_down;

        //Команда на поднимание двери
        bool UP_CMD
        {
            get
            {
                return up_cmd;
            }
            set
            {
                if (value == true && up_cmd != value && down_cmd == false)
                {
                    double time = (Rect.ActualHeight - 25) / 37.5;
                    da.From = Rect.ActualHeight;
                    da.To = 25;
                    da.Duration = TimeSpan.FromSeconds(time);

                    Rect.BeginAnimation(Rectangle.HeightProperty, da);
                }

                if (value == false && up_cmd != value)
                {
                    StopAnimation();
                }
                
                up_cmd = value;
            }
        }
        bool up_cmd;

        //Команда на опускание двери
        bool DOWN_CMD
        {
            get
            {
                return down_cmd;
            }
            set
            {
                //Если нет команды на поднимание , но есть команда на опускание
                if (value == true && down_cmd != value && up_cmd == false)
                {
                    double time = (100 - Rect.ActualHeight) / 37.5;

                    da.From = Rect.ActualHeight;
                    da.To = 100;
                    da.Duration = TimeSpan.FromSeconds(time);

                    Rect.BeginAnimation(Rectangle.HeightProperty, da);
                }

                if (value == false && down_cmd != value)
                {
                    StopAnimation();
                }

                down_cmd = value;
            }
        }
        bool down_cmd;

        #region КОНСТРУКТОР
        public Door()
        {
            InitializeComponent();
            DataContext = this;

            //Зажигаем концевик, так как приложение запускается когда дверь находится в 
            //нижнем положении
            LS_DOWN.State = true;

            //Запуск задачи
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        #endregion

        //ТАЙМЕР
        private void Timer_Tick(object sender, EventArgs e)
        {
            #region ЧТЕНИЕ КОМАНД ИЗ СИМУЛЯТОРА

            UP_CMD = SIMULATOR.ReadOutputBit(DRIVE_UP.Byte, DRIVE_UP.Bit);

            //Чтение комманды для опускания двери
            DOWN_CMD = SIMULATOR.ReadOutputBit(DRIVE_DOWN.Byte, DRIVE_DOWN.Bit);

            #endregion

            #region УПРАВЛЕНИЕ ВЕРХНИМ И НИЖНЕМ ИНДИКАТОРАМИ

            LS_DOWN.State = !(Rect.Height < 99.0);
            LS_UP.State = !(Rect.Height > 26.0);

            #endregion
        }

        //остановка анимации
        void StopAnimation()
        {
            da.From = Rect.ActualHeight;
            da.To = Rect.ActualHeight;
            da.Duration = TimeSpan.FromSeconds(0.001);

            Rect.BeginAnimation(Rectangle.HeightProperty, da);
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
