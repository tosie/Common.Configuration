using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Common.Configuration {
    /// <summary>
    /// The profiler class helps in profiling code sections. It outputs the time required to code completion and memory used before and afterwads.
    /// </summary>
    /// <example>
    /// using (new Profiler(/* Add output handler here */)) {
    ///   // Add code here
    /// }
    /// </example>
    public class Profiler : IDisposable {

        #region Properties

        /// <summary>
        /// If true, <see cref="GC.Collect"/> is called right before the profiling ends.
        /// </summary>
        public Boolean InvokeGarbageCollector { get; set; }

        /// <summary>
        /// A reference to the stopwatch being used to calculate the execution time.
        /// </summary>
        public Stopwatch Watch { get; protected set; }

        /// <summary>
        /// Shortcut to <see cref="Watch.Elapsed"/>. Returns TimeSpan.MinValue, if the watch has not been initialized, yet.
        /// </summary>
        public TimeSpan Duration {
            get {
                if (Watch == null)
                    return TimeSpan.MinValue;
                else
                    return Watch.Elapsed;
            }
        }

        /// <summary>
        /// Holds the amount of memory being used by the application at the beginning of the profiling.
        /// </summary>
        public Double MemAtBeginning { get; protected set; }

        /// <summary>
        /// Holds the amount of memory being used by the application at the end of the profiling.
        /// </summary>
        public Double MemAtEnd { get; protected set; }

        /// <summary>
        /// Difference of <see cref="MemAtEnd"/> and <see cref="MemAtBeginning"/>.
        /// </summary>
        public Double MemUsage { get { return MemAtEnd - MemAtBeginning; } }

        #endregion

        #region Events

        /// <summary>
        /// Function signature to use when receiving the profiling summary (<seealso cref="Output"/>).
        /// </summary>
        /// <param name="Text">A string containing the profiling summary.</param>
        public delegate void WriteDelegate(String Text);

        /// <summary>
        /// This event is raised at the end of the profiling and contains the profiling summary. If no event handler is specified, the output goes to the debugger (if available).
        /// </summary>
        public event WriteDelegate Output;

        #endregion

        #region Constructor

        public Profiler() {
            InitializeProperties();

            Start();
        }

        public Profiler(WriteDelegate Output) {
            InitializeProperties();

            this.Output += Output;

            Start();
        }

        protected void InitializeProperties() {
            InvokeGarbageCollector = true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose() {
            Stop();
        }

        #endregion

        #region Profiling

        protected void Start() {
            Watch = new Stopwatch();
            Watch.Start();

            MemAtBeginning = Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0;
        }

        protected void Stop() {
            if (InvokeGarbageCollector)
                GC.Collect();

            // Collect the data
            Watch.Stop();
            MemAtEnd = Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0;

            // Write a summary string
            WriteLine(String.Format("  Speicherverbrauch: {0:0.00} MB -> {1:0.00} MB (= {2:+ 0.00;- 0.00;+ 0} MB).",
                MemAtBeginning, MemAtEnd, MemUsage));
            WriteLine(String.Format("  Dauer: {0}.",
                Duration));

            // Reset the stopwatch
            Watch.Reset();
        }

        #endregion

        #region Output

        protected void Write(String Text) {
            // If there is an output event handler, send the text to the event handler(s).
            // Otherwise everything goes to the debugger.
            if (Output != null)
                Output(Text);
            else
                Debug.Write(Text);
        }

        protected void WriteLine(String Text) {
            Write(Text + Environment.NewLine);
        }

        #endregion

    }
}
