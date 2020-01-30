using KeyboardHook;
using System;
using System.Linq;
using System.Windows.Forms;
using static KeyboardHook.GlobalKeyboardHook;

namespace KeyboardLock
{
    public partial class MainForm : Form
    {
        private readonly GlobalKeyboardHook _globalKeyboardHook;

        public MainForm()
        {
            InitializeComponent();
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }
        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == KeyboardState.KeyDown)
            {
                textKeys.SelectedText = $"{e.KeyboardData.VirtualCode:x2} ";
                textKeys.SelectionStart += 3;
                if (textKeys.Text.Length > 1024 * 5)
                {
                    var text = textKeys.Text;
                    textKeys.Text = text.Substring(text.Length - 30);
                    textKeys.SelectionStart = textKeys.Text.Length;
                }
                e.Handled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) => _globalKeyboardHook?.Dispose();
    }
}
