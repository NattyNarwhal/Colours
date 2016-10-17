namespace Colours
{
    partial class SortForm
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
            this.sortByLabel = new System.Windows.Forms.Label();
            this.sortByBox = new System.Windows.Forms.ComboBox();
            this.orderBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sortByLabel
            // 
            this.sortByLabel.AutoSize = true;
            this.sortByLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.sortByLabel.Location = new System.Drawing.Point(12, 15);
            this.sortByLabel.Name = "sortByLabel";
            this.sortByLabel.Size = new System.Drawing.Size(43, 13);
            this.sortByLabel.TabIndex = 0;
            this.sortByLabel.Text = "&Sort by:";
            // 
            // sortByBox
            // 
            this.sortByBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sortByBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortByBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.sortByBox.FormattingEnabled = true;
            this.sortByBox.Location = new System.Drawing.Point(61, 12);
            this.sortByBox.Name = "sortByBox";
            this.sortByBox.Size = new System.Drawing.Size(211, 21);
            this.sortByBox.TabIndex = 1;
            // 
            // orderBox
            // 
            this.orderBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.orderBox.AutoSize = true;
            this.orderBox.Checked = true;
            this.orderBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.orderBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.orderBox.Location = new System.Drawing.Point(12, 42);
            this.orderBox.Name = "orderBox";
            this.orderBox.Size = new System.Drawing.Size(82, 18);
            this.orderBox.TabIndex = 2;
            this.orderBox.Text = "&Ascending";
            this.orderBox.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(116, 39);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "O&K";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(197, 39);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // SortForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 74);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.orderBox);
            this.Controls.Add(this.sortByBox);
            this.Controls.Add(this.sortByLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SortForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Sort By";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sortByLabel;
        private System.Windows.Forms.ComboBox sortByBox;
        private System.Windows.Forms.CheckBox orderBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}