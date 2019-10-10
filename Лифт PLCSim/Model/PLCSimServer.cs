using S7PROSIMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Лифт_PLCSim.Model
{
    public class PLCSimServer
    {
        //Экземпляр S7ProSim
        public S7ProSim PS = new S7ProSim();

        //Список тэгов
        public List<PLCSimTag> Tags = new List<PLCSimTag>();

        //Свойство соединения
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            set
            {
                if (value == true)
                {
                    PS.Connect();
                    PS.SetState("RUN_P");
                    PS.SetScanMode(S7PROSIMLib.ScanModeConstants.ContinuousScan);
                    isConnected = value;

                    Thread thread = new Thread(Update);
                    thread.IsBackground = true;
                    thread.Start();
                }
                else
                {
                    PS.SetState("STOP");
                    PS.Disconnect();
                    isConnected = value;

                }
            }
        }
        bool isConnected;

        public PLCSimServer()
        {
            Thread thread = new Thread(Update);
        }

        public void Add(string adr)
        {
            var tag = new PLCSimTag(adr);
            Tags.Add(tag);
        }

        public void Add(string adr, Ellipse ellipse)
        {
            var tag = new PLCSimTag(adr);
            Tags.Add(tag);
        }

        public void Add(string adr, TextBlock text)
        {
            var tag = new PLCSimTag(adr);
            Tags.Add(tag);
        }

        //Опрос
        void Update()
        {
            while (IsConnected)
            {
                foreach (var tag in Tags)
                {
                    //Переменная для временного хранения прочитанного значения
                    object val = null;
                    PS.ReadOutputPoint(tag.Byte, tag.Bit, tag.Type, ref val);
                }

                Thread.Sleep(10);
            }
        }
    }
}
