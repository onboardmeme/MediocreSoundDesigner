namespace MediocreSoundDesigner;

partial class FrmInstruments
{
    private System.ComponentModel.IContainer components = null;

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing) { StopPreview(); components?.Dispose(); }
        base.Dispose(disposing);
    }
    /// <summary>
    /// Initializes the form components.
    /// </summary>
    private void InitializeComponent()
    {
        lblTitle = new Label();
        lstInstruments = new ListBox();
        lblHint = new Label();
        btnClose = new Button();
        btnPreview = new Button();
        btnStop = new Button();
        btnAdd = new Button();
        SuspendLayout();
        lblTitle.AutoSize = true;
        lblTitle.Location = new Point(15, 15);
        lblTitle.Name = "lblTitle";
        lblTitle.Text = "Available instruments";
        lstInstruments.Location = new Point(15, 40);
        lstInstruments.Size = new Size(290, 124);
        lstInstruments.Name = "lstInstruments";
        lstInstruments.Items.AddRange(new object[] {
            "Snare Drum", "Snare Rimshot", "Snare Cross-Stick",
            "Ride Cymbal", "Ride Bell", "Pedal Hi-Hat"
        });
        lstInstruments.TabIndex = 0;
        lblHint.Location = new Point(15, 175);
        lblHint.Size = new Size(290, 35);
        lblHint.Name = "lblHint";
        lblHint.Text = "Select an instrument to preview or add as a new track.";
        btnPreview.Location = new Point(15, 220);
        btnPreview.Size = new Size(85, 28);
        btnPreview.Name = "btnPreview";
        btnPreview.Text = "Preview";
        btnPreview.Click += btnPreview_Click;
        btnStop.Location = new Point(110, 220);
        btnStop.Size = new Size(85, 28);
        btnStop.Name = "btnStop";
        btnStop.Text = "Stop";
        btnStop.Click += btnStop_Click;
        btnAdd.Location = new Point(205, 220);
        btnAdd.Size = new Size(100, 28);
        btnAdd.Name = "btnAdd";
        btnAdd.Text = "Add to Audio";
        btnAdd.Click += btnAdd_Click;
        btnClose.Location = new Point(230, 258);
        btnClose.Size = new Size(75, 28);
        btnClose.Name = "btnClose";
        btnClose.Text = "Close";
        btnClose.DialogResult = DialogResult.Cancel;
        btnClose.TabIndex = 1;
        CancelButton = btnClose;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(320, 300);
        Controls.Add(lblTitle);
        Controls.Add(lstInstruments);
        Controls.Add(lblHint);
        Controls.Add(btnClose);
        Controls.Add(btnPreview);
        Controls.Add(btnStop);
        Controls.Add(btnAdd);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Name = "FrmInstruments";
        Text = "Instruments";
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblTitle;
    private ListBox lstInstruments;
    private Label lblHint;
    private Button btnClose;
    private Button btnPreview;
    private Button btnStop;
    private Button btnAdd;
}
