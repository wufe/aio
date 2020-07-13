using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ghoul.Presentation.Web.Middleware.HostedService {
    public class ChildProcessHandlerContainer : IDisposable {

        public IEnumerable<Process> Processes { get; private set; } = new List<Process>();

        public void RegisterChildProcess(Process process) {
            if (!Processes.Any(p => p == process)) {
                Debug.WriteLine($"Registering process #{process.Id}");
                var processesList = Processes.ToList();
                processesList.Add(process);
                Processes = processesList;
            }
        }

        public void UnregisterChildProcess(Process process) {
            Processes = Processes.Where(p => p != process).ToList();
        }

        public void Dispose()
        {
            foreach (var process in Processes)
            {
                try {
                    Debug.WriteLine($"Killing process #{process.Id}");
                    process.Kill();
                } catch (Exception e) {
                    Debug.WriteLine(e.Message);
                }
            }
        }

    }
}