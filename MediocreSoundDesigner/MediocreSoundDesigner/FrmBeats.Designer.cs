namespace MediocreSoundDesigner;

partial class FrmBeats
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
        lblChoose = new Label();
        lstBeats = new ListBox();
        btnAdd = new Button();
        btnCancel = new Button();
        SuspendLayout();
        lblChoose.AutoSize = true;
        lblChoose.Location = new Point(15, 15);
        lblChoose.Text = "Select a beat to add:";
        lstBeats.Location = new Point(15, 40);
        lstBeats.Size = new Size(290, 100);
        lstBeats.Name = "lstBeats";
        btnAdd.Location = new Point(145, 160);
        btnAdd.Size = new Size(75, 28);
        btnAdd.Text = "Add Beat";
        btnAdd.Name = "btnAdd";
        btnAdd.Click += btnAdd_Click;
        btnCancel.Location = new Point(230, 160);
        btnCancel.Size = new Size(75, 28);
        btnCancel.Text = "Cancel";
        btnCancel.DialogResult = DialogResult.Cancel;
        AcceptButton = btnAdd;
        CancelButton = btnCancel;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(320, 205);
        Controls.AddRange(new Control[] { lblChoose, lstBeats, btnAdd, btnCancel });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Name = "FrmBeats";
        Text = "Beats";
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblChoose;
    private ListBox lstBeats;
    private Button btnAdd;
    private Button btnCancel;
}
