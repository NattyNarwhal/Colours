namespace Colours
{
    partial class PalettePropertiesForm
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.titleBox = new System.Windows.Forms.TextBox();
            this.columnsLabel = new System.Windows.Forms.Label();
            this.columnsBox = new System.Windows.Forms.NumericUpDown();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.commentsBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.commonTab = new System.Windows.Forms.TabPage();
            this.gimpTab = new System.Windows.Forms.TabPage();
            this.acbTab = new System.Windows.Forms.TabPage();
            this.spotProcessBox = new System.Windows.Forms.ComboBox();
            this.spotProcessLabel = new System.Windows.Forms.Label();
            this.colorspaceBox = new System.Windows.Forms.ComboBox();
            this.colorspaceLabel = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.postfixBox = new System.Windows.Forms.TextBox();
            this.postfixLabel = new System.Windows.Forms.Label();
            this.prefixBox = new System.Windows.Forms.TextBox();
            this.prefixLabel = new System.Windows.Forms.Label();
            this.defaultColorBox = new System.Windows.Forms.NumericUpDown();
            this.defaultColorLabel = new System.Windows.Forms.Label();
            this.idBox = new System.Windows.Forms.NumericUpDown();
            this.idLabel = new System.Windows.Forms.Label();
            this.actTab = new System.Windows.Forms.TabPage();
            this.transparencyIndexBox = new System.Windows.Forms.NumericUpDown();
            this.transparencyEnabledBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.columnsBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.commonTab.SuspendLayout();
            this.gimpTab.SuspendLayout();
            this.acbTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultColorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idBox)).BeginInit();
            this.actTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyIndexBox)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(6, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(27, 13);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "&Title";
            // 
            // titleBox
            // 
            this.titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBox.Location = new System.Drawing.Point(68, 6);
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(271, 20);
            this.titleBox.TabIndex = 1;
            // 
            // columnsLabel
            // 
            this.columnsLabel.AutoSize = true;
            this.columnsLabel.Location = new System.Drawing.Point(6, 34);
            this.columnsLabel.Name = "columnsLabel";
            this.columnsLabel.Size = new System.Drawing.Size(47, 13);
            this.columnsLabel.TabIndex = 2;
            this.columnsLabel.Text = "Co&lumns";
            // 
            // columnsBox
            // 
            this.columnsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.columnsBox.Location = new System.Drawing.Point(68, 32);
            this.columnsBox.Name = "columnsBox";
            this.columnsBox.Size = new System.Drawing.Size(271, 20);
            this.columnsBox.TabIndex = 3;
            // 
            // commentsLabel
            // 
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.Location = new System.Drawing.Point(8, 9);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(56, 13);
            this.commentsLabel.TabIndex = 4;
            this.commentsLabel.Text = "Co&mments";
            // 
            // commentsBox
            // 
            this.commentsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commentsBox.Location = new System.Drawing.Point(70, 6);
            this.commentsBox.Multiline = true;
            this.commentsBox.Name = "commentsBox";
            this.commentsBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.commentsBox.Size = new System.Drawing.Size(269, 182);
            this.commentsBox.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(209, 238);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "O&K";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(290, 238);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.commonTab);
            this.tabControl1.Controls.Add(this.gimpTab);
            this.tabControl1.Controls.Add(this.acbTab);
            this.tabControl1.Controls.Add(this.actTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(353, 220);
            this.tabControl1.TabIndex = 8;
            // 
            // commonTab
            // 
            this.commonTab.Controls.Add(this.titleLabel);
            this.commonTab.Controls.Add(this.titleBox);
            this.commonTab.Controls.Add(this.columnsLabel);
            this.commonTab.Controls.Add(this.columnsBox);
            this.commonTab.Location = new System.Drawing.Point(4, 22);
            this.commonTab.Name = "commonTab";
            this.commonTab.Padding = new System.Windows.Forms.Padding(3);
            this.commonTab.Size = new System.Drawing.Size(345, 194);
            this.commonTab.TabIndex = 0;
            this.commonTab.Text = "General";
            this.commonTab.UseVisualStyleBackColor = true;
            // 
            // gimpTab
            // 
            this.gimpTab.Controls.Add(this.commentsLabel);
            this.gimpTab.Controls.Add(this.commentsBox);
            this.gimpTab.Location = new System.Drawing.Point(4, 22);
            this.gimpTab.Name = "gimpTab";
            this.gimpTab.Padding = new System.Windows.Forms.Padding(3);
            this.gimpTab.Size = new System.Drawing.Size(345, 194);
            this.gimpTab.TabIndex = 1;
            this.gimpTab.Text = "GIMP Specific";
            this.gimpTab.UseVisualStyleBackColor = true;
            // 
            // acbTab
            // 
            this.acbTab.Controls.Add(this.spotProcessBox);
            this.acbTab.Controls.Add(this.spotProcessLabel);
            this.acbTab.Controls.Add(this.colorspaceBox);
            this.acbTab.Controls.Add(this.colorspaceLabel);
            this.acbTab.Controls.Add(this.descriptionBox);
            this.acbTab.Controls.Add(this.descriptionLabel);
            this.acbTab.Controls.Add(this.postfixBox);
            this.acbTab.Controls.Add(this.postfixLabel);
            this.acbTab.Controls.Add(this.prefixBox);
            this.acbTab.Controls.Add(this.prefixLabel);
            this.acbTab.Controls.Add(this.defaultColorBox);
            this.acbTab.Controls.Add(this.defaultColorLabel);
            this.acbTab.Controls.Add(this.idBox);
            this.acbTab.Controls.Add(this.idLabel);
            this.acbTab.Location = new System.Drawing.Point(4, 22);
            this.acbTab.Name = "acbTab";
            this.acbTab.Padding = new System.Windows.Forms.Padding(3);
            this.acbTab.Size = new System.Drawing.Size(345, 194);
            this.acbTab.TabIndex = 2;
            this.acbTab.Text = "Color Book Specific";
            this.acbTab.UseVisualStyleBackColor = true;
            // 
            // spotProcessBox
            // 
            this.spotProcessBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spotProcessBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spotProcessBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.spotProcessBox.FormattingEnabled = true;
            this.spotProcessBox.Location = new System.Drawing.Point(107, 163);
            this.spotProcessBox.Name = "spotProcessBox";
            this.spotProcessBox.Size = new System.Drawing.Size(232, 21);
            this.spotProcessBox.TabIndex = 13;
            // 
            // spotProcessLabel
            // 
            this.spotProcessLabel.AutoSize = true;
            this.spotProcessLabel.Location = new System.Drawing.Point(9, 166);
            this.spotProcessLabel.Name = "spotProcessLabel";
            this.spotProcessLabel.Size = new System.Drawing.Size(72, 13);
            this.spotProcessLabel.TabIndex = 12;
            this.spotProcessLabel.Text = "&Spot/Process";
            // 
            // colorspaceBox
            // 
            this.colorspaceBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorspaceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorspaceBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.colorspaceBox.FormattingEnabled = true;
            this.colorspaceBox.Location = new System.Drawing.Point(107, 136);
            this.colorspaceBox.Name = "colorspaceBox";
            this.colorspaceBox.Size = new System.Drawing.Size(232, 21);
            this.colorspaceBox.TabIndex = 11;
            // 
            // colorspaceLabel
            // 
            this.colorspaceLabel.AutoSize = true;
            this.colorspaceLabel.Location = new System.Drawing.Point(9, 139);
            this.colorspaceLabel.Name = "colorspaceLabel";
            this.colorspaceLabel.Size = new System.Drawing.Size(63, 13);
            this.colorspaceLabel.TabIndex = 10;
            this.colorspaceLabel.Text = "&Color space";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.Location = new System.Drawing.Point(107, 110);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(232, 20);
            this.descriptionBox.TabIndex = 9;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(6, 113);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 8;
            this.descriptionLabel.Text = "&Description";
            // 
            // postfixBox
            // 
            this.postfixBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.postfixBox.Location = new System.Drawing.Point(107, 84);
            this.postfixBox.Name = "postfixBox";
            this.postfixBox.Size = new System.Drawing.Size(232, 20);
            this.postfixBox.TabIndex = 7;
            // 
            // postfixLabel
            // 
            this.postfixLabel.AutoSize = true;
            this.postfixLabel.Location = new System.Drawing.Point(6, 87);
            this.postfixLabel.Name = "postfixLabel";
            this.postfixLabel.Size = new System.Drawing.Size(38, 13);
            this.postfixLabel.TabIndex = 6;
            this.postfixLabel.Text = "Pos&tfix";
            // 
            // prefixBox
            // 
            this.prefixBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prefixBox.Location = new System.Drawing.Point(107, 58);
            this.prefixBox.Name = "prefixBox";
            this.prefixBox.Size = new System.Drawing.Size(232, 20);
            this.prefixBox.TabIndex = 5;
            // 
            // prefixLabel
            // 
            this.prefixLabel.AutoSize = true;
            this.prefixLabel.Location = new System.Drawing.Point(6, 61);
            this.prefixLabel.Name = "prefixLabel";
            this.prefixLabel.Size = new System.Drawing.Size(33, 13);
            this.prefixLabel.TabIndex = 4;
            this.prefixLabel.Text = "P&refix";
            // 
            // defaultColorBox
            // 
            this.defaultColorBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultColorBox.Location = new System.Drawing.Point(107, 32);
            this.defaultColorBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.defaultColorBox.Name = "defaultColorBox";
            this.defaultColorBox.Size = new System.Drawing.Size(232, 20);
            this.defaultColorBox.TabIndex = 3;
            // 
            // defaultColorLabel
            // 
            this.defaultColorLabel.AutoSize = true;
            this.defaultColorLabel.Location = new System.Drawing.Point(6, 34);
            this.defaultColorLabel.Name = "defaultColorLabel";
            this.defaultColorLabel.Size = new System.Drawing.Size(95, 13);
            this.defaultColorLabel.TabIndex = 2;
            this.defaultColorLabel.Text = "Default color inde&x";
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(107, 6);
            this.idBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(232, 20);
            this.idBox.TabIndex = 1;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.idLabel.Location = new System.Drawing.Point(6, 8);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(18, 13);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "&ID";
            // 
            // actTab
            // 
            this.actTab.Controls.Add(this.transparencyIndexBox);
            this.actTab.Controls.Add(this.transparencyEnabledBox);
            this.actTab.Location = new System.Drawing.Point(4, 22);
            this.actTab.Name = "actTab";
            this.actTab.Padding = new System.Windows.Forms.Padding(3);
            this.actTab.Size = new System.Drawing.Size(345, 194);
            this.actTab.TabIndex = 3;
            this.actTab.Text = "Color Table Specific";
            this.actTab.UseVisualStyleBackColor = true;
            // 
            // transparencyIndexBox
            // 
            this.transparencyIndexBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transparencyIndexBox.Enabled = false;
            this.transparencyIndexBox.Location = new System.Drawing.Point(133, 6);
            this.transparencyIndexBox.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.transparencyIndexBox.Name = "transparencyIndexBox";
            this.transparencyIndexBox.Size = new System.Drawing.Size(197, 20);
            this.transparencyIndexBox.TabIndex = 1;
            // 
            // transparencyEnabledBox
            // 
            this.transparencyEnabledBox.AutoSize = true;
            this.transparencyEnabledBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.transparencyEnabledBox.Location = new System.Drawing.Point(6, 6);
            this.transparencyEnabledBox.Name = "transparencyEnabledBox";
            this.transparencyEnabledBox.Size = new System.Drawing.Size(121, 18);
            this.transparencyEnabledBox.TabIndex = 0;
            this.transparencyEnabledBox.Text = "&Transparent colour";
            this.transparencyEnabledBox.UseVisualStyleBackColor = true;
            // 
            // PalettePropertiesForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(377, 273);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PalettePropertiesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Palette Properties";
            ((System.ComponentModel.ISupportInitialize)(this.columnsBox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.commonTab.ResumeLayout(false);
            this.commonTab.PerformLayout();
            this.gimpTab.ResumeLayout(false);
            this.gimpTab.PerformLayout();
            this.acbTab.ResumeLayout(false);
            this.acbTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultColorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idBox)).EndInit();
            this.actTab.ResumeLayout(false);
            this.actTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyIndexBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox titleBox;
        private System.Windows.Forms.Label columnsLabel;
        private System.Windows.Forms.NumericUpDown columnsBox;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.TextBox commentsBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage commonTab;
        private System.Windows.Forms.TabPage gimpTab;
        private System.Windows.Forms.TabPage acbTab;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.NumericUpDown idBox;
        private System.Windows.Forms.Label defaultColorLabel;
        private System.Windows.Forms.TextBox postfixBox;
        private System.Windows.Forms.Label postfixLabel;
        private System.Windows.Forms.TextBox prefixBox;
        private System.Windows.Forms.Label prefixLabel;
        private System.Windows.Forms.NumericUpDown defaultColorBox;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.ComboBox colorspaceBox;
        private System.Windows.Forms.Label colorspaceLabel;
        private System.Windows.Forms.ComboBox spotProcessBox;
        private System.Windows.Forms.Label spotProcessLabel;
        private System.Windows.Forms.TabPage actTab;
        private System.Windows.Forms.NumericUpDown transparencyIndexBox;
        private System.Windows.Forms.CheckBox transparencyEnabledBox;
    }
}