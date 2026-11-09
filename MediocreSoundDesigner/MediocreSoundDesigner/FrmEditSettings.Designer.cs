namespace MediocreSoundDesigner {
  partial class FrmEditSettings {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
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
            btnApply = new Button();
            btnCancel = new Button();
            colorDialog = new ColorDialog();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnColorWinBg = new Button();
            btnColorWinFg = new Button();
            btnColorTrackSamples = new Button();
            btnColorTrackSamplesDisabled = new Button();
            btnColorTrackBgSelected = new Button();
            btnColorTrackBgUnselected = new Button();
            btnPlayline = new Button();
            label7 = new Label();
            btnRevertToDefault = new Button();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnApply
            // 
            btnApply.BackColor = Color.FromArgb(64, 64, 64);
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(347, 461);
            btnApply.Margin = new Padding(7);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(91, 33);
            btnApply.TabIndex = 2;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // btnCancel
            // 
            btnCancel.AutoSize = true;
            btnCancel.BackColor = Color.FromArgb(64, 64, 64);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(17, 461);
            btnCancel.Margin = new Padding(7);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(91, 33);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, 32);
            label1.Name = "label1";
            label1.Size = new Size(152, 20);
            label1.TabIndex = 4;
            label1.Text = "Window Background";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(20, 78);
            label2.Name = "label2";
            label2.Size = new Size(138, 20);
            label2.TabIndex = 5;
            label2.Text = "Window Text Color";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(20, 124);
            label3.Name = "label3";
            label3.Size = new Size(146, 20);
            label3.TabIndex = 6;
            label3.Text = "Track Samples Color";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(20, 170);
            label4.Name = "label4";
            label4.Size = new Size(203, 20);
            label4.TabIndex = 7;
            label4.Text = "Track Samples Color Inactive";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(20, 216);
            label5.Name = "label5";
            label5.Size = new Size(193, 20);
            label5.TabIndex = 8;
            label5.Text = "Track Selected Background";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(20, 262);
            label6.Name = "label6";
            label6.Size = new Size(211, 20);
            label6.TabIndex = 9;
            label6.Text = "Track Unselected Background";
            label6.Click += label6_Click;
            // 
            // btnColorWinBg
            // 
            btnColorWinBg.BackColor = Color.Red;
            btnColorWinBg.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            btnColorWinBg.FlatStyle = FlatStyle.Flat;
            btnColorWinBg.ForeColor = Color.Transparent;
            btnColorWinBg.Location = new Point(378, 28);
            btnColorWinBg.Name = "btnColorWinBg";
            btnColorWinBg.Size = new Size(40, 28);
            btnColorWinBg.TabIndex = 10;
            btnColorWinBg.UseVisualStyleBackColor = false;
            btnColorWinBg.Click += UpdateColorFromUserSelection;
            // 
            // btnColorWinFg
            // 
            btnColorWinFg.BackColor = Color.Red;
            btnColorWinFg.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            btnColorWinFg.FlatStyle = FlatStyle.Flat;
            btnColorWinFg.Location = new Point(378, 74);
            btnColorWinFg.Name = "btnColorWinFg";
            btnColorWinFg.Size = new Size(40, 28);
            btnColorWinFg.TabIndex = 11;
            btnColorWinFg.UseVisualStyleBackColor = false;
            btnColorWinFg.Click += UpdateColorFromUserSelection;
            // 
            // btnColorTrackSamples
            // 
            btnColorTrackSamples.BackColor = Color.Red;
            btnColorTrackSamples.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            btnColorTrackSamples.FlatStyle = FlatStyle.Flat;
            btnColorTrackSamples.Location = new Point(378, 120);
            btnColorTrackSamples.Name = "btnColorTrackSamples";
            btnColorTrackSamples.Size = new Size(40, 28);
            btnColorTrackSamples.TabIndex = 12;
            btnColorTrackSamples.UseVisualStyleBackColor = false;
            btnColorTrackSamples.Click += UpdateColorFromUserSelection;
            // 
            // btnColorTrackSamplesDisabled
            // 
            btnColorTrackSamplesDisabled.BackColor = Color.Red;
            btnColorTrackSamplesDisabled.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            btnColorTrackSamplesDisabled.FlatStyle = FlatStyle.Flat;
            btnColorTrackSamplesDisabled.Location = new Point(378, 166);
            btnColorTrackSamplesDisabled.Name = "btnColorTrackSamplesDisabled";
            btnColorTrackSamplesDisabled.Size = new Size(40, 28);
            btnColorTrackSamplesDisabled.TabIndex = 13;
            btnColorTrackSamplesDisabled.UseVisualStyleBackColor = false;
            btnColorTrackSamplesDisabled.Click += UpdateColorFromUserSelection;
            // 
            // btnColorTrackBgSelected
            // 
            btnColorTrackBgSelected.BackColor = Color.Red;
            btnColorTrackBgSelected.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            btnColorTrackBgSelected.FlatStyle = FlatStyle.Flat;
            btnColorTrackBgSelected.Location = new Point(378, 212);
            btnColorTrackBgSelected.Name = "btnColorTrackBgSelected";
            btnColorTrackBgSelected.Size = new Size(40, 28);
            btnColorTrackBgSelected.TabIndex = 14;
            btnColorTrackBgSelected.UseVisualStyleBackColor = false;
            btnColorTrackBgSelected.Click += UpdateColorFromUserSelection;
            // 
            // btnColorTrackBgUnselected
            // 
            btnColorTrackBgUnselected.BackColor = Color.Red;
            btnColorTrackBgUnselected.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            btnColorTrackBgUnselected.FlatStyle = FlatStyle.Flat;
            btnColorTrackBgUnselected.Location = new Point(378, 258);
            btnColorTrackBgUnselected.Name = "btnColorTrackBgUnselected";
            btnColorTrackBgUnselected.Size = new Size(40, 28);
            btnColorTrackBgUnselected.TabIndex = 15;
            btnColorTrackBgUnselected.UseVisualStyleBackColor = false;
            btnColorTrackBgUnselected.Click += UpdateColorFromUserSelection;
            // 
            // btnPlayline
            // 
            btnPlayline.BackColor = Color.Red;
            btnPlayline.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            btnPlayline.FlatStyle = FlatStyle.Flat;
            btnPlayline.Location = new Point(378, 304);
            btnPlayline.Name = "btnPlayline";
            btnPlayline.Size = new Size(40, 28);
            btnPlayline.TabIndex = 17;
            btnPlayline.UseVisualStyleBackColor = false;
            btnPlayline.Click += UpdateColorFromUserSelection;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(20, 308);
            label7.Name = "label7";
            label7.Size = new Size(70, 20);
            label7.TabIndex = 16;
            label7.Text = "Play Line";
            // 
            // btnRevertToDefault
            // 
            btnRevertToDefault.AutoSize = true;
            btnRevertToDefault.BackColor = Color.FromArgb(64, 64, 64);
            btnRevertToDefault.FlatAppearance.BorderSize = 0;
            btnRevertToDefault.FlatStyle = FlatStyle.Flat;
            btnRevertToDefault.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRevertToDefault.ForeColor = Color.White;
            btnRevertToDefault.Location = new Point(155, 461);
            btnRevertToDefault.Margin = new Padding(7);
            btnRevertToDefault.Name = "btnRevertToDefault";
            btnRevertToDefault.Size = new Size(154, 33);
            btnRevertToDefault.TabIndex = 18;
            btnRevertToDefault.Text = "Revert To Default";
            btnRevertToDefault.UseVisualStyleBackColor = false;
            btnRevertToDefault.Click += btnRevertToDefault_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DimGray;
            panel1.Controls.Add(btnPlayline);
            panel1.Controls.Add(btnRevertToDefault);
            panel1.Controls.Add(btnColorTrackBgUnselected);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(btnColorTrackBgSelected);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnColorTrackSamplesDisabled);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(btnColorTrackSamples);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(btnColorWinFg);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(btnColorWinBg);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btnApply);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(454, 523);
            panel1.TabIndex = 19;
            // 
            // FrmEditSettings
            // 
            AutoScaleDimensions = new SizeF(16F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            ClientSize = new Size(478, 547);
            Controls.Add(panel1);
            Margin = new Padding(16, 17, 16, 17);
            Name = "FrmEditSettings";
            Text = "FrmEditVolume";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btnApply;
    private Button btnCancel;
    private ColorDialog colorDialog;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Button btnColorWinBg;
    private Button btnColorWinFg;
    private Button btnColorTrackSamples;
    private Button btnColorTrackSamplesDisabled;
    private Button btnColorTrackBgSelected;
    private Button btnColorTrackBgUnselected;
    private Button btnPlayline;
    private Label label7;
    private Button btnRevertToDefault;
        private Panel panel1;
    }
}