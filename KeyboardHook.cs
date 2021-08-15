using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/////////////////////////////////////////////////////////////////////
//      Author : Mustafa Can Ozturk                                //
//      Purpose: This class registers custom shortcut keys         //
//      Input  : Form, Modifier, Key (e.g. Form1, "CTRL", F9)      //
//      Date   : 15.08.2021                                        //
/////////////////////////////////////////////////////////////////////

namespace APIdeneme.Controls
{
    public class KeyboardHook
    {
        //Register Hot Key
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        
        //Unregister Hot Key
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        public enum Modifiers
        {
            ALT = 0x0001,
            CONTROL = 0x0002,
            SHIFT = 0x004,
            WINDOWS = 0x008
        }

        public KeyboardHook(Form form, Keys key)
        {
            int UniqueHotkeyId = (int)key;
            int HotKeyCode = (int)key;
            Boolean keyRegistered = RegisterHotKey(form.Handle, UniqueHotkeyId, 0x0000, HotKeyCode);

            if (!keyRegistered)
            {
                MessageBox.Show($"Global Hotkey {key.ToString()} couldn't be registered !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public KeyboardHook(Form form, Modifiers modifier, Keys key)
        {
            int UniqueHotkeyId = (int)key + (int)modifier;
            int HotKeyCode = (int)key;
            Boolean keyRegistered = RegisterHotKey(form.Handle, UniqueHotkeyId, (int)modifier, HotKeyCode);

            if (!keyRegistered)
            {
                MessageBox.Show($"Global Hotkey {modifier.ToString()} + {key.ToString()} couldn't be registered !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void Unregister(Form form, int HotKeyID)
        {
            Boolean keyUnregistered = UnregisterHotKey(form.Handle, HotKeyID);

            if (!keyUnregistered)
            {
                MessageBox.Show($"Global Hotkey ID: {HotKeyID.ToString()} couldn't be unregistered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////
//      Author     : Mustafa Can Ozturk                                                            //
//      Explanation: THIS NEEDS TO BE PASTED INTO THE FORM WHERE WE WANT TO REGISTER THE KEYS      //
//      Date       : 15.08.2021                                                                    //
/////////////////////////////////////////////////////////////////////////////////////////////////////


protected override void WndProc(ref Message m)
{

    //Catch when a HotKey is pressed !
    if (m.Msg == 0x0312)
    {
        int id = m.WParam.ToInt32();
        //Cast keys into integer and sum up. The result will be used for id 
        if (id == Globals.CreateGridID()) Grid.Grid.CreateGrid(this);

        else if (id == Globals.DeleteGridID()) Grid.Grid.DeleteGrid(this);

    }

    base.WndProc(ref m);
}
