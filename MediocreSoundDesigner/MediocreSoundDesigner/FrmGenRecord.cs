using MediocreSoundDesigner.Properties;
using NAudio.Wave;

using System.ComponentModel;
namespace MediocreSoundDesigner;

/// <summary>
/// Records audio and places it into the track
/// </summary>
public partial class FrmGenRecord : FrmEditMaster {
  private WaveIn waveSource = null;
  private List<float> recordedSamples;
  private bool isRecording;


  /// <summary>
  /// Creates the recoding dialog box
  /// </summary>
  public FrmGenRecord() {
    InitializeComponent();
    DialogTheme.Build(this, "Record Audio", "Use the record control to start or stop microphone capture.",
        new[] { ("Record / Stop", (Control)btnRecord) }, btnCancel, btnApply);
    btnRecord.Paint += (_, e) =>
    {
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        var bounds = new Rectangle((btnRecord.ClientSize.Width - 18) / 2, (btnRecord.ClientSize.Height - 18) / 2, 18, 18);
        using var brush = new SolidBrush(Color.FromArgb(190, 40, 45));
        if (isRecording) e.Graphics.FillRectangle(brush, bounds);
        else e.Graphics.FillEllipse(brush, bounds);
    };
  }
    /// <summary>
    /// Initializes the computer recording software. Clears previous samples, sets recording to false, and sets up
    /// record button image.
    /// </summary>
    protected override void Init() {
    base.Init();
    recordedSamples = new();
    isRecording = false;
    btnApply.Enabled = false;
    UpdateRecordButton();
  }

    /// <inheritdoc/>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AppendToTrack { get; set; }
    /// <summary>
    /// Applies track changes
    /// </summary>
    protected override void Apply()
    {
        if (AppendToTrack)
        {
            float[] existing = track.CloneSamples();
            float[] combined = new float[existing.Length + recordedSamples.Count];
            existing.CopyTo(combined, 0);
            recordedSamples.CopyTo(combined, existing.Length);
            track.ReplaceSamples(combined);
        }
        else
        {
            track.ReplaceSamples(recordedSamples.ToArray());
        }
    }

    private void btnRecord_Click(object sender, EventArgs e) {
    if (isRecording) {
      waveSource?.StopRecording();
      isRecording = false;
      btnRecord.Enabled = false;
      UpdateRecordButton();
    }
    else {
      try {
        isRecording = true;
        btnApply.Enabled = false;
        UpdateRecordButton();

        waveSource = new();
        waveSource.WaveFormat = new WaveFormat(44100, 1);

        waveSource.DataAvailable += WaveSource_DataAvailable;
        waveSource.RecordingStopped += WaveSource_RecordingStopped;

        waveSource.StartRecording();
      }
      catch (Exception ex) {
        waveSource?.Dispose();
        waveSource = null;
        isRecording = false;
        btnRecord.Enabled = true;
        btnApply.Enabled = recordedSamples.Count > 0;
        UpdateRecordButton();
        MessageBox.Show($"Failed to start recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }

  private void UpdateRecordButton()
  {
    btnRecord.BackgroundImage = null;
    btnRecord.Text = "";
    btnRecord.AccessibleName = isRecording ? "Stop Recording" : "Start Recording";
    btnRecord.Invalidate();
  }

  private void WaveSource_DataAvailable(object sender, WaveInEventArgs e) {
    // 16-bit mono audio means 2 bytes per sample
    int sampleCount = e.BytesRecorded / 2;

    for (int i = 0; i < sampleCount; i++) {
      short sample = BitConverter.ToInt16(e.Buffer, i * 2);
      recordedSamples.Add(sample / (float)short.MaxValue);
    }
  }

  // Executed automatically after StopRecording() has completed
  private void WaveSource_RecordingStopped(object sender, StoppedEventArgs e) {
    isRecording = false;
    btnRecord.Enabled = true;
    btnApply.Enabled = recordedSamples.Count > 0 && e.Exception == null;
    UpdateRecordButton();
    // Safely dispose of resources in the correct order
    if (waveSource != null) {
      waveSource.Dispose();
      waveSource = null;
    }

    if (e.Exception != null)
        MessageBox.Show(this, e.Exception.Message, "Recording failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
  }
  /// <summary>
  /// Stops and releases the microphone if the dialog is closed while still recording,
  /// so the recording device isn't left running in the background.
  /// </summary>
  /// <param name="e">Details about the form closing event</param>
  protected override void OnFormClosing(FormClosingEventArgs e)
  {
    if (waveSource != null)
    {
        waveSource.DataAvailable -= WaveSource_DataAvailable;
        waveSource.RecordingStopped -= WaveSource_RecordingStopped;
        waveSource.StopRecording();
        waveSource.Dispose();
        waveSource = null;
        isRecording = false;
    }
    base.OnFormClosing(e);
  }
}
