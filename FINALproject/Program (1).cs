using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
           string command = Console.ReadLine();
        // open(command);
            endpro(command);
        }

        public static void endpro(string taskname)
        {
            string processName = taskname.Replace(".exe", "");

            foreach (Process process in Process.GetProcessesByName(processName))
            {
                process.Kill();
            }
        }
        public static void open(string command)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = procStartInfo;
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
        }
    }
}
