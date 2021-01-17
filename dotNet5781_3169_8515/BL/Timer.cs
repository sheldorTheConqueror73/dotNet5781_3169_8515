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
        static List<BO.Bus> observers=null;
        static BackgroundWorker worker = null;
        public Timer()
        {
            if (observers == null)
                observers = new List<Bus>();
            if (worker == null)
            { 
                worker = new BackgroundWorker();
                worker.DoWork += startTimer;
                worker.ProgressChanged += bl.passTimer(null,0);
                worker.WorkerSupportsCancellation = true;
                worker.WorkerReportsProgress = true;
              
            }
        }
        public static void add(Bus observer)
        {
            observers.Add(observer);
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }
        public static void remove(Bus observer)
        {
            observers.Remove(observer);
            if (observers.Count == 0 && worker.IsBusy)
                worker.CancelAsync();
               
        }
        private static void startTimer(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Worker is starting");
            while (!worker.CancellationPending)
            {
                foreach (var bus in observers)
                    bl.Tick(bus.id);
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
