namespace Colours
{
    partial class BlendForm
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
            Colours.RgbColor rgbColor1 = new Colours.RgbColor();
            Colours.RgbColor rgbColor2 = new Colours.RgbColor();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.stepsLabel = new System.Windows.Forms.Label();
            this.color1Label = new System.Windows.Forms.Label();
            this.colorButton1 = new Colours.ColorButton();
            this.stepsBox = new System.Windows.Forms.NumericUpDown();
            this.color2Label = new System.Windows.Forms.Label();
            this.colorButton2 = new Colours.ColorButton();
            this.paletteListImages = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.colourCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepsBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.stepsLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.color1Label, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.colorButton1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.stepsBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.color2Label, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.colorButton2, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(260, 41);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // stepsLabel
            // 
            this.stepsLabel.AutoSize = true;
            this.stepsLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.stepsLabel.Location = new System.Drawing.Point(89, 0);
            this.stepsLabel.Name = "stepsLabel";
            this.stepsLabel.Size = new System.Drawing.Size(34, 13);
            this.stepsLabel.TabIndex = 3;
            this.stepsLabel.Text = "S&teps";
            // 
            // color1Label
            // 
            this.color1Label.AutoSize = true;
            this.color1Label.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.color1Label.Location = new System.Drawing.Point(3, 0);
            this.color1Label.Name = "color1Label";
            this.color1Label.Size = new System.Drawing.Size(58, 13);
            this.color1Label.TabIndex = 0;
            this.color1Label.Text = "&First colour";
            // 
            // colorButton1
            // 
            this.colorButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            rgbColor1.B = ((byte)(240));
            rgbColor1.G = ((byte)(240));
            rgbColor1.R = ((byte)(240));
            this.colorButton1.Color = rgbColor1;
            this.colorButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colorButton1.ForeColor = System.Drawing.Color.Black;
            this.colorButton1.Location = new System.Drawing.Point(3, 16);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.Size = new System.Drawing.Size(80, 23);
            this.colorButton1.TabIndex = 1;
            this.colorButton1.UseVisualStyleBackColor = false;
            this.colorButton1.Click += new System.EventHandler(this.colorButton1_Click);
            // 
            // stepsBox
            // 
            this.stepsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepsBox.Location = new System.Drawing.Point(89, 16);
            this.stepsBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stepsBox.Name = "stepsBox";
            this.stepsBox.Size = new System.Drawing.Size(80, 20);
            this.stepsBox.TabIndex = 4;
            this.stepsBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stepsBox.ValueChanged += new System.EventHandler(this.stepsBox_ValueChanged);
            // 
            // color2Label
            // 
            this.color2Label.AutoSize = true;
            this.color2Label.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.color2Label.Location = new System.Drawing.Point(175, 0);
            this.color2Label.Name = "color2Label";
            this.color2Label.Size = new System.Drawing.Size(76, 13);
            this.color2Label.TabIndex = 5;
            this.color2Label.Text = "S&econd colour";
            // 
            // colorButton2
            // 
            this.colorButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            rgbColor2.B = ((byte)(240));
            rgbColor2.G = ((byte)(240));
            rgbColor2.R = ((byte)(240));
            this.colorButton2.Color = rgbColor2;
            this.colorButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorButton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colorButton2.ForeColor = System.Drawing.Color.Black;
            this.colorButton2.Location = new System.Drawing.Point(175, 16);
            this.colorButton2.Name = "colorButton2";
            this.colorButton2.Size = new System.Drawing.Size(82, 23);
            this.colorButton2.TabIndex = 6;
            this.colorButton2.UseVisualStyleBackColor = false;
            this.colorButton2.Click += new System.EventHandler(this.colorButton2_Click);
            // 
            // paletteListImages
            // 
            this.paletteListImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.paletteListImages.ImageSize = new System.Drawing.Size(16, 16);
            this.paletteListImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colourCol});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(12, 60);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(260, 160);
            this.listView1.SmallImageList = this.paletteListImages;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // colourCol
            // 
            this.colourCol.Text = "Colour";
            this.colourCol.Width = 120;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(116, 226);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "&Append";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(197, 226);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // BlendForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BlendForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Blend Colours";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepsBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label color1Label;
        private ColorButton colorButton1;
        private System.Windows.Forms.Label stepsLabel;
        private System.Windows.Forms.NumericUpDown stepsBox;
        private System.Windows.Forms.Label color2Label;
        private ColorButton colorButton2;
        private System.Windows.Forms.ImageList paletteListImages;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colourCol;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}