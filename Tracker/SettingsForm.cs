using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tracker
{
    public partial class SettingsForm : Form
    {
        private readonly SqlConnectionStringBuilder connectionStringBuilder;

        public SettingsForm()
        {
            InitializeComponent();

            connectionStringBuilder = new SqlConnectionStringBuilder(Properties.Settings.Default.ConnectionString);

            pgConnectionSettings.SelectedObject = connectionStringBuilder;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.ConnectionString = connectionStringBuilder.ToString();
            Properties.Settings.Default.Save();
        }
    }
}
