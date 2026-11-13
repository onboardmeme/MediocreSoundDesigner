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
    private Track trackMarkedForDeletion;
    private int selectionAnchor;
    private int selectionStart;
    private int selectionEnd;
    private bool selectingAudio;
    private Track movingClip;
    private int clipDragX;
    private int clipOriginalOffset;
    private int clipDragScale;
    private const int ClipHandleHeight = 18;
    private bool draggingPlayhead;
    private long viewportStartSample;

    private void FollowPlayback()
    {
        if (trackManager == null || draggingPlayhead || selectingAudio || movingClip != null) return;
        int scale = Math.Max(1, trackManager.DrawTimescale);
        int rightEdge = Math.Max(0, panTracks.ClientSize.Width - lblPlayhead.Width);
        long sample = trackManager.PlaybackSamplePosition;
        long start = Math.Max(0, viewportStartSample);
        if (sample < start) start = sample;
        else if (sample > start + (long)rightEdge * scale) start = Math.Max(0, sample - (long)rightEdge * scale);
        if (viewportStartSample != start)
        {
            viewportStartSample = start;
            trackManager.RedrawAll();
        }
        lblPlayhead.Left = (int)Math.Clamp((sample - start) / scale, 0, rightEdge);
        lblPlayhead.BringToFront();
    }


    private void ClearAudioSelection()
    {
        var oldTrack = selectionTrack;
        selectionTrack = null;
        trackMarkedForDeletion = null;
        selectingAudio = false;
        selectionStart = selectionEnd = 0;
        UpdateDeleteAudioButton(false);
        lblDebug.Text = "";
        if (oldTrack?.PanDrawSamples is { IsDisposed: false } panel) panel.Invalidate();
    }

    private int SampleAt(Track track, int x) => (int)Math.Clamp(
        viewportStartSample + (long)x * Math.Max(1, trackManager.DrawTimescale) - track.OffsetSamples, 0L, track.SampleCount);

    private void UpdateAudioSelection(Track track, int x)
    {
        int position = SampleAt(track, x);
        selectionStart = Math.Min(selectionAnchor, position);
        selectionEnd = Math.Max(selectionAnchor, position);
        UpdateDeleteAudioButton(selectionEnd > selectionStart);
        lblDebug.Text = selectionEnd > selectionStart
            ? $"Selected: {selectionStart / 44100.0:F2}s – {selectionEnd / 44100.0:F2}s | Click Delete Selected Audio or press Delete"
            : "Drag across a waveform to select audio.";
        track.PanDrawSamples.Invalidate();
    }

    private void UpdateDeleteAudioButton(bool hasSelection)
    {
        string action = trackMarkedForDeletion != null ? "Delete Track" : "Delete Selected Audio";
        btnDeleteAudio.AccessibleName = action;
        toolbarToolTip.SetToolTip(btnDeleteAudio, action);
        menuDeleteSelectedAudio.Enabled = hasSelection;
        btnDeleteAudio.Enabled = hasSelection;
        btnDeleteAudio.BackColor = hasSelection ? SystemColors.Control : Color.FromArgb(80, 80, 80);
        btnDeleteAudio.ForeColor = hasSelection ? SystemColors.ControlText : Color.Gray;
    }

    private void menuDeleteSelectedAudio_Click(object sender, EventArgs e)
    {
        if (trackMarkedForDeletion != null)
        {
            DeleteTrack(trackMarkedForDeletion);
            return;
        }
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
        SetupToolbarIcons();
        Setup();
    }

    private void SetupToolbarIcons()
    {
        ToolbarIcons.Apply(btnRestart, ToolbarIcons.Action.Restart);
        ToolbarIcons.Apply(btnOpenAudio, ToolbarIcons.Action.Open);
        ToolbarIcons.Apply(btnSaveAudio, ToolbarIcons.Action.Save);
        ToolbarIcons.Apply(btnAudioLibrary, ToolbarIcons.Action.Library);
        ToolbarIcons.Apply(btnSaveCurrentTrack, ToolbarIcons.Action.SaveTrack);
        ToolbarIcons.Apply(btnSettings, ToolbarIcons.Action.Settings);
        ToolbarIcons.Apply(btnRecordNew, ToolbarIcons.Action.Record);
        ToolbarIcons.Apply(btnRecordAppend, ToolbarIcons.Action.Append);
        ToolbarIcons.Apply(btnNoise, ToolbarIcons.Action.Noise);
        ToolbarIcons.Apply(btnBeats, ToolbarIcons.Action.Beats);
        ToolbarIcons.Apply(btnInstruments, ToolbarIcons.Action.Instruments);
        ToolbarIcons.Apply(btnFlip, ToolbarIcons.Action.Flip);
        ToolbarIcons.Apply(btnReverse, ToolbarIcons.Action.Reverse);
        ToolbarIcons.Apply(btnVolume, ToolbarIcons.Action.Volume);
        ToolbarIcons.Apply(btnPitch, ToolbarIcons.Action.Pitch);
        ToolbarIcons.Apply(btnEliminateSilence, ToolbarIcons.Action.Silence);
        ToolbarIcons.Apply(btnDeleteAudio, ToolbarIcons.Action.Delete);
    }

    // setup
    private void Setup()
    {
        DoubleBuffered = true;
        SetupTrackRows();
        trackManager = new(panTracks);
        btnPlay.Enabled = false;
        btnPlay.BackgroundImage = null;
        btnPlay.Paint += (_, e) =>
        {
            if (btnPlay.Enabled) return;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var gray = new SolidBrush(Color.Gray);
            float cx = btnPlay.ClientSize.Width / 2f;
            float cy = btnPlay.ClientSize.Height / 2f;
            e.Graphics.FillPolygon(gray, new PointF[] {
                new(cx - 7, cy - 10), new(cx + 9, cy), new(cx - 7, cy + 10) });
        };
        trackManager.DrawTimescale = trkDrawTimescale.Value;
        UpdateSizes();
        ApplySettings();
        lblPlayhead.Cursor = Cursors.SizeWE;
        lblPlayhead.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { draggingPlayhead = true; lblPlayhead.Capture = true; } };
        lblPlayhead.MouseMove += (s, e) =>
        {
            if (!draggingPlayhead) return;
            int x = panTracks.PointToClient(Cursor.Position).X;
            lblPlayhead.Left = Math.Clamp(x, 0, Math.Max(0, panTracks.ClientSize.Width - lblPlayhead.Width));
        };
        lblPlayhead.MouseUp += (s, e) =>
        {
            if (!draggingPlayhead) return;
            draggingPlayhead = false;
            lblPlayhead.Capture = false;
            trackManager.SeekToSample((int)Math.Clamp(viewportStartSample + (long)lblPlayhead.Left * Math.Max(1, trackManager.DrawTimescale), 0, int.MaxValue));
            FollowPlayback();
        };
        lblPlayhead.MouseCaptureChanged += (_, _) =>
        {
            if (!lblPlayhead.Capture) draggingPlayhead = false;
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
        if (keyData == Keys.F1)
        {
            btnHelp_Click(this, EventArgs.Empty);
            return true;
        }
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
                trackManager.SelectedTrack.ShiftSampleOver(-5, trackManager.DrawTimescale);
                trackManager.SelectedTrack.PanDrawSamples.Invalidate();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                trackManager.SelectedTrack.ShiftSampleOver(+5, trackManager.DrawTimescale);
                trackManager.SelectedTrack.PanDrawSamples.Invalidate();
                return true;
            }
            else if (keyData == Keys.Delete)
            {
                DeleteTrack(trackManager.SelectedTrack);
                return true;
            }
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void DeleteTrack(Track track)
    {
        int idx = trackManager.Tracks.IndexOf(track);
        if (idx < 0) return;
        if (MessageBox.Show(this, "Delete this entire track? This cannot be undone.", "Delete Track",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
        trackManager.StopForTimelineEdit();
        ClearAudioSelection();
        panTracks.Controls.Remove(track.PanDrawSamples);
        track.PanDrawSamples.Dispose();
        track.Dispose();
        trackManager.RemTrack(track);
        if (idx < trackHeaders.Count)
        {
            var header = trackHeaders[idx];
            trackHeaders.RemoveAt(idx);
            trackHeaders.Add(header);
            foreach (var name in header.Controls.OfType<TextBox>()) name.Text = "Track 0";
            RenumberHeaders();
        }
        UpdateSizes();
    }

    private void FrmMain_SizeChanged(object sender, EventArgs e)
    {
        if (trackManager is null) return;   // form is still being constructed
        UpdateSizes();
    }

    private void btnHelp_Click(object sender, EventArgs e)
    {
        using var guide = new FrmHelp();
        guide.ShowDialog(this);
    }

    private void btnRestart_Click(object sender, EventArgs e)
    {
        if (trackManager.Tracks.Count == 0) return;
        try
        {
            trackManager.RestartAll();
            viewportStartSample = 0;
            FollowPlayback();
            btnPlay.BackgroundImage = Resources.pause;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Could not restart playback: {ex.Message}", "Playback", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnPlay_Click(object sender, EventArgs e)
    {
        if (trackManager.Tracks.Count == 0) return;
        try
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
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Could not play audio", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        using var prompt = new FrmTextPrompt("Save Current Track", "Give this clip a name so you can reuse it from your audio library.");
        if (prompt.ShowDialog(this) != DialogResult.OK) return;
        string name = prompt.Value;
        if (string.IsNullOrWhiteSpace(name)) return;
        try
        {
            new AudioLibrary().SaveClip(name, selected);
            MessageBox.Show(this, "Track saved. Click Audio Library to reuse it.", "Save Current Track");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Could not save track", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void menuFileSave_Click(object sender, EventArgs e)
    {
        try
        {
        sfdSaveFile.DefaultExt = "wav";
        if (sfdSaveFile.ShowDialog() == DialogResult.OK)
        {
            trackManager.SaveToFile(sfdSaveFile.FileName);

            using var artistPrompt = new FrmTextPrompt("Export Details", "Enter an artist name for your exported audio.");
            string artist = artistPrompt.ShowDialog(this) == DialogResult.OK ? artistPrompt.Value : "";
            // tag the file we just wrote
            new WavMetadata
            {
                Title = Path.GetFileNameWithoutExtension(sfdSaveFile.FileName),
                Artist = artist,
                Year = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss")
            }.AppendTo(sfdSaveFile.FileName);
        }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Could not save audio", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        trackManager.SelectedTrack?.PanDrawSamples.Invalidate();
    }
    private void menuEditReverse_Click(object sender, EventArgs e)
    {
        ClearAudioSelection();
        trackManager.SelectedTrack?.Reverse();
        trackManager.SelectedTrack?.PanDrawSamples.Invalidate();
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

        using FrmGenRecord frm = new();
        frm.SetSound(trackManager.SelectedTrack);
        if (frm.ShowDialog() == DialogResult.Cancel)
        {
            ClearAudioSelection();
            trackManager.RemTrack(newTrack);
            panTracks.Controls.Remove(newTrack.PanDrawSamples);
            newTrack.PanDrawSamples.Dispose();
            newTrack.Dispose();
            UpdateSizes();
        }
    }
    private void btnNoise_Click(object sender, EventArgs e)
    {
        DialogTheme.StyleMenu(noiseMenu);
        noiseMenu.Show(btnNoise, new Point(0, btnNoise.Height));
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
        long samplePosition = trackManager.Tracks.Count == 0 ? 0 : Math.Max(0, trackManager.PlaybackSamplePosition);
        var elapsed = TimeSpan.FromSeconds(samplePosition / 44100.0);
        lblPlaybackTime.Text = $"{(int)elapsed.TotalHours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}.{elapsed.Milliseconds / 10:00}";
        bool hasTrack = trackManager.SelectedTrack != null && trackManager.Tracks.Contains(trackManager.SelectedTrack);
        foreach (var button in new[] { btnSaveCurrentTrack, btnRecordAppend, btnFlip, btnReverse, btnVolume, btnPitch, btnEliminateSilence })
            button.Enabled = hasTrack;
        btnSaveAudio.Enabled = trackManager.Tracks.Count > 0;
        btnRestart.Enabled = trackManager.Tracks.Count > 0;
        btnPlay.Enabled = trackManager.Tracks.Count > 0;
        if (trackManager.Tracks.Count > 0)
        {
            btnPlay.BackgroundImage = trackManager.State == NAudio.Wave.PlaybackState.Playing ? Resources.pause : Resources.play;
            FollowPlayback();
            lblPlayhead.Visible = true;
        }
        else
        {
            btnPlay.BackgroundImage = null;
            lblPlayhead.Visible = false;
        }
    }

    private void trkDrawTimescale_Scroll(object sender, EventArgs e)
    {
        long sample = trackManager.PlaybackSamplePosition;
        int position = lblPlayhead.Left;
        trackManager.DrawTimescale = Math.Max(1, trkDrawTimescale.Maximum - trkDrawTimescale.Value);
        viewportStartSample = Math.Max(0, sample - (long)position * trackManager.DrawTimescale);
        FollowPlayback();
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
            long hitSample = viewportStartSample + (long)e.X * Math.Max(1, trackManager.DrawTimescale);
            if (e.Y < ClipHandleHeight && hitSample >= track.OffsetSamples && hitSample < (long)track.OffsetSamples + track.SampleCount)
            {
                selectionTrack = track;
                trackMarkedForDeletion = track;
                UpdateDeleteAudioButton(true);
                lblDebug.Text = "Track selected — click the trash button to delete the entire track, or drag to move.";
                trackManager.StopForTimelineEdit();
                movingClip = track;
                clipDragX = e.X;
                clipOriginalOffset = track.OffsetSamples;
                clipDragScale = Math.Max(1, trackManager.DrawTimescale);
                pan.Cursor = Cursors.SizeWE;
                pan.Capture = true;
                return;
            }
            selectionTrack = track;
            selectionAnchor = SampleAt(track, e.X);
            selectingAudio = true;
            pan.Capture = true;
            UpdateAudioSelection(track, e.X);
        };
        pan.MouseMove += (sender, e) =>
        {
            if (movingClip == track)
            {
                track.OffsetSamples = (int)Math.Clamp(clipOriginalOffset + ((long)e.X - clipDragX) * clipDragScale,
                    0, Math.Max(0L, int.MaxValue - (long)track.SampleCount));
                lblDebug.Text = $"Clip starts at {track.OffsetSamples / 44100.0:F2}s";
                pan.Invalidate();
                return;
            }
            long position = viewportStartSample + (long)e.X * Math.Max(1, trackManager.DrawTimescale);
            pan.Cursor = e.Y < ClipHandleHeight && position >= track.OffsetSamples && position < (long)track.OffsetSamples + track.SampleCount
                ? Cursors.SizeWE : Cursors.Default;
            if (selectingAudio && selectionTrack == track) UpdateAudioSelection(track, e.X);
        };
        pan.MouseUp += (sender, e) =>
        {
            if (e.Button == MouseButtons.Left && movingClip == track)
            {
                movingClip = null;
                pan.Capture = false;
                pan.Cursor = Cursors.Default;
                pan.Invalidate();
                return;
            }
            if (e.Button != MouseButtons.Left || !selectingAudio || selectionTrack != track) return;
            UpdateAudioSelection(track, e.X);
            selectingAudio = false;
            pan.Capture = false;
        };
        pan.MouseCaptureChanged += (sender, e) =>
        {
            if (!pan.Capture)
            {
                selectingAudio = false;
                movingClip = null;
                pan.Cursor = Cursors.Default;
            }
        };
        pan.DoubleClick += (sender, e) =>
        {
            track.ToggleDisabled();
            track.PanDrawSamples.Invalidate();
        };
        pan.Paint += (sender, e) =>
        {
            track.DrawSamples(e.Graphics, trackManager.DrawTimescale, viewportStartSample);
            float handleScale = Math.Max(1, trackManager.DrawTimescale);
            int handleLeft = (int)Math.Clamp(((long)track.OffsetSamples - viewportStartSample) / handleScale, 0, pan.Width);
            int handleRight = (int)Math.Clamp(((long)track.OffsetSamples + track.SampleCount - viewportStartSample) / handleScale, 0, pan.Width);
            if (handleRight > handleLeft)
            {
                var handle = new Rectangle(handleLeft, 0, handleRight - handleLeft, ClipHandleHeight);
                using var fill = new SolidBrush(Color.FromArgb(65, 83, 96));
                e.Graphics.FillRectangle(fill, handle);
                TextRenderer.DrawText(e.Graphics, "↔ Drag to move", Font, handle, Color.White,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
            if (selectionTrack == track && selectionEnd > selectionStart)
            {
                float scale = Math.Max(1, trackManager.DrawTimescale);
                float left = (track.OffsetSamples + (long)Math.Min(selectionStart, track.SampleCount) - viewportStartSample) / scale;
                float right = (track.OffsetSamples + (long)Math.Min(selectionEnd, track.SampleCount) - viewportStartSample) / scale;
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
        FollowPlayback();
        lblPlayhead.Height = panTracks.Height;
        lblPlayhead.BringToFront();
        }
        finally { updatingTrackLayout = false; }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }
}
