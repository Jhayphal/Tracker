﻿using System;
using System.Data.SqlClient;

using Tracker.Extensions;

namespace Tracker.Forms
{
    public partial class SettingsForm : ScalableForm
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
                using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
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
