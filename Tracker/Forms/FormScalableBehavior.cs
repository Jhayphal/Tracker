using System.Drawing;
using System.Windows.Forms;

using Microsoft.Win32;

namespace Tracker.Forms
{
    internal sealed class FormScalableBehavior
    {
        private readonly Form target;

        public FormScalableBehavior(Form target)
        {
            this.target = target;
            this.target.Font = SystemFonts.IconTitleFont;
            this.target.FormClosing += Target_FormClosing;

            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged; ;
        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Window)
            {
                target.Font = SystemFonts.IconTitleFont;
            }
        }

        private void Target_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
        }
    }
}
