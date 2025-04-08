using System.Drawing;
using System.Windows.Forms;

using Microsoft.Win32;

namespace Tracker.Forms
{
    public class ScalableForm : Form
    {
        public ScalableForm()
        {
            Font = SystemFonts.IconTitleFont;

            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }

        protected override void Dispose(bool disposing)
        {
            SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;

            base.Dispose(disposing);
        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Window)
            {
                Font = SystemFonts.IconTitleFont;
            }
        }
    }
}
