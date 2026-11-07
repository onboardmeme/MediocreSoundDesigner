namespace MediocreSoundDesigner;

partial class FrmAudioLibrary
{
    private System.ComponentModel.IContainer components = null;

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lstClips = new ListBox();
        btnAdd = new Button();
        btnClose = new Button();
        btnDelete = new Button();
        lblStatus = new Label();
        SuspendLayout();
        // 
        // List of Tracks
        // 
        lstClips.Location = new Point(12, 12);
        lstClips.Name = "lstClips";
        lstClips.Size = new Size(440, 184);
        lstClips.TabIndex = 1;
        // 
        // Add Track
        // 
        btnAdd.Enabled = false;
        btnAdd.Location = new Point(215, 255);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(145, 28);
        btnAdd.TabIndex = 3;
        btnAdd.Text = "Add Clip to Project";
        btnAdd.Click += btnAdd_Click;
        // 
        // 
        // 
        btnClose.DialogResult = DialogResult.Cancel;
        btnClose.Location = new Point(370, 255);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(85, 28);
        btnClose.TabIndex = 4;
        btnClose.Text = "Close";
        // 
        // Delete Tracks
        // 
        btnDelete.Enabled = false;
        btnDelete.Location = new Point(15, 255);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(90, 28);
        btnDelete.TabIndex = 5;
        btnDelete.Text = "Delete Clip";
        btnDelete.Click += btnDelete_Click;
        // 
        // lblStatus
        // 
        lblStatus.Location = new Point(15, 205);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(440, 40);
        lblStatus.TabIndex = 2;
        // 
        // FrmAudioLibrary
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnClose;
        ClientSize = new Size(470, 300);
        Controls.Add(lstClips);
        Controls.Add(lblStatus);
        Controls.Add(btnAdd);
        Controls.Add(btnClose);
        Controls.Add(btnDelete);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmAudioLibrary";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Audio Library";
        ResumeLayout(false);
    }
    private ListBox lstClips;
    private Button btnAdd;
    private Button btnClose;
    private Button btnDelete;
    private Label lblStatus;
}
