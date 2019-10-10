//using System;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Threading;
//using System.Diagnostics;

//namespace Input_PLCSim
//{
   
//public partial class MainWindow : Window
//{
//    public S7PROSIMLib.S7ProSim PS = new S7PROSIMLib.S7ProSim();

//    private object I0_0 = false;
//    private object I0_1 = false;
//    private object I0_2 = false;
//    private object I0_3 = false;
//    private object I0_4 = false;
//    private object I0_5 = false;
//    private object I0_6 = false;
//    private object I0_7 = false;
//    private object I1_0 = false;
//    private object I1_1 = false;
//    private object I1_2 = false;
//    private object I1_3 = false;
//    private object I1_4 = false;
//    private object I1_5 = false;
//    private object I1_6 = false;

//    //Таймер для сброса сигналов с кнопок дверей
//    DispatcherTimer timer_door = new DispatcherTimer();
//    timer_door = new DispatcherTimer();
//    timer_door.Tick += new EventHandler(timer_door_Tick);
//    timer_door.Interval = new TimeSpan(0, 0, 2);
//    timer_door.Start();

//    private void timer_door_Tick(object sender, EventArgs e)
//    {

//        I0_0 = false;
//        PS.WriteInputPoint(0, 0, ref I0_0);

//        I0_1 = false;
//        PS.WriteInputPoint(0, 1, ref I0_1);

//        I0_2 = false;
//        PS.WriteInputPoint(0, 2, ref I0_2);

//        I0_3 = false;
//        PS.WriteInputPoint(0, 3, ref I0_3);

//        I0_4 = false;
//        PS.WriteInputPoint(0, 4, ref I0_4);

//        I0_5 = false;
//        PS.WriteInputPoint(0, 5, ref I0_5);

//        I0_6 = false;
//        PS.WriteInputPoint(0, 6, ref I0_6);

//        I0_7 = false;
//        PS.WriteInputPoint(0, 7, ref I0_7);
//    }

//}