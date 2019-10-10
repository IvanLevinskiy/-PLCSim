using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Лифт_PLCSim.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {

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
                if (value > 560) liftPosition = 560;
                if (value < 65)  liftPosition = 65;

                OnPropertyChanged("LiftPosition");
            }
        }
        double liftPosition = 65;

        #region Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
