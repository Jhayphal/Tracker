using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tracker
{
    public partial class MainForm : Form
    {
        private readonly DataStorage storage = new DataStorage(@"Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;");

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var refreshTask = RefreshAsync();

            var onSuccess = refreshTask.ContinueWith(
                OnRefreshSuccess,
                TaskContinuationOptions.ExecuteSynchronously
                    | TaskContinuationOptions.NotOnFaulted
                    | TaskContinuationOptions.NotOnCanceled);

            var onCanceled = refreshTask.ContinueWith(
                OnRefreshCanceled,
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnCanceled);

            var onError = refreshTask.ContinueWith(
                t => this.ShowError(t.Exception),
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);

            Task.WhenAny(onError, onCanceled, onSuccess)
                .ContinueWith(_ => Enabled = true, TaskContinuationOptions.ExecuteSynchronously);
        }

        private async Task<IEnumerable<Record>> RefreshAsync()
        {
            Enabled = false;

            return await storage.GetRecordsAsync();
        }

        private void OnRefreshCanceled(Task<IEnumerable<Record>> t)
            => this.ShowInformation(t.Exception.Message);

        private void OnRefreshSuccess(Task<IEnumerable<Record>> t) => dgvRecords.DataSource = t.Result;
    }
}
