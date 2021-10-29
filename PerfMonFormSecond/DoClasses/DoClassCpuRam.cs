using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using PerfMonFormSecond.UtilityClasses;

namespace PerfMonFormSecond
{
    public class DoClassCpuRam
    {
        public Icon _IconCpu;
        public Icon _IconRam;
        public NotifyIcon _notifyIconCpu = new NotifyIcon();
        public NotifyIcon _notifyIconRam = new NotifyIcon();
        public Thread _threadTelemetryCpu;
        public Thread _threadTelemetryRam;
        private static ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        public void StartThreads() // start threads
        {
            try 
            {
                _threadTelemetryCpu = new Thread(new ThreadStart(GetValuesCPU));
                _threadTelemetryRam = new Thread(new ThreadStart(GetValuesRAM));
                _threadTelemetryCpu.IsBackground = true;
                _threadTelemetryRam.IsBackground = true;
                _threadTelemetryCpu.Start();
                _threadTelemetryRam.Start();
            }
            catch (ThreadStartException tse)
            {
                tse.Message.ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        public void GetValuesCPU() //get CPU value and add the value to the taskbar
        {
            float cpuCount;
            PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            CommonClass cc = new CommonClass();
            while (true)
            {
                Thread.Sleep(1000);
                cpuCount = perfCPUCount.NextValue();
                cc.WriteDataToFileSW(Lock, "CPU Usage", cpuCount.ToString("0.00") + "%");
                if (float.Parse(cpuCount.ToString("0.00")) > 0 && float.Parse(cpuCount.ToString("0.00")) <= 6.25)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon6x25.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 6.25 && float.Parse(cpuCount.ToString("0.00")) <= 12.5)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon12x5.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 12.5 && float.Parse(cpuCount.ToString("0.00")) <= 18.75)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon18x75.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 18.75 && float.Parse(cpuCount.ToString("0.00")) <= 25)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon25.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 25 && float.Parse(cpuCount.ToString("0.00")) <= 31.25)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon31x25.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 31.25 && float.Parse(cpuCount.ToString("0.00")) <= 37.5)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon37x5.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 37.5 && float.Parse(cpuCount.ToString("0.00")) <= 43.75)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon43x75.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 43.75 && float.Parse(cpuCount.ToString("0.00")) <= 50)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon50.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 50 && float.Parse(cpuCount.ToString("0.00")) <= 56.25)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon56x25.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 56.25 && float.Parse(cpuCount.ToString("0.00")) <= 62.5)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon62x5.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 62.5 && float.Parse(cpuCount.ToString("0.00")) <= 68.75)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon68x75.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 68.75 && float.Parse(cpuCount.ToString("0.00")) <= 75)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon75.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 75 && float.Parse(cpuCount.ToString("0.00")) <= 81.25)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon81x25.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 81.25 && float.Parse(cpuCount.ToString("0.00")) <= 87.5)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon87x5.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 87.5 && float.Parse(cpuCount.ToString("0.00")) <= 93.75)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon93x75.ico", cpuCount);
                else if (float.Parse(cpuCount.ToString("0.00")) > 93.75 && float.Parse(cpuCount.ToString("0.00")) <= 100)
                    cc.ShowCpuIcon(_notifyIconCpu, "Icon100.ico", cpuCount);
            }
        }
        public void GetValuesRAM() //get RAM value and add the value to the taskbar
        {
            float ramCount;
            PerformanceCounter perfRAMCount = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            CommonClass cc = new CommonClass();

            while (true)
            {
                Thread.Sleep(1000);
                ramCount = perfRAMCount.NextValue();
                cc.WriteDataToFileSW(Lock, "RAM Usage", ramCount.ToString("0.00") + "%");

                if (float.Parse(ramCount.ToString("0.00")) > 0 && float.Parse(ramCount.ToString("0.00")) <= 6.25)
                    cc.ShowRamIcon(_notifyIconRam, "Icon6x25.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 6.25 && float.Parse(ramCount.ToString("0.00")) <= 12.5)
                    cc.ShowRamIcon(_notifyIconRam, "Icon12x5.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 12.5 && float.Parse(ramCount.ToString("0.00")) <= 18.75)
                    cc.ShowRamIcon(_notifyIconRam, "Icon18x75.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 18.75 && float.Parse(ramCount.ToString("0.00")) <= 25)
                    cc.ShowRamIcon(_notifyIconRam, "Icon25.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 25 && float.Parse(ramCount.ToString("0.00")) <= 31.25)
                    cc.ShowRamIcon(_notifyIconRam, "Icon32x25.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 31.25 && float.Parse(ramCount.ToString("0.00")) <= 37.5)
                    cc.ShowRamIcon(_notifyIconRam, "Icon37x5.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 37.5 && float.Parse(ramCount.ToString("0.00")) <= 43.75)
                    cc.ShowRamIcon(_notifyIconRam, "Icon43x75.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 43.75 && float.Parse(ramCount.ToString("0.00")) <= 50)
                    cc.ShowRamIcon(_notifyIconRam, "Icon50.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 50 && float.Parse(ramCount.ToString("0.00")) <= 56.25)
                    cc.ShowRamIcon(_notifyIconRam, "Icon56x25.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 56.25 && float.Parse(ramCount.ToString("0.00")) <= 62.5)
                    cc.ShowRamIcon(_notifyIconRam, "Icon62x5.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 62.5 && float.Parse(ramCount.ToString("0.00")) <= 68.75)
                    cc.ShowRamIcon(_notifyIconRam, "Icon68x75.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 68.75 && float.Parse(ramCount.ToString("0.00")) <= 75)
                    cc.ShowRamIcon(_notifyIconRam, "Icon75.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 75 && float.Parse(ramCount.ToString("0.00")) <= 81.25)
                    cc.ShowRamIcon(_notifyIconRam, "Icon81x25.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 81.25 && float.Parse(ramCount.ToString("0.00")) <= 87.5)
                    cc.ShowRamIcon(_notifyIconRam, "Icon87x5.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 87.5 && float.Parse(ramCount.ToString("0.00")) <= 93.75)
                    cc.ShowRamIcon(_notifyIconRam, "Icon93x75.ico", ramCount);
                else if (float.Parse(ramCount.ToString("0.00")) > 93.75 && float.Parse(ramCount.ToString("0.00")) <= 100)
                    cc.ShowRamIcon(_notifyIconRam, "Icon100.ico", ramCount);
            }
        }
        public void CloseAppMethod()
        {
            try
            {
                if(_threadTelemetryCpu.IsAlive)
                    _threadTelemetryCpu.Abort();
                if(_threadTelemetryRam.IsAlive)
                    _threadTelemetryRam.Abort();
                if(_notifyIconCpu.Visible)
                    _notifyIconCpu.Dispose();
                if(_notifyIconRam.Visible)
                    _notifyIconRam.Dispose();
                Environment.Exit(0);
            }
            catch(ThreadAbortException tae)
            {
                tae.Message.ToString();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
