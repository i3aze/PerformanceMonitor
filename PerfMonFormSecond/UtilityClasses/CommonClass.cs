using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PerfMonFormSecond.UtilityClasses
{
    class CommonClass
    {
        internal void ShowCpuIcon(NotifyIcon notifyIcon, string iconName, float cpuCount)
        {
            string s = "IconsCPU\\" + iconName;
            Icon icon = new Icon(s);
            notifyIcon.Icon = icon;
            notifyIcon.Visible = true;
            notifyIcon.Text = "CPU Usage: " + cpuCount.ToString("0.00") + "%";
        }
        internal void ShowRamIcon(NotifyIcon notifyIcon, string iconName, float cpuCount)
        {
            string s = "IconsRAM\\" + iconName;
            Icon icon = new Icon(s);
            notifyIcon.Icon = icon;
            notifyIcon.Visible = true;
            notifyIcon.Text = "RAM Usage: " + cpuCount.ToString("0.00") + "%";
        }
        internal void WriteDataToFileSW(ReaderWriterLockSlim Lock, string telemetry, string value)
        {
            Lock.EnterWriteLock();
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (StreamWriter sw = File.AppendText(path + "\\CPU-RAM Telemetry.csv"))
                {
                    string dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    var line = String.Format("{0}; {1}; {2}", dateTime, telemetry, value);
                    sw.WriteLine(line);
                }
            }
            catch(IOException ioe)
            {
                ioe.Message.ToString();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }
    }
}
