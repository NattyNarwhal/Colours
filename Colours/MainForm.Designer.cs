namespace Colours
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCSSRGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCSSHSLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.intensityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saturateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desaturateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyHexToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCSSRGBToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCSSHSLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Complement",
            "Split Complements",
            "Triads",
            "Tetrads",
            "Analogous",
            "Monochromatic"});
            this.comboBox1.Location = new System.Drawing.Point(12, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(260, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 54);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(260, 87);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyHexToolStripMenuItem,
            this.copyCSSRGBToolStripMenuItem,
            this.copyCSSHSLToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 70);
            // 
            // copyHexToolStripMenuItem
            // 
            this.copyHexToolStripMenuItem.Name = "copyHexToolStripMenuItem";
            this.copyHexToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyHexToolStripMenuItem.Text = "Copy He&x";
            this.copyHexToolStripMenuItem.Click += new System.EventHandler(this.copyHexToolStripMenuItem_Click);
            // 
            // copyCSSRGBToolStripMenuItem
            // 
            this.copyCSSRGBToolStripMenuItem.Name = "copyCSSRGBToolStripMenuItem";
            this.copyCSSRGBToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyCSSRGBToolStripMenuItem.Text = "Copy &CSS (RGB)";
            this.copyCSSRGBToolStripMenuItem.Click += new System.EventHandler(this.copyCSSRGBToolStripMenuItem_Click);
            // 
            // copyCSSHSLToolStripMenuItem
            // 
            this.copyCSSHSLToolStripMenuItem.Name = "copyCSSHSLToolStripMenuItem";
            this.copyCSSHSLToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyCSSHSLToolStripMenuItem.Text = "Copy CSS (&HSL)";
            this.copyCSSHSLToolStripMenuItem.Click += new System.EventHandler(this.copyCSSHSLToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem,
            this.intensityToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // intensityToolStripMenuItem
            // 
            this.intensityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brightenToolStripMenuItem,
            this.darkenToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saturateToolStripMenuItem,
            this.desaturateToolStripMenuItem});
            this.intensityToolStripMenuItem.Name = "intensityToolStripMenuItem";
            this.intensityToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.intensityToolStripMenuItem.Text = "&Intensity";
            // 
            // brightenToolStripMenuItem
            // 
            this.brightenToolStripMenuItem.Name = "brightenToolStripMenuItem";
            this.brightenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.brightenToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.brightenToolStripMenuItem.Text = "&Brighten";
            this.brightenToolStripMenuItem.Click += new System.EventHandler(this.brightenToolStripMenuItem_Click);
            // 
            // darkenToolStripMenuItem
            // 
            this.darkenToolStripMenuItem.Name = "darkenToolStripMenuItem";
            this.darkenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.darkenToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.darkenToolStripMenuItem.Text = "&Darken";
            this.darkenToolStripMenuItem.Click += new System.EventHandler(this.darkenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // saturateToolStripMenuItem
            // 
            this.saturateToolStripMenuItem.Name = "saturateToolStripMenuItem";
            this.saturateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.saturateToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.saturateToolStripMenuItem.Text = "&Saturate";
            this.saturateToolStripMenuItem.Click += new System.EventHandler(this.saturateToolStripMenuItem_Click);
            // 
            // desaturateToolStripMenuItem
            // 
            this.desaturateToolStripMenuItem.Name = "desaturateToolStripMenuItem";
            this.desaturateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.desaturateToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.desaturateToolStripMenuItem.Text = "&Desaturate";
            this.desaturateToolStripMenuItem.Click += new System.EventHandler(this.desaturateToolStripMenuItem_Click);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyHexToolStripMenuItem1,
            this.copyCSSRGBToolStripMenuItem1,
            this.copyCSSHSLToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.pasteToolStripMenuItem,
            this.randomToolStripMenuItem});
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.colorToolStripMenuItem.Text = "&Color";
            // 
            // randomToolStripMenuItem
            // 
            this.randomToolStripMenuItem.Name = "randomToolStripMenuItem";
            this.randomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.randomToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.randomToolStripMenuItem.Text = "&Random";
            this.randomToolStripMenuItem.Click += new System.EventHandler(this.randomButton_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // copyHexToolStripMenuItem1
            // 
            this.copyHexToolStripMenuItem1.Name = "copyHexToolStripMenuItem1";
            this.copyHexToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyHexToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.copyHexToolStripMenuItem1.Text = "Copy He&x";
            this.copyHexToolStripMenuItem1.Click += new System.EventHandler(this.copyHexToolStripMenuItem1_Click);
            // 
            // copyCSSRGBToolStripMenuItem1
            // 
            this.copyCSSRGBToolStripMenuItem1.Name = "copyCSSRGBToolStripMenuItem1";
            this.copyCSSRGBToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.copyCSSRGBToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.copyCSSRGBToolStripMenuItem1.Text = "Copy CSS (&RGB)";
            this.copyCSSRGBToolStripMenuItem1.Click += new System.EventHandler(this.copyCSSRGBToolStripMenuItem1_Click);
            // 
            // copyCSSHSLToolStripMenuItem1
            // 
            this.copyCSSHSLToolStripMenuItem1.Name = "copyCSSHSLToolStripMenuItem1";
            this.copyCSSHSLToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.copyCSSHSLToolStripMenuItem1.Text = "Copy CSS (&HSL)";
            this.copyCSSHSLToolStripMenuItem1.Click += new System.EventHandler(this.copyCSSHSLToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 153);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.comboBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyCSSRGBToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem copyCSSHSLToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem intensityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saturateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desaturateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyHexToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyCSSRGBToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyCSSHSLToolStripMenuItem1;
    }
}

