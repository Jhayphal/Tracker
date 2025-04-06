using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tracker
{
    public partial class SettingsForm : Form
    {
        public SqlConnectionStringBuilder ConnectionStringBuilder { get; }

        public SettingsForm(string connectionString)
        {
            InitializeComponent();

            ConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            pgConnectionSettings.SelectedObject = ConnectionStringBuilder;
        }
    }
}
