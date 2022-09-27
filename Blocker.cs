using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace _202020
{
    internal class Blocker
    {
        internal Window Overlay;
        public Blocker()
        {
            
            Overlay = new Window();
            Overlay.Topmost = true;
            Overlay.WindowState = WindowState.Maximized;
            Overlay.WindowStyle = WindowStyle.None;
            Overlay.AllowsTransparency = true;
            Overlay.Background = new SolidColorBrush(Color.FromArgb(200, 10, 10, 10));
           
        }

    }
}
