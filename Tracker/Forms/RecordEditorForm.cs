using System.Windows.Forms;

using Tracker.Database.Models;

namespace Tracker.Forms
{
    public partial class RecordEditorForm : Form
    {
        public RecordEditorForm()
        {
            InitializeComponent();
        }

        public RecordEditorForm(Record record)
        {
            InitializeComponent();

            tbDescription.Text = record.Description;
            nudTotal.Value = record.Total;
            tbComment.Text = record.Comment;
        }

        public string Description
            => tbDescription.Text;

        public decimal Total
            => nudTotal.Value;

        public string Comment
            => tbComment.Text;
    }
}
