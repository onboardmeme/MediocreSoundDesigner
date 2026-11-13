using MediocreSoundDesigner.Properties;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MediocreSoundDesigner;
/// <summary>
/// The main window of the Sound Designer
/// </summary>
public partial class FrmMain : Form
{
    // fields
    private TrackManager trackManager;
    private Panel trackRows;
    private readonly List<Panel> trackHeaders = new();
    private int trackRowHeight;
    private int trackRowGap;
    private int waveformLeft;
    private bool updatingTrackLayout;
    private Track selectionTrack;
    private int selectionAnchor;
    private int selectionStart;
    private int selectionEnd;
    private bool selectingAudio;
    private bool draggingPlayhead;


    private void ClearAudioSelection()
    {
        var oldTrack = selectionTrack;
        selectionTrack = null;
        selectingAudio = false;
        selectionStart = selectionEnd = 0;
        menuDeleteSelectedAudio.Enabled = false;
        lblDebug.Text = "";
        if (oldTrack?.PanDrawSamples is { IsDisposed: false } panel) panel.Invalidate();
    }

    private int SampleAt(Track track, int x) => (int)Math.Clamp(
        ((long)x - track.DrawOffset) * Math.Max(1, trackManager.DrawTimescale), 0L, track.SampleCount);

    private void UpdateAudioSelection(Track track, int x)
    {
        int position = SampleAt(track, x);
        selectionStart = Math.Min(selectionAnchor, position);
        selectionEnd = Math.Max(selectionAnchor, position);
        menuDeleteSelectedAudio.Enabled = selectionEnd > selectionStart;
        lblDebug.Text = selectionEnd > selectionStart
            ? $"Selected: {selectionStart / 44100.0:F2}s – {selectionEnd / 44100.0:F2}s | Edit > Delete Selected Audio"
            : "Drag across a waveform to select audio.";
        track.PanDrawSamples.Invalidate();
    }

