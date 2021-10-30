using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using PerfMonFormSecond.UtilityClasses;

namespace PerfMonFormSecond
{
    class DoClassInternetConnection
    {
        Thread _threadPing;
        private static ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        public void StartThreads() // start threads
        {
            _threadPing = new Thread(new ThreadStart(PingGoogle));
            _threadPing.Start();
        }
        public void PingGoogle() // ping google every second and write in the csv file
        {
            Ping myPing = new Ping();
            PingReply reply;
            CommonClass cc = new CommonClass();

            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    reply = myPing.Send("8.8.8.8", 1000);
                    if (reply.Status == IPStatus.Success)
                        cc.WriteDataToFileSW(Lock, "Connected", null);
                    else
                        cc.WriteDataToFileSW(Lock, "Not Connected", null);
                }
                catch (PingException pe)
                {
                    pe.Message.ToString();
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }
            }
        }
        public void ClosePing()
        {
            if(_threadPing.IsAlive)
            {
                _threadPing.Abort();
                Application.Exit();
            }
        }
    }
}
