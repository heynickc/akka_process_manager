using System.Collections.Generic;
using Akka.Actor;

namespace AkkaProcessManager {

    public class ProcessStarted {
        public string ProcessId { get; private set; }
        public IActorRef Process { get; private set; }

        public ProcessStarted(string processId, IActorRef process) {
            ProcessId = processId;
            Process = process;
        }
    }

    public class ProcessStopped {
        public string ProcessId { get; private set; }
        public IActorRef Process { get; private set; }

        public ProcessStopped(string processId, IActorRef process) {
            ProcessId = processId;
            Process = process;
        }
    }

    public class ProcessManager : ReceiveActor {

        private readonly Dictionary<string, IActorRef> _processes;

        public ProcessManager() {
            _processes = new Dictionary<string, IActorRef>();
        }

        public IActorRef ProcessOf(string processId) {
            if (_processes.ContainsKey(processId)) {
                return _processes[processId];
            }
            return null;
        }

        public void StartProcess(string processId, IActorRef process) {
            if (!_processes.ContainsKey(processId)) {
                _processes.Add(processId, process);
                Self.Tell(new ProcessStarted(processId, process));
            }
        }

        public void StopProcess(string processId) {
            if (_processes.ContainsKey("processId")) {
                var process = _processes[processId];
                _processes.Remove(processId);
                Self.Tell(new ProcessStopped(processId, process));
            }
        }
    }
}