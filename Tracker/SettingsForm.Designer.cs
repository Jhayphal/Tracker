namespace Tracker
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpParameters = new System.Windows.Forms.TableLayoutPanel();
            this.pgConnectionSettings = new System.Windows.Forms.PropertyGrid();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tlpParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpParameters
            // 
            this.tlpParameters.ColumnCount = 3;
            this.tlpParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpParameters.Controls.Add(this.pgConnectionSettings, 0, 0);
            this.tlpParameters.Controls.Add(this.btnTestConnection, 0, 1);
            this.tlpParameters.Controls.Add(this.btnOk, 1, 1);
            this.tlpParameters.Controls.Add(this.btnCancel, 2, 1);
            this.tlpParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpParameters.Location = new System.Drawing.Point(0, 0);
            this.tlpParameters.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tlpParameters.Name = "tlpParameters";
            this.tlpParameters.RowCount = 3;
            this.tlpParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpParameters.Size = new System.Drawing.Size(496, 492);
            this.tlpParameters.TabIndex = 0;
            // 
            // pgConnectionSettings
            // 
            this.tlpParameters.SetColumnSpan(this.pgConnectionSettings, 3);
            this.pgConnectionSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgConnectionSettings.Location = new System.Drawing.Point(2, 2);
            this.pgConnectionSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pgConnectionSettings.Name = "pgConnectionSettings";
            this.pgConnectionSettings.Size = new System.Drawing.Size(492, 460);
            this.pgConnectionSettings.TabIndex = 0;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTestConnection.Location = new System.Drawing.Point(2, 466);
            this.btnTestConnection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(161, 24);
            this.btnTestConnection.TabIndex = 1;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOk.Location = new System.Drawing.Point(167, 466);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(161, 24);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(332, 466);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(162, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(496, 492);
            this.Controls.Add(this.tlpParameters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application Settings";
            this.tlpParameters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpParameters;
        private System.Windows.Forms.PropertyGrid pgConnectionSettings;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}