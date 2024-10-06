using S7PROSIMLib;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Лифт_PLCSim.Model;

namespace Лифт_PLCSim.UControls
{
    class xButton : Button, INotifyPropertyChanged
    {
        //Для реализации улавливания изменения свойства IsPressed
        PropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(xButton.IsPressedProperty, typeof(xButton));

        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(xButton), new PropertyMetadata("I0.0"));


        #region ПОЛЯ И СВОЙСТВА

        //номер бита
        private int Bit_CNT;

        //Номер байта
        private int Byte_CNT;

        /// <summary>
        /// Символьный адрес переменной
        /// </summary>
        public string Address
        {

            get
            {
                return (string)this.GetValue(AddressProperty);
            }
            set
            {
                this.SetValue(AddressProperty, value);
                ParseAddress();
            }
        }

        /// <summary>
        /// Метод для разбора символьного адреса
        /// </summary>
        private void ParseAddress()
        {
            //Парсим адрес
            try
            {
                string adr = (string)this.GetValue(AddressProperty);
                adr = Address.Replace("M", "").Replace("I", "").Replace("Q", "").Replace(" ", "");

                var adr_arr = adr.Split('.');

                Byte_CNT = int.Parse(adr_arr[0]);
                Bit_CNT = int.Parse(adr_arr[1]);

            }
            catch
            {

            }
        }

        #endregion


        private void ButtonPropertyChanged(Object sender, EventArgs e)
        {
            xButton button = (xButton)sender;
            bool value = button.IsPressed;

            //Запись значения в симулятор
            SIMULATOR.WriteInputBit(this.Byte_CNT, Bit_CNT, value);

        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public xButton()
        {
            descriptor.AddValueChanged(this, new EventHandler(ButtonPropertyChanged));

            this.Loaded += (s, e) =>
            {
                ParseAddress();
            };
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
