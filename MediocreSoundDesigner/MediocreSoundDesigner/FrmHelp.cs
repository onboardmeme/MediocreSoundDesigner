namespace MediocreSoundDesigner;

/// <summary>A searchable-by-topic guide to the editor's controls.</summary>
public sealed class FrmHelp : Form
{
    /// <summary>Creates the user guide.</summary>
    public FrmHelp()
    {
        Text = "User Guide";
        Font = new Font("Segoe UI", 10F);
        BackColor = Settings.Color.WinBg;
        ForeColor = Settings.Color.WinFg;
        StartPosition = FormStartPosition.CenterParent;
        ShowInTaskbar = false;
        MinimizeBox = false;
        ClientSize = new Size(820, 560);
        MinimumSize = new Size(660, 450);

        var topics = new (string Title, string Body)[]
        {
            ("Getting started", "1. Hover over any toolbar icon to see its action.\r\n\r\n2. Open an audio file, generate noise, add an instrument, or record audio to create a track.\r\n\r\n3. Click a waveform to select its track. Each waveform lines up with a numbered box on the left.\r\n\r\n4. Press Play to listen, then use the editing buttons on the right of Play.\r\n\r\nGrey buttons need a track or an audio selection before they can be used. Choose a topic on the left for details."),
            ("Files & audio library", "Open Audio: Choose an audio file to add as a new track.\r\n\r\nSave Audio: Export the combined tracks to a WAV file. You can also enter an artist name for the exported file.\r\n\r\nSave Current Track: Name and save the selected track to the application's clip library for reuse. This is separate from exporting your mix.\r\n\r\nAudio Library: Select a saved clip and use Add to insert it as a new track. Delete removes the saved library copy; a copy already added to the project remains available.\r\n\r\nThe library stores clips on this computer. It does not save the complete multitrack project."),
            ("Generate & instruments", "Record New: Open the recording window to create a new track.\r\n\r\nRecord Append: Record audio to add to the end of the selected track.\r\n\r\nGenerate Noise: Choose Noise or Gradient Noise from the popup menu to create a track.\r\n\r\nPremade Beats: Select a beat and click Add. A beat marked 'audio file not installed' is unavailable until its file is supplied.\r\n\r\nInstruments: Select a sound, click Preview to listen, Stop to end the preview, or Add to create a track. Available sounds include snare, rimshot, cross-stick, ride, ride bell, and pedal hi-hat."),
            ("Recording", "1. Connect a microphone and allow microphone access in Windows.\r\n\r\n2. Choose Record New or Record Append.\r\n\r\n3. Click the record circle to begin capturing audio. Click the stop square to finish.\r\n\r\n4. Wait for recording to stop, then click Apply to keep the captured sound. Apply stays disabled until audio is ready.\r\n\r\nCancel discards this recording. Cancelling Record New also removes its temporary empty track."),
            ("Playback & navigation", "Play / Pause: Start playback from the current position or pause it. Press again to resume.\r\n\r\nRestart: Start playback again from the beginning.\r\n\r\nTimer: Shows the current audio position as hours:minutes:seconds.hundredths. It holds when paused and updates when you seek.\r\n\r\nPlayhead: Drag the vertical playback line left or right to choose where playback continues.\r\n\r\nZoom: Move the slider above the waveforms to change how much audio time is visible.\r\n\r\nDuring playback, the waveforms scroll when needed to keep the playhead visible. Scroll the track area vertically to reach additional tracks."),
            ("Tracks & selections", "Select a track: Click its waveform. Editing buttons act on that track.\r\n\r\nRename: Edit the name in the track box on the left. Press Enter to leave the name field.\r\n\r\nMute / unmute: Double-click a waveform to exclude or include its track in playback and the exported mix.\r\n\r\nMove audio: Drag the strip labeled 'Drag to move' at the top of a clip left or right to set its start time. Audio cannot be moved before the beginning. Playback stops while you reposition a clip; press Play to hear the updated placement.\r\n\r\nYou can also move audio with the keyboard: With a track selected and the name field unfocused, use the Left and Right arrow keys to shift it in time. The step depends on the zoom level.\r\n\r\nSelect a section: Click and drag across a waveform. The highlighted area is the selected audio range.\r\n\r\nDelete Selected Audio: Click the delete icon or press Delete, then confirm. The remaining sections join together.\r\n\r\nDelete a whole track: Click the Drag to move strip to enable the trash button, then click the trash button and confirm. You can also click a waveform without dragging to leave no range selected, then press Delete and confirm. Deletions cannot be undone."),
            ("Editing audio", "Select a track before using these buttons. Range selection is used for deleting sections; the following edits affect the selected track as a whole.\r\n\r\nFlip: Invert the waveform's polarity.\r\n\r\nReverse: Play the track's audio in reverse order.\r\n\r\nVolume: Set a percentage and click Apply. 100% is the original level; 0% is silent.\r\n\r\nPitch: Enter semitones and click Apply. Positive values raise pitch and negative values lower it. This also changes duration and playback speed.\r\n\r\nRemove Silence: Choose the beginning, end, both ends, or anywhere. Set the minimum silent duration and the threshold used to identify silence, then Apply.\r\n\r\nCancel closes an edit dialog without applying its settings. Save a library copy before destructive edits you may want to reverse."),
            ("Settings & shortcuts", "Settings: Change the window, track, and playhead colors. Click a color button to choose a color, then Apply to use your changes. Restore Defaults fills in the default colors; Cancel leaves the current settings unchanged.\r\n\r\nF1: Open this user guide.\r\n\r\nLeft / Right: Shift the selected track in time.\r\n\r\nDelete: Delete the highlighted range, or the selected track when no range is highlighted. Confirmation is required.\r\n\r\nEnter / Escape while editing a track name: Leave the text field.\r\n\r\nShortcuts for editing audio do not run while you are typing in a text field.")
        };
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 2, RowCount = 3 };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        var heading = new Label { Text = "User Guide", Dock = DockStyle.Fill, Font = new Font(Font, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft };
        root.Controls.Add(heading, 0, 0);
        root.SetColumnSpan(heading, 2);
        var navigation = new ListBox { Name = "lstTopics", Dock = DockStyle.Fill, IntegralHeight = false, BackColor = Color.FromArgb(196, 200, 203), ForeColor = Color.Black, BorderStyle = BorderStyle.FixedSingle, AccessibleName = "Guide topics" };
        var content = new TextBox { Name = "txtGuide", Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, BackColor = Color.FromArgb(229, 232, 234), ForeColor = Color.Black, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(12, 3, 0, 3), AccessibleName = "Guide instructions" };
        navigation.Items.AddRange(topics.Select(t => (object)t.Title).ToArray());
        navigation.SelectedIndexChanged += (_, _) =>
        {
            if (navigation.SelectedIndex < 0) return;
            var topic = topics[navigation.SelectedIndex];
            content.Text = topic.Title + "\r\n\r\n" + topic.Body;
            content.SelectionStart = 0;
            content.ScrollToCaret();
        };
        root.Controls.Add(navigation, 0, 1);
        root.Controls.Add(content, 1, 1);
        var close = new Button { Text = "Close", DialogResult = DialogResult.Cancel, Anchor = AnchorStyles.Right | AnchorStyles.Bottom, Size = new Size(88, 32), BackColor = Color.FromArgb(196, 200, 203), ForeColor = Color.Black, FlatStyle = FlatStyle.Flat };
        root.Controls.Add(close, 1, 2);
        Controls.Add(root);
        CancelButton = close;
        navigation.SelectedIndex = 0;
    }
}
