using System.Windows.Forms;

namespace Kengic.Opns.Command
{
    public class UI
    {
        public static IWin32Window Top() => (IWin32Window) new Form()
        {
            TopMost = true,
            WindowState = FormWindowState.Maximized
        };
    }
}