using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Velopack.Logging;

namespace APBConfigInstaller.Services;

class WindowsEventLogLogger : IVelopackLogger
{
    public void Log(VelopackLogLevel logLevel, string? message, Exception? exception)
    {
        EventLog.WriteEntry("Application", message);
    }
}