    private void menuDeleteSelectedAudio_Click(object sender, EventArgs e)
    {
        if (selectionTrack == null || !trackManager.Tracks.Contains(selectionTrack) || selectionEnd <= selectionStart) return;
        if (MessageBox.Show(this, "Delete the highlighted audio and join the remaining sections? This cannot be undone.",
            "Delete Selected Audio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
        selectionTrack.DeleteRange(selectionStart, selectionEnd);
        ClearAudioSelection();
        UpdateSizes();
    }

    // ctor
    /// <summary>
    /// Initializes all of the components and setup.
    /// </summary>
    public FrmMain()
    {
        InitializeComponent();
        Setup();
    }

    // setup
    private void Setup()
    {
        DoubleBuffered = true;
        SetupTrackRows();
        trackManager = new(panTracks);
        btnPlay.BackgroundImage = Resources.play;
        trackManager.DrawTimescale = trkDrawTimescale.Value;
        UpdateSizes();
        ApplySettings();
        lblPlayhead.Cursor = Cursors.SizeWE;
        lblPlayhead.MouseDown += (s, e) => draggingPlayhead = true;
        lblPlayhead.MouseMove += (s, e) =>
        {
            if (!draggingPlayhead) return;
            int x = panTracks.PointToClient(Cursor.Position).X;
            lblPlayhead.Left = Math.Clamp(x, 0, panTracks.Width - lblPlayhead.Width);
        };
        lblPlayhead.MouseUp += (s, e) =>
        {
            if (!draggingPlayhead) return;
            draggingPlayhead = false;
            trackManager.SeekTo(lblPlayhead.Left);
        };
        Click += (s, e) => ActiveControl = null;
        panTracks.Click += (s, e) => ActiveControl = null;
        trackRows.Click += (s, e) => ActiveControl = null;
    }
    private void ApplySettings()
    {
        BackColor = Settings.Color.WinBg;
        panTracks.BackColor = Settings.Color.TrackBg_Unselected;
        lblPlayhead.BackColor = Settings.Color.Playline;
        trackManager.ApplySettings();
    }
    private void RenumberHeaders()
    {
        for (int i = 0; i < trackHeaders.Count; i++)
            foreach (var tb in trackHeaders[i].Controls.OfType<TextBox>())
                if (Regex.IsMatch(tb.Text, @"^Track \d+$"))
                    tb.Text = $"Track {i + 1}";
    }

    // GUI controls
    /// <summary>
    /// Processes a command key
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="keyData"></param>
    /// <returns>True if the keystroke was handled, otherwise returns false</returns>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (ActiveControl is TextBoxBase && (keyData == Keys.Enter || keyData == Keys.Escape))
        {
            ActiveControl = null;   // drops focus from the text box
            return true;
        }
        if (ActiveControl is TextBoxBase) return base.ProcessCmdKey(ref msg, keyData);
        if (keyData == Keys.Delete && selectionTrack != null && selectionEnd > selectionStart)
        {
            menuDeleteSelectedAudio_Click(this, EventArgs.Empty);
            return true;
        }
        if (trackManager.SelectedTrack is not null)
        {
            if (keyData == Keys.Left)
            {
                trackManager.SelectedTrack.ShiftSampleOver(-5);
                trackManager.SelectedTrack.PanDrawSamples.Invalidate();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                trackManager.SelectedTrack.ShiftSampleOver(+5);
                trackManager.SelectedTrack.PanDrawSamples.Invalidate();
                return true;
            }
            else if (keyData == Keys.Delete)
            {
                if (trackManager.SelectedTrack is not null)
                {
                    if (MessageBox.Show("This will delete the selected track. This is NOT undoable!", "Are You Sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        int idx = trackManager.Tracks.IndexOf(trackManager.SelectedTrack);   

                        ClearAudioSelection();
                        panTracks.Controls.Remove(trackManager.SelectedTrack.PanDrawSamples);
                        trackManager.SelectedTrack.PanDrawSamples.Dispose();
                        trackManager.SelectedTrack.Dispose();
                        trackManager.RemTrack(trackManager.SelectedTrack);

                        if (idx >= 0 && idx < trackHeaders.Count)
                        {
                            var h = trackHeaders[idx];
                            trackHeaders.RemoveAt(idx);
                            trackHeaders.Add(h);
                            foreach (var tb in h.Controls.OfType<TextBox>())
                                tb.Text = "Track 0";    
                            RenumberHeaders();
                        }
                        UpdateSizes();
                        return true;
                    }
                }
            }
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void FrmMain_SizeChanged(object sender, EventArgs e)
    {
        if (trackManager is null) return;   // form is still being constructed
        UpdateSizes();
    }

    private void btnPlay_Click(object sender, EventArgs e)
    {
        if (trackManager.State is NAudio.Wave.PlaybackState.Stopped or NAudio.Wave.PlaybackState.Paused)
        {
            trackManager.PlayAll();
            btnPlay.BackgroundImage = Resources.pause;
        }
        else
        {
            trackManager.PauseAll();
            btnPlay.BackgroundImage = Resources.play;
        }
    }

    private void menuFileOpen_Click(object sender, EventArgs e)
    {
        ofdLoadFile.DefaultExt = "wav";
        try
        {
            if (ofdLoadFile.ShowDialog() == DialogResult.OK)
            {
                Track newTrack = new(ofdLoadFile.FileName);
                CreateTrackPanel(newTrack);
                trackManager.AddTrack(newTrack);
                UpdateSizes();
                newTrack.PanDrawSamples.Invalidate();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Could not open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void menuAudioLibrary_Click(object sender, EventArgs e)
    {
        using var dialog = new FrmAudioLibrary();
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        Track clip;
        try { clip = new Track(dialog.SelectedClipPath); }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Could not load the clip: {ex.Message}", "Audio Library", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        CreateTrackPanel(clip);
        trackManager.AddTrack(clip);
        UpdateSizes();
        clip.PanDrawSamples.Invalidate();
    }

    private void btnSaveCurrentTrack_Click(object sender, EventArgs e)
    {
        var selected = trackManager.SelectedTrack;
        if (selected == null || !trackManager.Tracks.Contains(selected))
        {
            MessageBox.Show(this, "Select a track to save first.", "Save Current Track");
            return;
        }
        string name = Microsoft.VisualBasic.Interaction.InputBox(
            "Name this clip to save it in your audio library:", "Save Current Track", "");
        if (string.IsNullOrWhiteSpace(name)) return;
        try
        {
            new AudioLibrary().SaveClip(name, selected);
            MessageBox.Show(this, "Track saved. Open File > Audio Library to reuse it.", "Save Current Track");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Could not save track", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void menuFileSave_Click(object sender, EventArgs e)
    {
        sfdSaveFile.DefaultExt = "wav";
        if (sfdSaveFile.ShowDialog() == DialogResult.OK)
        {
            trackManager.SaveToFile(sfdSaveFile.FileName);

            // tag the file we just wrote
            new WavMetadata
            {
                Title = Path.GetFileNameWithoutExtension(sfdSaveFile.FileName),
                Artist = Microsoft.VisualBasic.Interaction.InputBox("Artist name:", "Export", "Your Name Here"),
                Year = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss")
            }.AppendTo(sfdSaveFile.FileName);
        }
    }
    private void menuFileSettings_Click(object sender, EventArgs e)
    {
        FrmEditSettings frm = new();
        frm.SetSound(trackManager.SelectedTrack);
        if (frm.ShowDialog() == DialogResult.OK)
        {
            ApplySettings();
        }
    }

    private void menuEditFlip_Click(object sender, EventArgs e)
    {
        ClearAudioSelection();
        trackManager.SelectedTrack?.Flip();
    }
    private void menuEditReverse_Click(object sender, EventArgs e)
    {
        ClearAudioSelection();
        trackManager.SelectedTrack?.Reverse();
    }
    private void menuEditVolume_Click(object sender, EventArgs e)
    {
        if (trackManager.SelectedTrack is not null)
        {
            FrmEditVolume frm = new();
            frm.SetSound(trackManager.SelectedTrack);
            frm.ShowDialog();
        }
    }
    private void menuEditPitch_Click(object sender, EventArgs e)
    {
        ClearAudioSelection();
        if (trackManager.SelectedTrack is not null)
        {
            FrmEditPitch frm = new();
            frm.SetSound(trackManager.SelectedTrack);
            frm.ShowDialog();
            UpdateSizes();
            trackManager.RedrawAll();
        }
    }
    private void menuEditEliminateSilence_Click(object sender, EventArgs e)
    {
        ClearAudioSelection();
        if (trackManager.SelectedTrack is not null)
        {
            FrmEditEliminateSilence frm = new();
            frm.SetSound(trackManager.SelectedTrack);
            frm.ShowDialog();
        }
    }

    private void menuGenInstruments_Click(object sender, EventArgs e)
    {
        using var dialog = new FrmInstruments();
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        Track instrument;
        try { instrument = new Track(dialog.SelectedInstrumentPath); }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Could not add the instrument: {ex.Message}", "Instruments", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        CreateTrackPanel(instrument);
        trackManager.AddTrack(instrument);
        UpdateSizes();
        instrument.PanDrawSamples.Invalidate();
    }

    private void menuGenBeats_Click(object sender, EventArgs e)
    {
        using var dialog = new FrmBeats();
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        Track beat;
        try
        {
            beat = new Track(dialog.SelectedBeatPath);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Could not open the beat: {ex.Message}", "Beat could not be added",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        CreateTrackPanel(beat);
        trackManager.AddTrack(beat);
        UpdateSizes();
        beat.PanDrawSamples.Invalidate();
    }

    private void menuGenRecord_Click(object sender, EventArgs e)
    {
        Track newTrack = new(new float[0]);
        CreateTrackPanel(newTrack);
        trackManager.AddTrack(newTrack);
        UpdateSizes();
        newTrack.PanDrawSamples.Invalidate();

        FrmGenRecord frm = new();
        frm.SetSound(trackManager.SelectedTrack);
        if (frm.ShowDialog() == DialogResult.Cancel)
        {
            ClearAudioSelection();
            trackManager.RemTrack(newTrack);
            panTracks.Controls.Remove(newTrack.PanDrawSamples);
            newTrack.PanDrawSamples.Dispose();
            UpdateSizes();
        }
    }
    private void menuGenNoise_Click(object sender, EventArgs e)
    {
        const int NUM_SAMPLES = 100_000;
        float[] samples = new float[NUM_SAMPLES];
        Random rand = new();
        for (int i = 0; i < NUM_SAMPLES; i++)
        {
            samples[i] = rand.NextSingle() * 2.0f - 1.0f;
        }
        Track newTrack = new(samples);
        CreateTrackPanel(newTrack);
        trackManager.AddTrack(newTrack);
        UpdateSizes();
        newTrack.PanDrawSamples.Invalidate();
    }

    private void menuGenRecordAppend_Click(object sender, EventArgs e)
    {
        if (trackManager.SelectedTrack is not Track target)
        {
            MessageBox.Show(this, "Select a track first.", "Record onto Track", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        ClearAudioSelection();
        FrmGenRecord frm = new() { AppendToTrack = true };
        frm.SetSound(target);
        if (frm.ShowDialog(this) == DialogResult.OK)
        {
            UpdateSizes();
            trackManager.RedrawAll();
        }
    }

    private void menuGenGradientNoise_Click(object sender, EventArgs e)
    {
        const int NUM_SAMPLES = 250_000;
        float[] samples = new float[NUM_SAMPLES];
        Random rand = new();
        float cur = 0;
        float inc = 0.005f;
        int dir = +1;
        for (int i = 0; i < NUM_SAMPLES; i++)
        {
            cur += (inc * dir);
            if (rand.NextSingle() < 0.5f)
            {
                dir *= -1;
            }
            if (cur > 0.9f)
            {
                dir = -1;
            }
            if (cur < -0.9f)
            {
                dir = +1;
            }
            samples[i] = cur;
        }
        Track newTrack = new(samples);
        CreateTrackPanel(newTrack);
        trackManager.AddTrack(newTrack);
        UpdateSizes();
        newTrack.PanDrawSamples.Invalidate();
    }

    private void tmrUpdatePlaybackState_Tick(object sender, EventArgs e)
    {
        if (trackManager.Tracks.Count > 0)
        {
            btnPlay.BackgroundImage = trackManager.State == NAudio.Wave.PlaybackState.Playing ? Resources.pause : Resources.play;
            btnPlay.Visible = true;
            if (!draggingPlayhead) lblPlayhead.Left = trackManager.GetPlayheadPos();
            lblPlayhead.Visible = true;
        }
        else
        {
            btnPlay.BackgroundImage = null;
            btnPlay.Visible = false;
            lblPlayhead.Visible = false;
        }
    }

    private void trkDrawTimescale_Scroll(object sender, EventArgs e)
    {
        trackManager.DrawTimescale = Math.Max(1, trkDrawTimescale.Maximum - trkDrawTimescale.Value);
        trackManager.RedrawAll();
    }

    // Helper methods
    private Panel CreateTrackPanel(Track track)
    {
        Panel pan = new()
        {
            BackColor = Settings.Color.TrackBg_Unselected,
            Width = panTracks.Width,
            Left = 0,
            Top = 0,
        };
        pan.Click += (sender, e) =>
        {
            if (selectionTrack != track) ClearAudioSelection();
            trackManager.SelectTrack(track);
        };
        pan.MouseDown += (sender, e) =>
        {
            ActiveControl = null;
            if (e.Button != MouseButtons.Left) return;
            ClearAudioSelection();
            trackManager.SelectTrack(track);
            selectionTrack = track;
            selectionAnchor = SampleAt(track, e.X);
            selectingAudio = true;
            pan.Capture = true;
            UpdateAudioSelection(track, e.X);
        };
        pan.MouseMove += (sender, e) =>
        {
            if (selectingAudio && selectionTrack == track) UpdateAudioSelection(track, e.X);
        };
        pan.MouseUp += (sender, e) =>
        {
            if (e.Button != MouseButtons.Left || !selectingAudio || selectionTrack != track) return;
            UpdateAudioSelection(track, e.X);
            selectingAudio = false;
            pan.Capture = false;
        };
        pan.MouseCaptureChanged += (sender, e) =>
        {
            if (!pan.Capture) selectingAudio = false;
        };
        pan.DoubleClick += (sender, e) =>
        {
            track.ToggleDisabled();
            track.PanDrawSamples.Invalidate();
        };
        pan.Paint += (sender, e) =>
        {
            track.DrawSamples(e.Graphics, trackManager.DrawTimescale);
            if (selectionTrack == track && selectionEnd > selectionStart)
            {
                float scale = Math.Max(1, trackManager.DrawTimescale);
                float left = track.DrawOffset + Math.Min(selectionStart, track.SampleCount) / scale;
                float right = track.DrawOffset + Math.Min(selectionEnd, track.SampleCount) / scale;
                using var highlight = new SolidBrush(Color.FromArgb(90, Color.DodgerBlue));
                e.Graphics.FillRectangle(highlight, left, 0, right - left, pan.Height);
            }
        };
        panTracks.Controls.Add(pan);
        track.LinkPanels(pan, panTracks);
        trackManager.SelectTrack(track);

        return pan;
    }

    /// <summary>
    /// Sets up the track rows panel and adds the track headers and waveform panel to it.
    /// </summary>
    private void SetupTrackRows()
    {
        Panel[] headers = { panel1, panel2, panel3, panel4, panel5, panel6, panel7, panel8, panel9 };
        trackRowHeight = panel1.Height;
        trackRowGap = Math.Max(0, panel2.Top - panel1.Bottom);
        waveformLeft = panTracks.Left - panel1.Left;
        trackRows = new Panel
        {
            Name = "panTrackRows",
            Location = panel1.Location,
            AutoScroll = true,
            BackColor = BackColor
        };
        Controls.Add(trackRows);
        foreach (Panel header in headers)
        {
            trackHeaders.Add(header);
            trackRows.Controls.Add(header);
        }
        trackRows.Controls.Add(panTracks);
        // Both columns scroll together; each waveform gets one fixed-height row.
        panTracks.AutoScroll = false;
        panTracks.BorderStyle = BorderStyle.None;
    }

    private void UpdateSizes()
    {
        if (trackRows == null || trackManager == null || updatingTrackLayout) return;
        updatingTrackLayout = true;
        try
        {
        const int PADDING = 15;
        Point scroll = trackRows.AutoScrollPosition;
        trackRows.SuspendLayout();
        trackRows.AutoScrollPosition = Point.Empty;
        trackRows.Width = Math.Max(waveformLeft + 100, ClientSize.Width - trackRows.Left - PADDING);
        trackRows.Height = Math.Max(50, ClientSize.Height - trackRows.Top - lblDebug.Height);
        int stride = trackRowHeight + trackRowGap;
        while (trackHeaders.Count < trackManager.Tracks.Count)
        {
            var header = new Panel { BackColor = panel1.BackColor, Size = panel1.Size };
            header.Controls.Add(new TextBox
            {
                Text = $"Track {trackHeaders.Count + 1}",
                Location = textBox1.Location,
                Size = textBox1.Size,
                BackColor = textBox1.BackColor,
                ForeColor = textBox1.ForeColor,
                BorderStyle = textBox1.BorderStyle
            });
            trackHeaders.Add(header);
            trackRows.Controls.Add(header);
        }
        int rowCount = Math.Max(9, trackManager.Tracks.Count);
        int contentHeight = rowCount * stride - trackRowGap;
        int scrollbarWidth = contentHeight > trackRows.ClientSize.Height ? SystemInformation.VerticalScrollBarWidth : 0;
        panTracks.Location = new Point(waveformLeft, 0);
        panTracks.Width = Math.Max(1, trackRows.Width - waveformLeft - scrollbarWidth);
        panTracks.Height = contentHeight;
        for (int i = 0; i < trackHeaders.Count; i++)
        {
            trackHeaders[i].Location = new Point(0, i * stride);
            trackHeaders[i].Visible = i < rowCount;
        }
        for (int i = 0; i < trackManager.Tracks.Count; i++)
        {
            Track track = trackManager.Tracks[i];
            track.PanDrawSamples.Left = 0;
            track.PanDrawSamples.Width = panTracks.ClientSize.Width;
            track.PanDrawSamples.Height = trackRowHeight;
            track.PanDrawSamples.Top = i * stride;
            track.PanDrawSamples.Invalidate();
        }
        trackRows.AutoScrollMinSize = new Size(0, contentHeight);
        trackRows.ResumeLayout();
        trackRows.AutoScrollPosition = new Point(-scroll.X, -scroll.Y);
        trkDrawTimescale.Width = panTracks.Width;
        trkDrawTimescale.Left = trackRows.Left + waveformLeft;
        lblPlayhead.Left = 0;
        lblPlayhead.Height = panTracks.Height;
        lblPlayhead.BringToFront();
        }
        finally { updatingTrackLayout = false; }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }
}
