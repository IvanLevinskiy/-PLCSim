using System.ComponentModel;

namespace Лифт_PLCSim.Model
{
    [TypeConverter(typeof(PLCSimTagConverter))]
    public class PLCSimTag : INotifyPropertyChanged
    {
        public PLCSimTag(string addr)
        {
            this.Address = addr;
            Parse();
        }

        public string Name
        {
            get;
            set;
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged("Address");
               
            }
        }
        string address;

        public int Bit
        {
            get;
            set;
        }
        public int Byte
        {
            get;
            set;
        }

        //M0.0
        //Парсим адрес  PS.ReadOutputPoint(0, 0, S7PROSIMLib.PointDataTypeConstants.S7_Bit, ref val);
        void Parse()
        {
            if (address.Contains("."))
            {
                Type = S7PROSIMLib.PointDataTypeConstants.S7_Bit;

                string adr = address.Replace("M", "").Replace("I", "").Replace("Q", "").Replace(" ", "");

                var adr_arr = adr.Split('.');
                Byte = int.Parse(adr_arr[0]);
                Bit = int.Parse(adr_arr[1]);
            }
        }

        //Тип тэга
        public S7PROSIMLib.PointDataTypeConstants Type;


        #region Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
