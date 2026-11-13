namespace MediocreSoundDesigner;

/// <summary>A basic library for saving and reusing saved audio tracks.</summary>
public partial class FrmAudioLibrary : Form
{
    private readonly AudioLibrary library = new();

    /// <summary>The library clip chosen to add to the current project.</summary>
    public string SelectedClipPath { get; private set; }

    /// <summary>Creates the window to pick and reuse saved tracks.</summary>
    public FrmAudioLibrary()
    {
        InitializeComponent();
        lstClips.SelectedIndexChanged += (_, _) => UpdateButtons();
        Load += (_, _) => RefreshClips();
        DialogTheme.Build(this, "Audio Library", "Reuse saved clips or remove sounds you no longer need.",
            new[] { ("", (Control)lstClips), ("", (Control)lblStatus) }, btnDelete, btnClose, btnAdd);
    }

    /// <summary>
    /// Gets the number of saved tracks
    /// </summary>
    private void RefreshClips()
    {
        try
        {
            lstClips.DataSource = library.GetClips().Select(path => new ClipItem(path)).ToList();
            UpdateButtons();
            lblStatus.Text = lstClips.Items.Count == 0 ? "No saved clips yet." : $"{lstClips.Items.Count} saved clip(s).";
        }
        catch (Exception ex) { ShowError(ex); }
    }

    /// <summary>
    /// Updates the enabled state of the add and delete buttons based on the selected clip.
    /// </summary>
    private void UpdateButtons()
    {
        btnAdd.Enabled = lstClips.SelectedItem is ClipItem;
        btnDelete.Enabled = lstClips.SelectedItem is ClipItem;
    }

    /// <summary>
    /// Handles the click event for the delete button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (lstClips.SelectedItem is not ClipItem item) return;
        if (MessageBox.Show(this, "This action cannot be undone.\nTracks already added to your project will remain.",
            "Delete Saved Clip", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
        try
        {
            library.DeleteClip(item.Path);
            RefreshClips();
        }
        catch (Exception ex) { ShowError(ex); }
    }

    /// <summary>
    /// Handles the click event for the add button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (lstClips.SelectedItem is not ClipItem item) return;
        SelectedClipPath = item.Path;
        DialogResult = DialogResult.OK;
        Close();
    }

    /// <summary>
    /// Displays an error message in a MessageBox using the provided Exception's message.
    /// </summary>
    /// <param name="ex"><ex>Exception object containing the error message to display.</ex></param>
    private void ShowError(Exception ex) => MessageBox.Show(this, ex.Message, "Audio Library", MessageBoxButtons.OK, MessageBoxIcon.Error);

    /// <summary>
    /// Represents a saved audio clip in the library.
    /// </summary>
    /// <param name="Path"></param>
    private sealed record ClipItem(string Path)
    {
        public override string ToString() => System.IO.Path.GetFileNameWithoutExtension(Path);
    }
}
