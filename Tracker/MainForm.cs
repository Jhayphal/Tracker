using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tracker
{
    public partial class MainForm : Form
    {
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private DataStorage storage;

        private volatile bool loading;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
            => RefreshData();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tokenSource.Cancel();

            int triesCount = 10;

            while (loading && triesCount-- > 0)
            {
                Thread.Sleep(40);
            }
        }

        private void BeginLoading()
        {
            loading = true;
            Cursor = Cursors.WaitCursor;

            tlpContent.Enabled = false;

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.ConnectionString))
            {
                new SettingsForm().ShowDialog(this);
            }

            storage = new DataStorage(Properties.Settings.Default.ConnectionString);
        }

        private void EndLoading()
        {
            tlpContent.Enabled = true;

            Cursor = Cursors.Default;
            loading = false;
        }

        private void RefreshData()
        {
            BeginLoading();

            var refreshTask = storage.GetRecordsAsync(tokenSource.Token);

            var onSuccess = refreshTask.ContinueWith(
                t => dgvRecords.DataSource = t.Result,
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion);

            var onCanceled = refreshTask.ContinueWith(
                _ => { },
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnCanceled);

            var onError = refreshTask.ContinueWith(
                t => this.ShowError(t.Exception),
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);

            Task.WhenAny(onSuccess, onCanceled, onError)
                .ContinueWith(_ => EndLoading(), TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}
