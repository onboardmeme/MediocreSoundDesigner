namespace MediocreSoundDesigner;

/// <summary>Selects a premade beat file to add as a new track.</summary>
public partial class FrmBeats : Form
{
    private readonly string[] beatFiles = { "Top - PBC.wav", "beat2.wav", "beat3.wav", "beat4.wav" };

    /// <summary>The selected beat file after Add Beat is pressed.</summary>
    public string SelectedBeatPath { get; private set; }

    /// <summary>Creates the beat selection window.</summary>
    public FrmBeats()
    {
        InitializeComponent();
        lstBeats.Items.AddRange(new object[] { "Beat 1", "Beat 2", "Beat 3", "Beat 4" });
        lstBeats.SelectedIndex = 0;
        DialogTheme.Build(this, "Premade Beats", "Choose a beat to add as a new audio track.",
            new[] { ("", (Control)lstBeats) }, btnCancel, btnAdd);
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (lstBeats.SelectedIndex < 0) return;
        string path = Path.Combine(AppContext.BaseDirectory, "Beats", beatFiles[lstBeats.SelectedIndex]);
        
        SelectedBeatPath = path;
        DialogResult = DialogResult.OK;
        Close();
    }
}
