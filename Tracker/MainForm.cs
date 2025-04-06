using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private DataStorage storage;
        private ObservableCollection<Record> records;

        private volatile bool loading;

        public MainForm()
        {
            InitializeComponent();
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
            using (var editor = new RecordEditorForm())
            {
                while (editor.ShowDialog(this) == DialogResult.OK)
                {
                    var newRecord = new Record
                    {
                        Description = editor.Description,
                        Total = editor.Total,
                        Comment = editor.Comment
                    };

                    if (storage.TryCreateRecord(newRecord, out var exception))
                    {
                        RefreshData();

                        break;
                    }
                    else
                    {
                        this.ShowError(exception);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var currentRecord = records[dgvRecords.SelectedRows[0].Index];

            using (var editor = new RecordEditorForm(currentRecord))
            {
                while (editor.ShowDialog(this) == DialogResult.OK)
                {
                    var modifiedRecord = new Record
                    {
                        Id = currentRecord.Id,
                        Description = editor.Description,
                        Total = editor.Total,
                        Comment = editor.Comment
                    };

                    if (storage.TryUpdateRecord(modifiedRecord, out Exception exception))
                    {
                        RefreshData();

                        break;
                    }
                    else
                    {
                        this.ShowError(exception);
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var currentRecord = records[dgvRecords.SelectedRows[0].Index];

            if (storage.TryRemoveRecord(currentRecord.Id, out Exception exception))
            {
                RefreshData();
            }
            else
            {
                this.ShowError(exception);
            }
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

            SetRecords(Array.Empty<Record>());

            var refreshTask = storage.GetRecordsAsync(tokenSource.Token);

            var onSuccess = refreshTask.ContinueWith(
                t => SetRecords(t.Result),
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
                using (var settingsForm = new SettingsForm(settings.ConnectionString))
                {
                    if (settingsForm.ShowDialog(this) == DialogResult.OK)
                    {
                        settings.ConnectionString = settingsForm.ConnectionString;
                        settings.Save();

                        return true;
                    }
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
            if (dgvRecords.SelectedRows.Count > 0)
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
            }
        }

        private void SetRecords(IEnumerable<Record> records)
        {
            dgvRecords.DataSource = this.records = new ObservableCollection<Record>(records);
        }
    }
}
