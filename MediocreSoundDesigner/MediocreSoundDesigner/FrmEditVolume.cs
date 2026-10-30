namespace MediocreSoundDesigner;
/// <summary>
/// Dialouge box that changes the volume
/// </summary>
public partial class FrmEditVolume : FrmEditMaster {
    /// <summary>
    /// Initializes the volume component
    /// </summary>
  public FrmEditVolume() {
    InitializeComponent();
  }
    /// <summary>
    /// Opens the dialouge box that changes the volume as a percentage
    /// </summary>
  protected override void Init() {
    base.Init();
    nudVolume.Value = (decimal)(track.Volume * 100.0f);
  }

  /// <inheritdoc/>
  protected override void Apply() {
    track.SetVolume((float)nudVolume.Value / 100.0f);
  }
}
