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
        private void InitializeComponent()
        {
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
            menuFileSettings = new ToolStripMenuItem();
            menuEdit = new ToolStripMenuItem();
            menuEditFlip = new ToolStripMenuItem();
            menuEditReverse = new ToolStripMenuItem();
            menuEditVolume = new ToolStripMenuItem();
            menuEditPitch = new ToolStripMenuItem();
            menuEditEliminateSilence = new ToolStripMenuItem();
            menuDeleteSelectedAudio = new ToolStripMenuItem();
            menuGen = new ToolStripMenuItem();
            menuGenRecord = new ToolStripMenuItem();
            menuGenRecordAppend = new ToolStripMenuItem();
            menuGenNoise = new ToolStripMenuItem();
            menuGenGradientNoise = new ToolStripMenuItem();
            beatsToolStripMenuItem = new ToolStripMenuItem();
            menuGenInstruments = new ToolStripMenuItem();
            btnPlay = new Button();
            btnSaveCurrentTrack = new Button();
            tmrUpdate = new System.Windows.Forms.Timer(components);
            sfdSaveFile = new SaveFileDialog();
            trkDrawTimescale = new TrackBar();
            colorDialog = new ColorDialog();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            panel9 = new Panel();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            textBox8 = new TextBox();
            textBox9 = new TextBox();
            panTracks.SuspendLayout();
            menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkDrawTimescale).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            SuspendLayout();
            // 
            // panTracks
            // 
            panTracks.AutoScroll = true;
            panTracks.BackColor = Color.DarkGray;
            panTracks.BorderStyle = BorderStyle.Fixed3D;
            panTracks.Controls.Add(lblPlayhead);
            panTracks.Location = new Point(192, 127);
            panTracks.Name = "panTracks";
            panTracks.Size = new Size(1398, 642);
            panTracks.TabIndex = 0;
            // 
            // lblPlayhead
            // 
            lblPlayhead.BackColor = Color.White;
            lblPlayhead.Location = new Point(39, -2);
            lblPlayhead.Name = "lblPlayhead";
            lblPlayhead.Size = new Size(5, 642);
            lblPlayhead.TabIndex = 9;
            // 
            // lblDebug
            // 
            lblDebug.Dock = DockStyle.Bottom;
            lblDebug.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDebug.ForeColor = Color.FromArgb(255, 255, 192);
            lblDebug.Location = new Point(0, 769);
            lblDebug.Name = "lblDebug";
            lblDebug.Size = new Size(1604, 14);
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
            menuFile.Name = "menuFile";
            menuFile.Size = new Size(37, 20);
            menuFile.Text = "&File";
            // 
            // menuFileOpen
            // 
            menuFileOpen.Name = "menuFileOpen";
            menuFileOpen.Size = new Size(154, 22);
            menuFileOpen.Text = "Open...";
            menuFileOpen.ToolTipText = "Opens an audio file.";
            menuFileOpen.Click += menuFileOpen_Click;
            // 
            // menuFileSave
            // 
            menuFileSave.Name = "menuFileSave";
            menuFileSave.Size = new Size(154, 22);
            menuFileSave.Text = "Save...";
            menuFileSave.ToolTipText = "Saves the created audio to a .wav file.";
            menuFileSave.Click += menuFileSave_Click;
            // 
            // menuAudioLibrary
            // 
            menuAudioLibrary.Name = "menuAudioLibrary";
            menuAudioLibrary.Size = new Size(154, 22);
            menuAudioLibrary.Text = "Audio Library...";
            menuAudioLibrary.ToolTipText = "Browse and reuse saved audio clips.";
            menuAudioLibrary.Click += menuAudioLibrary_Click;
            // 
            // menuFileSettings
            // 
            menuFileSettings.Name = "menuFileSettings";
            menuFileSettings.Size = new Size(154, 22);
            menuFileSettings.Text = "Settings...";
            menuFileSettings.ToolTipText = "Adjust the colors of the program.";
            menuFileSettings.Click += menuFileSettings_Click;
            // 
            // menuEdit
            // 
            menuEdit.DropDownItems.AddRange(new ToolStripItem[] { menuEditFlip, menuEditReverse, menuEditVolume, menuEditPitch, menuEditEliminateSilence, menuDeleteSelectedAudio });
            menuEdit.Name = "menuEdit";
            menuEdit.Size = new Size(39, 20);
            menuEdit.Text = "E&dit";
            // 
            // menuEditFlip
            // 
            menuEditFlip.Name = "menuEditFlip";
            menuEditFlip.Size = new Size(189, 22);
            menuEditFlip.Text = "Flip";
            menuEditFlip.ToolTipText = "Inverts the waveform of the track.";
            menuEditFlip.Click += menuEditFlip_Click;
            // 
            // menuEditReverse
            // 
            menuEditReverse.Name = "menuEditReverse";
            menuEditReverse.Size = new Size(189, 22);
            menuEditReverse.Text = "Reverse";
            menuEditReverse.ToolTipText = "Reverses the audio of the tracks.";
            menuEditReverse.Click += menuEditReverse_Click;
            // 
            // menuEditVolume
            // 
            menuEditVolume.Name = "menuEditVolume";
            menuEditVolume.Size = new Size(189, 22);
            menuEditVolume.Text = "Alter Volume...";
            menuEditVolume.ToolTipText = "Raise or lower the volume.";
            menuEditVolume.Click += menuEditVolume_Click;
            // 
            // menuEditPitch
            // 
            menuEditPitch.Name = "menuEditPitch";
            menuEditPitch.Size = new Size(189, 22);
            menuEditPitch.Text = "Alter Pitch...";
            menuEditPitch.ToolTipText = "Raise or lower the pitch of the selected track in semitones.";
            menuEditPitch.Click += menuEditPitch_Click;
            // 
            // menuEditEliminateSilence
            // 
            menuEditEliminateSilence.Name = "menuEditEliminateSilence";
            menuEditEliminateSilence.Size = new Size(189, 22);
            menuEditEliminateSilence.Text = "Eliminate Silence...";
            menuEditEliminateSilence.ToolTipText = "Algorithmically eliminates silence from the track.";
            menuEditEliminateSilence.Click += menuEditEliminateSilence_Click;
            // 
            // menuDeleteSelectedAudio
            // 
            menuDeleteSelectedAudio.Enabled = false;
            menuDeleteSelectedAudio.Name = "menuDeleteSelectedAudio";
            menuDeleteSelectedAudio.Size = new Size(189, 22);
            menuDeleteSelectedAudio.Text = "Delete Selected Audio";
            menuDeleteSelectedAudio.Click += menuDeleteSelectedAudio_Click;
            // 
            // menuGen
            // 
            menuGen.DropDownItems.AddRange(new ToolStripItem[] { menuGenRecord, menuGenRecordAppend, menuGenNoise, menuGenGradientNoise, beatsToolStripMenuItem, menuGenInstruments });
            menuGen.Name = "menuGen";
            menuGen.Size = new Size(66, 20);
            menuGen.Text = "&Generate";
            // 
            // menuGenRecord
            // 
            menuGenRecord.Name = "menuGenRecord";
            menuGenRecord.Size = new Size(152, 22);
            menuGenRecord.Text = "Record new audio...";
            menuGenRecord.ToolTipText = "Records audio from the microphone.";
            menuGenRecord.Click += menuGenRecord_Click;
            // 
            // menuGenRecordAppend
            // 
            menuGenRecordAppend.Name = "menuGenRecordAppend";
            menuGenRecordAppend.Size = new Size(152, 22);
            menuGenRecordAppend.Text = "Record onto existing Track...";
            menuGenRecordAppend.ToolTipText = "Records from the microphone and adds it to the end of the selected track.";
            menuGenRecordAppend.Click += menuGenRecordAppend_Click;
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
            // beatsToolStripMenuItem
            // 
            beatsToolStripMenuItem.Name = "beatsToolStripMenuItem";
            beatsToolStripMenuItem.Size = new Size(152, 22);
            beatsToolStripMenuItem.Text = "Beats";
            beatsToolStripMenuItem.ToolTipText = "Choose a premade beat to add as a new track.";
            beatsToolStripMenuItem.Click += menuGenBeats_Click;
            // 
            // menuGenInstruments
            // 
            menuGenInstruments.Name = "menuGenInstruments";
            menuGenInstruments.Size = new Size(152, 22);
            menuGenInstruments.Text = "Instruments";
            menuGenInstruments.ToolTipText = "View instruments for beat creation.";
            menuGenInstruments.Click += menuGenInstruments_Click;
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
            btnPlay.Location = new Point(870, 27);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(37, 38);
            btnPlay.TabIndex = 6;
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click;
            // 
            // btnSaveCurrentTrack
            // 
            btnSaveCurrentTrack.Location = new Point(1464, 34);
            btnSaveCurrentTrack.Name = "btnSaveCurrentTrack";
            btnSaveCurrentTrack.Size = new Size(126, 32);
            btnSaveCurrentTrack.TabIndex = 8;
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
            trkDrawTimescale.BackColor = Color.DimGray;
            trkDrawTimescale.LargeChange = 50;
            trkDrawTimescale.Location = new Point(192, 76);
            trkDrawTimescale.Maximum = 1000;
            trkDrawTimescale.Minimum = 1;
            trkDrawTimescale.Name = "trkDrawTimescale";
            trkDrawTimescale.Size = new Size(1398, 45);
            trkDrawTimescale.SmallChange = 10;
            trkDrawTimescale.TabIndex = 7;
            trkDrawTimescale.TickFrequency = 10;
            trkDrawTimescale.Value = 500;
            trkDrawTimescale.Scroll += trkDrawTimescale_Scroll;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DimGray;
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(15, 127);
            panel1.Name = "panel1";
            panel1.Size = new Size(171, 66);
            panel1.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DimGray;
            panel2.Controls.Add(textBox2);
            panel2.Location = new Point(15, 199);
            panel2.Name = "panel2";
            panel2.Size = new Size(171, 66);
            panel2.TabIndex = 10;
            // 
            // panel3
            // 
            panel3.BackColor = Color.DimGray;
            panel3.Controls.Add(textBox3);
            panel3.Location = new Point(15, 271);
            panel3.Name = "panel3";
            panel3.Size = new Size(171, 66);
            panel3.TabIndex = 10;
            // 
            // panel4
            // 
            panel4.BackColor = Color.DimGray;
            panel4.Controls.Add(textBox4);
            panel4.Location = new Point(15, 343);
            panel4.Name = "panel4";
            panel4.Size = new Size(171, 66);
            panel4.TabIndex = 10;
            // 
            // panel5
            // 
            panel5.BackColor = Color.DimGray;
            panel5.Controls.Add(textBox5);
            panel5.Location = new Point(15, 415);
            panel5.Name = "panel5";
            panel5.Size = new Size(171, 66);
            panel5.TabIndex = 10;
            // 
            // panel6
            // 
            panel6.BackColor = Color.DimGray;
            panel6.Controls.Add(textBox6);
            panel6.Location = new Point(15, 487);
            panel6.Name = "panel6";
            panel6.Size = new Size(171, 66);
            panel6.TabIndex = 10;
            // 
            // panel7
            // 
            panel7.BackColor = Color.DimGray;
            panel7.Controls.Add(textBox7);
            panel7.Location = new Point(15, 559);
            panel7.Name = "panel7";
            panel7.Size = new Size(171, 66);
            panel7.TabIndex = 10;
            // 
            // panel8
            // 
            panel8.BackColor = Color.DimGray;
            panel8.Controls.Add(textBox8);
            panel8.Location = new Point(15, 631);
            panel8.Name = "panel8";
            panel8.Size = new Size(171, 66);
            panel8.TabIndex = 10;
            // 
            // panel9
            // 
            panel9.BackColor = Color.DimGray;
            panel9.Controls.Add(textBox9);
            panel9.Location = new Point(15, 703);
            panel9.Name = "panel9";
            panel9.Size = new Size(171, 66);
            panel9.TabIndex = 10;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Silver;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.ForeColor = Color.Black;
            textBox1.Location = new Point(3, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 0;
            textBox1.Text = "Track 1";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Silver;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.ForeColor = Color.Black;
            textBox2.Location = new Point(3, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 1;
            textBox2.Text = "Track 2";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.Silver;
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.ForeColor = Color.Black;
            textBox3.Location = new Point(3, 3);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 2;
            textBox3.Text = "Track 3";
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.Silver;
            textBox4.BorderStyle = BorderStyle.FixedSingle;
            textBox4.ForeColor = Color.Black;
            textBox4.Location = new Point(3, 3);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 11;
            textBox4.Text = "Track 4";
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.Silver;
            textBox5.BorderStyle = BorderStyle.FixedSingle;
            textBox5.ForeColor = Color.Black;
            textBox5.Location = new Point(3, 3);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 12;
            textBox5.Text = "Track 5";
            // 
            // textBox6
            // 
            textBox6.BackColor = Color.Silver;
            textBox6.BorderStyle = BorderStyle.FixedSingle;
            textBox6.ForeColor = Color.Black;
            textBox6.Location = new Point(3, 3);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 23);
            textBox6.TabIndex = 13;
            textBox6.Text = "Track 6";
            // 
            // textBox7
            // 
            textBox7.BackColor = Color.Silver;
            textBox7.BorderStyle = BorderStyle.FixedSingle;
            textBox7.ForeColor = Color.Black;
            textBox7.Location = new Point(3, 3);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(100, 23);
            textBox7.TabIndex = 14;
            textBox7.Text = "Track 7";
            // 
            // textBox8
            // 
            textBox8.BackColor = Color.Silver;
            textBox8.BorderStyle = BorderStyle.FixedSingle;
            textBox8.ForeColor = Color.Black;
            textBox8.Location = new Point(3, 3);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(100, 23);
            textBox8.TabIndex = 15;
            textBox8.Text = "Track 8";
            // 
            // textBox9
            // 
            textBox9.BackColor = Color.Silver;
            textBox9.BorderStyle = BorderStyle.FixedSingle;
            textBox9.ForeColor = Color.Black;
            textBox9.Location = new Point(3, 3);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(100, 23);
            textBox9.TabIndex = 16;
            textBox9.Text = "Track 9";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1604, 783);
            Controls.Add(panel9);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnPlay);
            Controls.Add(trkDrawTimescale);
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
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
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
    private ToolStripMenuItem menuGenRecordAppend;
    private ToolStripMenuItem beatsToolStripMenuItem;
    private ToolStripMenuItem menuFileSettings;
        private Panel panel1;
        private TextBox textBox1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        private TextBox textBox2;
        private TextBox textBox4;
        private TextBox textBox6;
        private TextBox textBox8;
        private TextBox textBox9;
        private TextBox textBox3;
        private TextBox textBox5;
        private TextBox textBox7;
    }
}
