using System;
using System.Windows.Forms;

namespace PerfMonFormSecond
{
    public partial class MainForm : Form
    {
        DoClassCpuRam doClassCpuRam = new DoClassCpuRam();
        DoClassInternetConnection doClassInternetConnection = new DoClassInternetConnection();
        NotifyIcon niCPU;
        NotifyIcon niRAM;

        public MainForm()
        {
            // start the app and the threads
            InitializeComponent();
            StartMethod();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
            niCPU = doClassCpuRam._notifyIconCpu;
            niRAM = doClassCpuRam._notifyIconRam;
            niCPU.ContextMenuStrip = CPUcontextMenuStrip;
            niRAM.ContextMenuStrip = RAMcontextMenuStrip;
            niCPU.Visible = true;
            niRAM.Visible = true;
        }
        public void StartMethod()
        {
            doClassCpuRam.StartThreads();
            doClassInternetConnection.StartThreads();
        }

        #region Events
        private void Exit_button_Click(object sender, EventArgs e)
        {
            doClassCpuRam.CloseAppMethod();
            doClassInternetConnection.ClosePing();
            if(niCPU.Visible)
                niCPU.Dispose();
            if(niRAM.Visible)
                niRAM.Dispose();
            Application.Exit();
        }
        private void ExitCPUToolStripMenuItem_Click(object sender, EventArgs e) //Exit the CPU thread. If the RAM thread is closed, close the app 
        {
            if(doClassCpuRam._threadTelemetryCpu.IsAlive)
            {
                doClassCpuRam._threadTelemetryCpu.Abort();
                doClassCpuRam._notifyIconCpu.Dispose();
            }
            if(doClassCpuRam._threadTelemetryRam.IsAlive == false)
            {
                doClassCpuRam.CloseAppMethod();
            }
        }
        private void RAMToolStripMenuItem_Click(object sender, EventArgs e) //Exit the RAM thread. If the CPU thread is closed, close the app 
        {
            if(doClassCpuRam._threadTelemetryRam.IsAlive)
            {
                doClassCpuRam._threadTelemetryRam.Abort();
                doClassCpuRam._notifyIconRam.Dispose();
            }
            if(doClassCpuRam._threadTelemetryCpu.IsAlive == false)
            {
                doClassCpuRam.CloseAppMethod();
            }
        }
        #endregion
    }
}
