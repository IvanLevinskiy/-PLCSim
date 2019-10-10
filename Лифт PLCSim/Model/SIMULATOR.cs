using S7PROSIMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Лифт_PLCSim.Model
{
    public static class SIMULATOR
    {
        //Экземпляр симулятора
        public static S7ProSim PLCSim = new S7ProSim();

       
        
        //Запись сигнала в симулятор наподобие I0.0
        public static bool IsAvailable
        {
            get
            {
                //Проверяем, если сумулятора нет, то выходим из функции
                if (PLCSim == null)
                {
                    return false;
                }

                //Получаем статус симулятора, если он подключен
                if (!PLCSim.GetState().Contains("RUN"))
                {
                    return false;
                }

                return true;
            }
        }

        
        //Запись дискретного сигнала в симулятор наподобие
        public static void WriteInputBit(int ByteIndex, int BitIndex, bool value)
        {
            //Если симулятор не отвечает, тогда выходим из метода
            if (IsAvailable == false)
            {
                return;
            }

            //Создаем объект, в который помещаем элемент для записи
            object Value = value;

            //Записываем позиции в симулятор
            PLCSim.WriteInputPoint(ByteIndex, BitIndex, ref Value);
        }

        
        //Запись слова PIW
        public static void WriteInputWord(int Address, short value)
        {
            //Если симулятор не отвечает, тогда выходим из метода
            if (IsAvailable == false)
            {
                return;
            }

            //Создаем объект, в который помещаем элемент для записи
            object Value = value;

            //Получаем значение
            object PIW_VALUE = new short[1] { value };

            //Запись значения в симулятор
            PLCSim.WriteInputImage(Address, ref PIW_VALUE);
        }

       
        //Чтение дискретного сигнала в симулятор наподобие
        public static bool ReadOutputBit(int ByteIndex, int BitIndex)
        {
            //Если симулятор не отвечает, тогда выходим из метода
            if (IsAvailable == false)
            {
                return false;
            }

            //Читаем состояние бита и возвращаем результат
            object BitState = false;
            PLCSim.ReadOutputPoint(ByteIndex, BitIndex, S7PROSIMLib.PointDataTypeConstants.S7_Bit, ref BitState);

            return (bool)BitState;
        }
    }
}
