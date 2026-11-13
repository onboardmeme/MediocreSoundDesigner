using NAudio.Wave;

namespace MediocreSoundDesigner;

/// <summary>Displays the instruments planned for beat creation.</summary>
public partial class FrmInstruments : Form
{
    /// <summary>The list of instrument files to display.</summary>
    private readonly string[] instrumentFiles = {
        "acoustic-drum-kit-snare-01.mp3",
        "acoustic-drum-kit-snare-rimshot-01.mp3",
        "acoustic-drum-kit-snare-cross-stick-01.mp3",
        "acoustic-drum-kit-ride-01.mp3",
        "acoustic-drum-kit-ride-bell-01.mp3",
        "acoustic-drum-kit-pedal-hat-01.mp3"
    };
    /// <summary>The audio output for previewing the instrument.</summary>
    private WaveOut preview;

    /// <summary>The audio reader for previewing the instrument.</summary>
    private AudioFileReader previewReader;

    /// <summary>The instrument file chosen to add as a track.</summary>
    public string SelectedInstrumentPath { get; private set; }

    /// <summary>Creates the instrument list window.</summary>
    public FrmInstruments()
    {
        InitializeComponent();
        lstInstruments.SelectedIndex = 0;
        lstInstruments.SelectedIndexChanged += (_, _) => StopPreview();
        DialogTheme.Build(this, "Instruments", "Preview a sound, then add it to your audio tracks.",
            new[] { ("", (Control)lstInstruments) }, btnPreview, btnStop, btnClose, btnAdd);
    }
    /// <summary>
    /// Gets the full path of the selected instrument file.
    /// </summary>
    /// <returns></returns>
    private string GetSelectedPath()
    {
        if (lstInstruments.SelectedIndex < 0) return null;
        string path = Path.Combine(AppContext.BaseDirectory, "Beats", instrumentFiles[lstInstruments.SelectedIndex]);
        if (File.Exists(path)) return path;
        MessageBox.Show(this, "File not found", "Instruments");
        return null;
    }

    /// <summary>
    /// Previews the selected instrument file.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnPreview_Click(object sender, EventArgs e)
    {
        StopPreview();
        string path = GetSelectedPath();
        if (path == null) return;
        try
        {
            previewReader = new AudioFileReader(path);
            preview = new WaveOut();
            preview.Init(previewReader);
            preview.Play();
        }
        catch (Exception ex)
        {
            StopPreview();
            MessageBox.Show(this, $"Error: {ex.Message}", "Instruments", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Stops the preview of the instrument file.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnStop_Click(object sender, EventArgs e) => StopPreview();

    /// <summary>
    /// Adds instrument
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAdd_Click(object sender, EventArgs e)
    {
        string path = GetSelectedPath();
        if (path == null) return;
        StopPreview();
        SelectedInstrumentPath = path;
        DialogResult = DialogResult.OK;
        Close();
    }

    /// <summary>
    /// Stops the preview
    /// </summary>
    private void StopPreview()
    {
        preview?.Stop();
        preview?.Dispose();
        preview = null;
        previewReader?.Dispose();
        previewReader = null;
    }
}
