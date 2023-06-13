using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCheck
{
    public class ProcessDispatcher
    {
        public static Process Get()
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process proc in procs)
                if ((proc.Id != curr.Id) && (proc.MainModule.FileName == curr.MainModule.FileName))
                    return proc;
            return null;
        }
    }
}
