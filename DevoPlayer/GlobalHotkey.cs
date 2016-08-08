using System;
using System.Windows.Forms;

namespace DevoPlayer
{
    public enum KeyModifier
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        WinKey = 8
    }
    class GlobalHotkeys
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        Form1 form;

        int pauseId = 0;     // The id of the hotkey. 
        int prevId = 1;
        int nextId = 2;

        public GlobalHotkeys(Form1 form)
        {
            this.form = form;
            register();
        }

        public void register()
        {
            RegisterHotKey(form.Handle, pauseId, 0, Keys.Pause.GetHashCode());       // Register PauseBreak as global hotkey. 
            RegisterHotKey(form.Handle, prevId, (int)KeyModifier.Control | (int)KeyModifier.Alt, Keys.PageUp.GetHashCode());
            RegisterHotKey(form.Handle, nextId, (int)KeyModifier.Control | (int)KeyModifier.Alt, Keys.PageDown.GetHashCode());
        }

        public void unregister()
        {
            UnregisterHotKey(form.Handle, pauseId);       // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.
            UnregisterHotKey(form.Handle, prevId);
            UnregisterHotKey(form.Handle, nextId);
        }
    }
}