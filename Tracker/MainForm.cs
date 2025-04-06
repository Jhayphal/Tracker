using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tracker
{
    public partial class MainForm : Form
    {
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly Properties.Settings settings = Properties.Settings.Default;
        private readonly Button[] actionButtons;

        private DataStorage storage;

        private volatile bool loading;

        public MainForm()
        {
            InitializeComponent();

            actionButtons = tlpContent.Controls
                .OfType<Button>()
                .Where(c => c != btnSettings)
                .ToArray();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                TryUpdateSettings();
            }

            storage = new DataStorage(settings.ConnectionString);

            RefreshData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tokenSource.Cancel();

            int triesCount = 10;

            while (loading && triesCount-- > 0)
            {
                Thread.Sleep(40);
            }
        }

        private void dgvRecords_SelectionChanged(object sender, EventArgs e)
        {
            UpdateActionsState();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new RecordEditorForm().ShowDialog(this);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (TryUpdateSettings())
            {
                storage = new DataStorage(settings.ConnectionString);

                RefreshData();
            }
        }

        private void BeginLoading()
        {
            loading = true;
            Cursor = Cursors.WaitCursor;

            tlpContent.Enabled = false;
        }

        private void EndLoading()
        {
            tlpContent.Enabled = true;

            Cursor = Cursors.Default;
            loading = false;

            UpdateActionsState();
        }

        private void RefreshData()
        {
            BeginLoading();

            dgvRecords.DataSource = new List<Record>();

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

        private bool TryUpdateSettings()
        {
            BeginLoading();

            try
            {
                var settingsForm = new SettingsForm(settings.ConnectionString);
                if (settingsForm.ShowDialog(this) == DialogResult.OK)
                {
                    settings.ConnectionString = settingsForm.ConnectionString;
                    settings.Save();

                    return true;
                }

                return false;
            }
            finally
            {
                EndLoading();
            }
        }

        private void UpdateActionsState()
        {
            var shouldBeEnabled = dgvRecords.SelectedRows.Count > 0;
            foreach (var button in actionButtons)
            {
                button.Enabled = shouldBeEnabled;
            }
        }
    }
}
