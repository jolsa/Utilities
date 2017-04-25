using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Utilities
{
    public class InstanceManagement
    {
        private readonly string _mutexId;
        private readonly Mutex _mutex;

		/// <summary>
		/// Message posted when another instance is started
		/// </summary>
        public int WM_SHOW_APP { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="mutexId">Presumably unique string for the mutex or application</param>
        public InstanceManagement(string mutexId)
        {
            _mutexId = mutexId;
            _mutex = new Mutex(true, _mutexId);
            WM_SHOW_APP = WinAPI.RegisterWindowMessage(_mutexId);
        }
        public bool IsRunning
        {
            get
            {
                bool running = !_mutex.WaitOne(TimeSpan.Zero, true);
                if (!running)
                    _mutex.ReleaseMutex();
                return running;
            }
        }
        public void Activate()
        {
			WinAPI.PostMessage(WinAPI.HWND_BROADCAST, WM_SHOW_APP, IntPtr.Zero, IntPtr.Zero);
        }
		public static void SetForeground(Form theForm)
		{
			if (theForm.WindowState == FormWindowState.Minimized)
				theForm.WindowState = FormWindowState.Normal;
			//  Is it top-most?
			var wasTop = theForm.TopMost;
			//  Set it to top-most
			theForm.TopMost = true;
			//  Set it back
			theForm.TopMost = wasTop;
			//	Get Focus
			theForm.Activate();
		}
		public void WndProcHandler(Message m, Form theForm)
		{
			if (m.Msg == WM_SHOW_APP)
				SetForeground(theForm);
		}
	}
}
