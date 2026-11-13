namespace MediocreSoundDesigner;
/// <summary>
/// 
/// </summary>
public partial class FrmEditSettings : FrmEditMaster
{
    /// <summary>
    /// 
    /// </summary>
    public FrmEditSettings()
    {
        InitializeComponent();
        btnRevertToDefault.Text = "Restore Defaults";
        DialogTheme.Build(this, "Appearance Settings", "Choose colors for the editor, tracks, and playback cursor.",
            new[] { ("Window background", (Control)btnColorWinBg), ("Window text", (Control)btnColorWinFg),
                ("Waveform", (Control)btnColorTrackSamples), ("Muted waveform", (Control)btnColorTrackSamplesDisabled),
                ("Selected track", (Control)btnColorTrackBgSelected), ("Unselected track", (Control)btnColorTrackBgUnselected),
                ("Playhead", (Control)btnPlayline) }, btnRevertToDefault, btnCancel, btnApply);
    }
    /// <inheritdoc/>
    protected override void Init()
    {
        base.Init();
        btnColorWinBg.BackColor = Settings.Color.WinBg;
        btnColorWinFg.BackColor = Settings.Color.WinFg;
        btnColorTrackSamples.BackColor = Settings.Color.TrackSamples_Enabled;
        btnColorTrackSamplesDisabled.BackColor = Settings.Color.TrackSamples_Disabled;
        btnColorTrackBgSelected.BackColor = Settings.Color.TrackBg_Selected;
        btnColorTrackBgUnselected.BackColor = Settings.Color.TrackBg_Unselected;
        btnPlayline.BackColor = Settings.Color.Playline;
    }
    /// <summary>
    /// 
    /// </summary>
    protected override void Apply()
    {
        Settings.Color.WinBg = btnColorWinBg.BackColor;
        Settings.Color.WinFg = btnColorWinFg.BackColor;
        Settings.Color.TrackSamples_Enabled = btnColorTrackSamples.BackColor;
        Settings.Color.TrackSamples_Disabled = btnColorTrackSamplesDisabled.BackColor;
        Settings.Color.TrackBg_Selected = btnColorTrackBgSelected.BackColor;
        Settings.Color.TrackBg_Unselected = btnColorTrackBgUnselected.BackColor;
        Settings.Color.Playline = btnPlayline.BackColor;
        Settings.SaveToFile();
    }

    private void UpdateColorFromUserSelection(object sender, EventArgs e)
    {
        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            (sender as Button).BackColor = colorDialog.Color;
        }
    }

    private void btnRevertToDefault_Click(object sender, EventArgs e)
    {
        Settings.RevertToDefault();
        Init();
    }

    private void label6_Click(object sender, EventArgs e)
    {

    }
}
