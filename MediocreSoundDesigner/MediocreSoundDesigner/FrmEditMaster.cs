namespace MediocreSoundDesigner;
/// <summary>
/// 
/// </summary>
public partial class FrmEditMaster : Form {
  // fields
  /// <summary>
  /// The track that is being edited
  /// </summary>
  protected Track track;

  // ctor
  /// <summary>
  /// Creates the editor dialog
  /// </summary>
  public FrmEditMaster() {
    InitializeComponent();
  }

  // sound
  /// <summary>
  /// Sets the track and calls Init to work on it.
  /// </summary>
  /// <param name="sound"></param>
  public virtual void SetSound(Track sound) {
    this.track = sound;
    Init();
  }

  // gui
  /// <summary>
  /// Applies the window background colors
  /// </summary>
  protected void ApplySettings() {
    BackColor = Settings.Color.WinBg;
    ForeColor = Settings.Color.WinFg;
  }
  /// <summary>
  /// Cancels and closes
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void btnCancel_Click(object sender, EventArgs e) {
    Cancel();
    DialogResult = DialogResult.Cancel;
    Close();
  }

  public void btnApply_Click(object sender, EventArgs e) {
    Apply();
    track?.PanDrawSamples?.Invalidate();
    DialogResult = DialogResult.OK;
    Close();
  }

  // override in subclass if needed
  /// <summary>
  /// Called when the dialog
  /// </summary>
  protected virtual void Init() { ApplySettings(); }
  /// <summary>
  /// Called when the user applies changes; subclasses override this to apply their edits to the track
  /// </summary>
  protected virtual void Apply() { }
  protected virtual void Cancel() { }
}
