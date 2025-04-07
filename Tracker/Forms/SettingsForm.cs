using System;
using System.Data.SqlClient;
using System.Windows.Forms;

using Tracker.Extensions;

namespace Tracker.Forms
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
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                }

                this.ShowInformation("Connection established.", "Success");
            }
            catch (Exception ex)
            {
                this.ShowError(ex, "Failure");
            }
        }
    }
}
