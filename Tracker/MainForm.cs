using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tracker
{
    public partial class MainForm : Form
    {
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly DataStorage storage = new DataStorage(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;");

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var refreshTask = RefreshAsync();

            var onSuccess = refreshTask.ContinueWith(
                t => dgvRecords.DataSource = t.Result,
                TaskContinuationOptions.ExecuteSynchronously
                    | TaskContinuationOptions.NotOnFaulted
                    | TaskContinuationOptions.NotOnCanceled);

            var onCanceled = refreshTask.ContinueWith(
                _ => this.ShowWarning("Operation was cancelled."),
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnCanceled);

            var onError = refreshTask.ContinueWith(
                t => this.ShowError(t.Exception),
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);

            Task.WhenAny(onError, onCanceled, onSuccess)
                .ContinueWith(_ => EndLoading(), TaskContinuationOptions.ExecuteSynchronously);
        }

        private async Task<IEnumerable<Record>> RefreshAsync()
        {
            BeginLoading();

            return await storage.GetRecordsAsync(tokenSource.Token);
        }

        private void BeginLoading()
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void EndLoading()
        {
            Cursor = Cursors.Default;
            Enabled = true;
        }
    }
}
