using System;
using System.Linq;
using System.Windows.Forms;

namespace Tracker.Extensions
{
    internal static class MessageExtensions
    {
        public static void ShowError(this Form owner, Exception exception, string caption = "Error")
        {
            var message = exception.Message;
            if (exception is AggregateException aggregate)
            {
                message = string.Join(
                    Environment.NewLine,
                    aggregate.InnerExceptions.Select(e => e.Message));
            }

            ShowMessage(owner, message, caption, MessageBoxIcon.Error);
        }

        public static void ShowInformation(this Form owner, string message, string caption = "Information")
            => ShowMessage(owner, message, caption, MessageBoxIcon.Information);

        public static void ShowWarning(this Form owner, string message, string caption = "Warning")
            => ShowMessage(owner, message, caption, MessageBoxIcon.Warning);

        public static void ShowMessage(
            this Form owner,
            string message,
            string caption,
            MessageBoxIcon boxIcon,
            MessageBoxButtons buttons = MessageBoxButtons.OK)
            => MessageBox.Show(owner, message, caption, buttons, boxIcon);
    }
}
