namespace MediocreSoundDesigner;
/// <summary>
/// Base class for edit dialogs that holds tracks and button options.
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
    foreach (Control control in Controls) control.BackColor = BackColor;
  }
  /// <summary>
  /// Cancels and closes the dialog box
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void btnCancel_Click(object sender, EventArgs e) {
    Cancel();
    DialogResult = DialogResult.Cancel;
    Close();
  }
  /// <summary>
  /// Applies the setting and closes the dialog box
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void btnApply_Click(object sender, EventArgs e) {
    if (sender is Button button && !button.Enabled) return;
    try {
    Apply();
    track?.PanDrawSamples?.Invalidate();
    DialogResult = DialogResult.OK;
    Close();
    }
    catch (Exception ex) {
      MessageBox.Show(this, ex.Message, "Could not apply changes", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }

  // override in subclass if needed
  /// <summary>
  /// Called when the dialog opens.
  /// </summary>
  protected virtual void Init() { ApplySettings(); }
  /// <summary>
  /// Called when the user applies changes
  /// </summary>
  protected virtual void Apply() { }
  /// <summary>
  /// Called when the user cancels changes
  /// </summary>
  protected virtual void Cancel() { }
}
