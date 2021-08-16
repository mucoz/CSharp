using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


//FIRST BLOCK


namespace ThemeDeneme
{
    class API
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}




//SECOND BLOCK


//create mouse down event in the main form 
lbl.MouseDown += Lbl_MouseDown;

//codes for the event
private void Lbl_MouseDown(object sender, MouseEventArgs e)
{
    if (e.Button == MouseButtons.Left)
    {
        API.ReleaseCapture();
        API.SendMessage(this.Handle, API.WM_NCLBUTTONDOWN, API.HT_CAPTION, 0);
    }
}
