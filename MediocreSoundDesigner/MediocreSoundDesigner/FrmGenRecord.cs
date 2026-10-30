using MediocreSoundDesigner.Properties;
using NAudio.Wave;

namespace MediocreSoundDesigner;

/// <summary>
/// Records audio and places it into the track
/// </summary>
public partial class FrmGenRecord : FrmEditMaster {
  private WaveIn waveSource = null;
  private List<float> recordedSamples;
  private bool isRecording;


  /// <summary>
  /// Creates the recoding dialouge box
  /// </summary>
  public FrmGenRecord() {
    InitializeComponent();
  }
    /// <summary>
    /// Initializes the computer recording software. Clears previous samples, sets recording to false, and sets up
    /// record button image.
    /// </summary>
    protected override void Init() {
    base.Init();
    recordedSamples = new();
    isRecording = false;
    btnRecord.BackgroundImage = Resources.record;
  }

  /// <inheritdoc/>
  protected override void Apply() {
    track.ReplaceSamples(recordedSamples.ToArray());
  }

  private void btnRecord_Click(object sender, EventArgs e) {
    if (isRecording) {
      waveSource?.StopRecording();
      isRecording = false;
      btnRecord.BackgroundImage = Resources.record;
    }
    else {
      try {
        isRecording = true;
        btnRecord.BackgroundImage = Resources.stop;

        waveSource = new();
        waveSource.WaveFormat = new WaveFormat(44100, 1);

        waveSource.DataAvailable += WaveSource_DataAvailable;
        waveSource.RecordingStopped += WaveSource_RecordingStopped;

        waveSource.StartRecording();
      }
      catch (Exception ex) {
        MessageBox.Show($"Failed to start recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
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
    // Safely dispose of resources in the correct order
    if (waveSource != null) {
      waveSource.Dispose();
      waveSource = null;
    }

    MessageBox.Show("Recording saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
  }
}
