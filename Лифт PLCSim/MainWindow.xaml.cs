using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;
using S7PROSIMLib;
using System.ComponentModel;
using Лифт_PLCSim.Model;

namespace Лифт_PLCSim
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        //Позиция лифта
        public double LiftPosition
        {
            get
            {
                return liftPosition;
            }
            set
            {
                liftPosition = value;

                //Ограничения
                if (value > 514) liftPosition = 514;
                if (value < 0) liftPosition = 0;

                //Передаем позицию в концевик
                LS_2_0.Position = LS_2_1.Position = LS_2_2.Position = liftPosition;

                //Пересчитываем высоту в значение энкодера
                PIW256 = (short)(LiftPosition * 53);

                //Извещаем модель представления
                OnPropertyChanged("LiftPosition");
            }
        }
        double liftPosition = 36;

        //Значение энкодера положения кабины лифта
        public short PIW256
        {
            get
            {
                return piw256;
            }
            set
            {
                piw256 = value;

                SIMULATOR.WriteInputWord(256, piw256);

                OnPropertyChanged("PIW256");
            }
        }
        short piw256 = 0;

        public MainWindow()
        {
            InitializeComponent();
        }


        //Подключение к симулятору
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            SIMULATOR.PLCSim.Connect();
            SIMULATOR.PLCSim.SetState("RUN");
            SIMULATOR.PLCSim.SetScanMode(ScanModeConstants.ContinuousScan);
        }


        private void Step7_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("S7tgtopx.exe");
        }

        private void PLCSim_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("s7wsvapx.exe");
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
