using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tracker
{
    public partial class SettingsForm : Form
    {
        private readonly SqlConnectionStringBuilder connectionStringBuilder;

        public SettingsForm(string connectionString)
        {
            InitializeComponent();

            connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            pgConnectionSettings.SelectedObject = connectionStringBuilder;
        }

        public string ConnectionString
            => connectionStringBuilder.ConnectionString;

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (ConnectionUtils.TryConnect(connectionStringBuilder.ConnectionString, out var exception))
            {
                this.ShowInformation("Connection established.", "Success");
            }
            else
            {
                this.ShowError(exception, "Failure");
            }
        }
    }
}
