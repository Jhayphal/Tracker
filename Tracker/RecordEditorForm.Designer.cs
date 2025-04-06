namespace Tracker
{
    partial class RecordEditorForm
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lDescription = new System.Windows.Forms.Label();
            this.lTotal = new System.Windows.Forms.Label();
            this.lComment = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.flpActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.flpActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lDescription, 0, 0);
            this.tlpMain.Controls.Add(this.lTotal, 0, 1);
            this.tlpMain.Controls.Add(this.lComment, 0, 2);
            this.tlpMain.Controls.Add(this.tbDescription, 1, 0);
            this.tlpMain.Controls.Add(this.tbComment, 1, 2);
            this.tlpMain.Controls.Add(this.numericUpDown1, 1, 1);
            this.tlpMain.Controls.Add(this.flpActions, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(8, 8);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(440, 494);
            this.tlpMain.TabIndex = 0;
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(3, 0);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(75, 16);
            this.lDescription.TabIndex = 0;
            this.lDescription.Text = "Description";
            // 
            // lTotal
            // 
            this.lTotal.AutoSize = true;
            this.lTotal.Location = new System.Drawing.Point(3, 215);
            this.lTotal.Name = "lTotal";
            this.lTotal.Size = new System.Drawing.Size(38, 16);
            this.lTotal.TabIndex = 1;
            this.lTotal.Text = "Total";
            // 
            // lComment
            // 
            this.lComment.AutoSize = true;
            this.lComment.Location = new System.Drawing.Point(3, 243);
            this.lComment.Name = "lComment";
            this.lComment.Size = new System.Drawing.Size(64, 16);
            this.lComment.TabIndex = 2;
            this.lComment.Text = "Comment";
            // 
            // tbDescription
            // 
            this.tbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDescription.Location = new System.Drawing.Point(84, 3);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(353, 209);
            this.tbDescription.TabIndex = 3;
            // 
            // tbComment
            // 
            this.tbComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbComment.Location = new System.Drawing.Point(84, 246);
            this.tbComment.Multiline = true;
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(353, 209);
            this.tbComment.TabIndex = 4;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Location = new System.Drawing.Point(84, 218);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(353, 22);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // flpActions
            // 
            this.tlpMain.SetColumnSpan(this.flpActions, 2);
            this.flpActions.Controls.Add(this.btnCancel);
            this.flpActions.Controls.Add(this.btnOk);
            this.flpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpActions.Location = new System.Drawing.Point(3, 461);
            this.flpActions.Name = "flpActions";
            this.flpActions.Size = new System.Drawing.Size(434, 30);
            this.flpActions.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(356, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(275, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // RecordEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 510);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(200, 300);
            this.Name = "RecordEditorForm";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Record Editor";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.flpActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.Label lTotal;
        private System.Windows.Forms.Label lComment;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.FlowLayoutPanel flpActions;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}