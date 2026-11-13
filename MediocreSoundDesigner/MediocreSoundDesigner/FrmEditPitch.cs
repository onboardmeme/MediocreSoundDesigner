namespace MediocreSoundDesigner;
/// <summary>
/// Dialog box that changes the pitch in semitones
/// </summary>
public partial class FrmEditPitch : FrmEditMaster
{
    /// <summary>
    /// Initializes the pitch dialog
    /// </summary>
    public FrmEditPitch()
    {
        InitializeComponent();
        DialogTheme.Build(this, "Track Pitch", "Raise or lower pitch in semitones. This also changes the track's length.",
            new[] { ("Semitones", (Control)nudSemitones) }, btnCancel, btnApply);
    }

    /// <inheritdoc/>
    protected override void Init()
    {
        base.Init();
        nudSemitones.Value = 0;   // the edit is destructive, so always start neutral
    }

    /// <inheritdoc/>
    protected override void Apply()
    {
        float factor = MathF.Pow(2.0f, (float)nudSemitones.Value / 12.0f);
        track.AlterPitch(factor);
    }
}
