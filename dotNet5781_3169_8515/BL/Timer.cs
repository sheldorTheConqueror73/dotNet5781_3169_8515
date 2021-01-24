using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using BO;

namespace BL
{
   public sealed class Timer
    {
        static BLAPI.IBL bl = BLAPI.BLFactory.GetBL();
        static List<int> observers=null;
        static BackgroundWorker worker = null;
        public Timer()
        {
            if (observers == null)
                observers = new List<int>();
            if (worker == null)
            { 
                worker = new BackgroundWorker();
                worker.DoWork += startTimer;
                worker.ProgressChanged += bl.getTimer();
                worker.WorkerSupportsCancellation = true;
                worker.WorkerReportsProgress = true;
              
            }
        }
        /// <summary>
        /// adds a new bus to the observer list, starts worker if it is not currently running
        /// </summary>
        /// <param name="observerID">bus id</param>
        public static void add(int observerID)
        {
            Console.WriteLine($"Bus {observerID} has been added to observer list");
            observers.Add(observerID);
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }
        /// <summary>
        /// removes a  bus from the observer list, stops worker if it is currently running
        /// </summary>
        /// <param name="observerID">bus id</param>
        public static void remove(int observerID)
        {
            Console.WriteLine ($"Bus {observerID} has been removed from observer list");
            observers.Remove(observerID);
            if (observers.Count == 0 && worker.IsBusy)
                worker.CancelAsync();
               
        }
        /// <summary>
        /// timer engine
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">DoWorkEventArgs</param>
        private static void startTimer(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Worker has started");
          
            while (!worker.CancellationPending)
            {
                foreach (var bus in observers.ToList())
                    bl.Tick(bus);
                 Thread.Sleep(1000);
                worker.ReportProgress(1);
                Console.WriteLine("Worker finihshed a loop");
            }
            Console.WriteLine("Worker has stopped");
            e.Cancel = true;
            return;
        }
        
    }
}
