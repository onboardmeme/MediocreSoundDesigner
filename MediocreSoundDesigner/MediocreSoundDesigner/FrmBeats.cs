namespace MediocreSoundDesigner;

/// <summary>Selects a premade beat file to add as a new track.</summary>
public partial class FrmBeats : Form
{
    private readonly string[] beatFiles = { "Top - PBC.wav", "Did it again.wav", "beat3.wav", "beat4.wav" };

    /// <summary>The selected beat file after Add Beat is pressed.</summary>
    public string SelectedBeatPath { get; private set; }

    /// <summary>Creates the beat selection window.</summary>
    public FrmBeats()
    {
        InitializeComponent();
        lstBeats.Items.AddRange(new object[] { "Beat 1 — Top", "Did it again", "Beat 3", "Beat 4" });
        for (int i = 0; i < beatFiles.Length; i++)
            if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "Beats", beatFiles[i])))
                lstBeats.Items[i] = $"{lstBeats.Items[i]} (audio file not installed)";
        lstBeats.SelectedIndex = 0;
        DialogTheme.Build(this, "Premade Beats", "Choose a beat to add as a new audio track.",
            new[] { ("", (Control)lstBeats) }, btnCancel, btnAdd);
        void UpdateAvailableBeat()
        {
            btnAdd.Enabled = lstBeats.SelectedIndex >= 0 && File.Exists(Path.Combine(AppContext.BaseDirectory, "Beats", beatFiles[lstBeats.SelectedIndex]));
        }
        lstBeats.SelectedIndexChanged += (_, _) => UpdateAvailableBeat();
        UpdateAvailableBeat();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (lstBeats.SelectedIndex < 0) return;
        string path = Path.Combine(AppContext.BaseDirectory, "Beats", beatFiles[lstBeats.SelectedIndex]);
        if (!File.Exists(path))
        {
            MessageBox.Show(this, "This beat has not been added yet.", "Premade Beats");
            return;
        }
        
        SelectedBeatPath = path;
        DialogResult = DialogResult.OK;
        Close();
    }
}
