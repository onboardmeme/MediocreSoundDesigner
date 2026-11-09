namespace MediocreSoundDesigner
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      components = new System.ComponentModel.Container();
      panTracks = new Panel();
      lblPlayhead = new Label();
      lblDebug = new Label();
      ofdLoadFile = new OpenFileDialog();
      menu = new MenuStrip();
      menuFile = new ToolStripMenuItem();
      menuFileOpen = new ToolStripMenuItem();
      menuFileSave = new ToolStripMenuItem();
      menuAudioLibrary = new ToolStripMenuItem();
      menuEdit = new ToolStripMenuItem();
      menuDeleteSelectedAudio = new ToolStripMenuItem();
      menuEditFlip = new ToolStripMenuItem();
      menuEditReverse = new ToolStripMenuItem();
      menuEditVolume = new ToolStripMenuItem();
      menuEditPitch = new ToolStripMenuItem();
      menuEditEliminateSilence = new ToolStripMenuItem();
      menuGen = new ToolStripMenuItem();
      menuGenInstruments = new ToolStripMenuItem();
      menuGenRecord = new ToolStripMenuItem();
      beatsToolStripMenuItem = new ToolStripMenuItem();
      menuGenNoise = new ToolStripMenuItem();
      menuGenGradientNoise = new ToolStripMenuItem();
      btnPlay = new Button();
      btnSaveCurrentTrack = new Button();
      tmrUpdate = new System.Windows.Forms.Timer(components);
      sfdSaveFile = new SaveFileDialog();
      trkDrawTimescale = new TrackBar();
      colorDialog = new ColorDialog();
      menuFileSettings = new ToolStripMenuItem();
      panTracks.SuspendLayout();
      menu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)trkDrawTimescale).BeginInit();
      SuspendLayout();
      // 
      // panTracks
      // 
      panTracks.AutoScroll = true;
      panTracks.BackColor = Color.DimGray;
      panTracks.BorderStyle = BorderStyle.Fixed3D;
      panTracks.Controls.Add(lblPlayhead);
      panTracks.Location = new Point(10, 102);
      panTracks.Name = "panTracks";
      panTracks.Size = new Size(1580, 541);
      panTracks.TabIndex = 0;
      // 
      // lblPlayhead
      // 
      lblPlayhead.BackColor = Color.White;
      lblPlayhead.Location = new Point(39, -2);
      lblPlayhead.Name = "lblPlayhead";
      lblPlayhead.Size = new Size(5, 385);
      lblPlayhead.TabIndex = 9;
      // 
      // lblDebug
      // 
      lblDebug.Dock = DockStyle.Bottom;
      lblDebug.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lblDebug.ForeColor = Color.FromArgb(255, 255, 192);
      lblDebug.Location = new Point(0, 746);
      lblDebug.Name = "lblDebug";
      lblDebug.Size = new Size(1604, 37);
      lblDebug.TabIndex = 1;
      // 
      // ofdLoadFile
      // 
      ofdLoadFile.FileName = "openFileDialog1";
      // 
      // menu
      // 
      menu.Items.AddRange(new ToolStripItem[] { menuFile, menuEdit, menuGen });
      menu.Location = new Point(0, 0);
      menu.Name = "menu";
      menu.Size = new Size(1604, 24);
      menu.TabIndex = 5;
      menu.Text = "menuStrip1";
      // 
      // menuFile
      // 
      menuFile.DropDownItems.AddRange(new ToolStripItem[] { menuFileOpen, menuFileSave, menuAudioLibrary, menuFileSettings });
      menuAudioLibrary.Name = "menuAudioLibrary";
      menuAudioLibrary.Text = "Audio Library...";
      menuAudioLibrary.ToolTipText = "Browse and reuse saved audio clips.";
      menuAudioLibrary.Click += menuAudioLibrary_Click;
      menuFile.Name = "menuFile";
      menuFile.Size = new Size(37, 20);
      menuFile.Text = "&File";
      // 
      // menuFileOpen
      // 
      menuFileOpen.Name = "menuFileOpen";
      menuFileOpen.Size = new Size(180, 22);
      menuFileOpen.Text = "Open...";
      menuFileOpen.ToolTipText = "Opens an audio file.";
      menuFileOpen.Click += menuFileOpen_Click;
      // 
      // menuFileSave
      // 
      menuFileSave.Name = "menuFileSave";
      menuFileSave.Size = new Size(180, 22);
      menuFileSave.Text = "Save...";
      menuFileSave.ToolTipText = "Saves the created audio to a .wav file.";
      menuFileSave.Click += menuFileSave_Click;
      // 
      // menuEdit
      // 
      menuEdit.DropDownItems.AddRange(new ToolStripItem[] { menuEditFlip, menuEditReverse, menuEditVolume, menuEditPitch, menuEditEliminateSilence, menuDeleteSelectedAudio });
      menuDeleteSelectedAudio.Name = "menuDeleteSelectedAudio";
      menuDeleteSelectedAudio.Text = "Delete Selected Audio";
      menuDeleteSelectedAudio.Enabled = false;
      menuDeleteSelectedAudio.Click += menuDeleteSelectedAudio_Click;
      menuEdit.Name = "menuEdit";
      menuEdit.Size = new Size(39, 20);
      menuEdit.Text = "E&dit";
      // 
      // menuEditFlip
      // 
      menuEditFlip.Name = "menuEditFlip";
      menuEditFlip.Size = new Size(180, 22);
      menuEditFlip.Text = "Flip";
      menuEditFlip.ToolTipText = "Inverts the waveform of the track.";
      menuEditFlip.Click += menuEditFlip_Click;
      // 
      // menuEditReverse
      // 
      menuEditReverse.Name = "menuEditReverse";
      menuEditReverse.Size = new Size(180, 22);
      menuEditReverse.Text = "Reverse";
      menuEditReverse.ToolTipText = "Reverses the audio of the tracks.";
      menuEditReverse.Click += menuEditReverse_Click;
      // 
      // menuEditVolume
      // 
      menuEditVolume.Name = "menuEditVolume";
      menuEditVolume.Size = new Size(180, 22);
      menuEditVolume.Text = "Alter Volume...";
      menuEditVolume.ToolTipText = "Raise or lower the volume.";
      menuEditVolume.Click += menuEditVolume_Click;
      // 
      // menuEditPitch
      // 
      menuEditPitch.Name = "menuEditPitch";
      menuEditPitch.Size = new Size(180, 22);
      menuEditPitch.Text = "Alter Pitch...";
      menuEditPitch.ToolTipText = "Raise or lower the pitch of the selected track in semitones.";
      menuEditPitch.Click += menuEditPitch_Click;
      // 
      // menuEditEliminateSilence
      // 
      menuEditEliminateSilence.Name = "menuEditEliminateSilence";
      menuEditEliminateSilence.Size = new Size(180, 22);
      menuEditEliminateSilence.Text = "Eliminate Silence...";
      menuEditEliminateSilence.ToolTipText = "Algorithmically eliminates silence from the track.";
      menuEditEliminateSilence.Click += menuEditEliminateSilence_Click;
      // 
      // menuGen
      // 
      menuGen.DropDownItems.AddRange(new ToolStripItem[] { menuGenRecord, menuGenNoise, menuGenGradientNoise, beatsToolStripMenuItem, menuGenInstruments });
      menuGenInstruments.Name = "menuGenInstruments";
      menuGenInstruments.Text = "Instruments";
      menuGenInstruments.ToolTipText = "View instruments for beat creation.";
      menuGenInstruments.Click += menuGenInstruments_Click;
      beatsToolStripMenuItem.Name = "beatsToolStripMenuItem";
      beatsToolStripMenuItem.Text = "Beats";
      beatsToolStripMenuItem.ToolTipText = "Choose a premade beat to add as a new track.";
      beatsToolStripMenuItem.Click += menuGenBeats_Click;
      menuGen.Name = "menuGen";
      menuGen.Size = new Size(66, 20);
      menuGen.Text = "&Generate";
      // 
      // menuGenRecord
      // 
      menuGenRecord.Name = "menuGenRecord";
      menuGenRecord.Size = new Size(152, 22);
      menuGenRecord.Text = "Record...";
      menuGenRecord.ToolTipText = "Records audio from the microphone.";
      menuGenRecord.Click += menuGenRecord_Click;
      // 
      // menuGenNoise
      // 
      menuGenNoise.Name = "menuGenNoise";
      menuGenNoise.Size = new Size(152, 22);
      menuGenNoise.Text = "Noise";
      menuGenNoise.ToolTipText = "Generates random noise.";
      menuGenNoise.Click += menuGenNoise_Click;
      // 
      // menuGenGradientNoise
      // 
      menuGenGradientNoise.Name = "menuGenGradientNoise";
      menuGenGradientNoise.Size = new Size(152, 22);
      menuGenGradientNoise.Text = "Gradient Noise";
      menuGenGradientNoise.ToolTipText = "Generates random gradient noise.";
      menuGenGradientNoise.Click += menuGenGradientNoise_Click;
      // 
      // btnPlay
      // 
      btnPlay.AutoSize = true;
      btnPlay.BackColor = Color.Transparent;
      btnPlay.BackgroundImage = Properties.Resources.play;
      btnPlay.BackgroundImageLayout = ImageLayout.Stretch;
      btnPlay.FlatAppearance.BorderSize = 0;
      btnPlay.FlatAppearance.MouseDownBackColor = Color.Transparent;
      btnPlay.FlatAppearance.MouseOverBackColor = Color.Transparent;
      btnPlay.FlatStyle = FlatStyle.Flat;
      btnPlay.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
      btnPlay.Location = new Point(717, 661);
      btnPlay.Name = "btnPlay";
      btnPlay.Size = new Size(100, 100);
      btnPlay.TabIndex = 6;
      btnPlay.UseVisualStyleBackColor = false;
      btnPlay.Click += btnPlay_Click;
      btnSaveCurrentTrack.Location = new Point(15, 665);
      btnSaveCurrentTrack.Size = new Size(160, 32);
      btnSaveCurrentTrack.Name = "btnSaveCurrentTrack";
      btnSaveCurrentTrack.Text = "Save Current Track";
      btnSaveCurrentTrack.UseVisualStyleBackColor = true;
      btnSaveCurrentTrack.Click += btnSaveCurrentTrack_Click;
      // 
      // tmrUpdate
      // 
      tmrUpdate.Enabled = true;
      tmrUpdate.Interval = 20;
      tmrUpdate.Tick += tmrUpdatePlaybackState_Tick;
      // 
      // trkDrawTimescale
      // 
      trkDrawTimescale.LargeChange = 50;
      trkDrawTimescale.Location = new Point(12, 55);
      trkDrawTimescale.Maximum = 1000;
      trkDrawTimescale.Minimum = 1;
      trkDrawTimescale.Name = "trkDrawTimescale";
      trkDrawTimescale.Size = new Size(1580, 45);
      trkDrawTimescale.SmallChange = 10;
      trkDrawTimescale.TabIndex = 7;
      trkDrawTimescale.TickFrequency = 10;
      trkDrawTimescale.Value = 500;
      trkDrawTimescale.Scroll += trkDrawTimescale_Scroll;
      // 
      // menuFileSettings
      // 
      menuFileSettings.Name = "menuFileSettings";
      menuFileSettings.Size = new Size(180, 22);
      menuFileSettings.Text = "Settings...";
      menuFileSettings.ToolTipText = "Adjust the colors of the program.";
      menuFileSettings.Click += menuFileSettings_Click;
      // 
      // FrmMain
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.Black;
      ClientSize = new Size(1604, 783);
      Controls.Add(trkDrawTimescale);
      Controls.Add(btnPlay);
      Controls.Add(btnSaveCurrentTrack);
      Controls.Add(lblDebug);
      Controls.Add(panTracks);
      Controls.Add(menu);
      KeyPreview = true;
      MainMenuStrip = menu;
      Name = "FrmMain";
      Text = "Mediocre Sound Designer";
      SizeChanged += FrmMain_SizeChanged;
      panTracks.ResumeLayout(false);
      menu.ResumeLayout(false);
      menu.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)trkDrawTimescale).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Panel panTracks;
    private Label lblDebug;
    private OpenFileDialog ofdLoadFile;
    private MenuStrip menu;
    private ToolStripMenuItem menuEdit;
    private ToolStripMenuItem menuDeleteSelectedAudio;
    private Button btnPlay;
    private Button btnSaveCurrentTrack;
    private ToolStripMenuItem menuEditVolume;
    private ToolStripMenuItem menuEditPitch;
    private ToolStripMenuItem menuEditReverse;
    private ToolStripMenuItem menuFile;
    private ToolStripMenuItem menuFileOpen;
    private ToolStripMenuItem menuFileSave;
    private ToolStripMenuItem menuAudioLibrary;
    private System.Windows.Forms.Timer tmrUpdate;
    private ToolStripMenuItem menuGen;
    private ToolStripMenuItem menuGenInstruments;
    private ToolStripMenuItem menuGenNoise;
    private ToolStripMenuItem menuGenGradientNoise;
    private ToolStripMenuItem menuEditFlip;
    private SaveFileDialog sfdSaveFile;
    private TrackBar trkDrawTimescale;
    private ColorDialog colorDialog;
    private ToolStripMenuItem menuEditEliminateSilence;
    private Label lblPlayhead;
    private ToolStripMenuItem menuGenRecord;
    private ToolStripMenuItem beatsToolStripMenuItem;
    private ToolStripMenuItem menuFileSettings;
  }
}
