namespace DevoPlayer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblCurrentlyPlaying = new System.Windows.Forms.Label();
            this.lblCurrentSongLength = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.seekBar = new System.Windows.Forms.TrackBar();
            this.seekerUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnRandom = new System.Windows.Forms.Button();
            this.chkShuffle = new System.Windows.Forms.CheckBox();
            this.tkbVolume = new System.Windows.Forms.TrackBar();
            this.lblShuffleSongsLeft = new System.Windows.Forms.Label();
            this.lblTotalSongs = new System.Windows.Forms.Label();
            this.lvPLaylist = new System.Windows.Forms.ListView();
            this.chkIsAutoSave = new System.Windows.Forms.CheckBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbxVolume = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.seekBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(17, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(32, 32);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(93, 12);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(32, 32);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(169, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(32, 32);
            this.btnStop.TabIndex = 2;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblCurrentlyPlaying
            // 
            this.lblCurrentlyPlaying.AutoSize = true;
            this.lblCurrentlyPlaying.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentlyPlaying.Location = new System.Drawing.Point(7, 78);
            this.lblCurrentlyPlaying.Name = "lblCurrentlyPlaying";
            this.lblCurrentlyPlaying.Size = new System.Drawing.Size(312, 26);
            this.lblCurrentlyPlaying.TabIndex = 6;
            this.lblCurrentlyPlaying.Text = "Currently playing: --- nothing ---";
            // 
            // lblCurrentSongLength
            // 
            this.lblCurrentSongLength.AutoSize = true;
            this.lblCurrentSongLength.Location = new System.Drawing.Point(695, 107);
            this.lblCurrentSongLength.Name = "lblCurrentSongLength";
            this.lblCurrentSongLength.Size = new System.Drawing.Size(46, 13);
            this.lblCurrentSongLength.TabIndex = 7;
            this.lblCurrentSongLength.Text = "Length: ";
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(131, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(32, 32);
            this.btnPause.TabIndex = 8;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // seekBar
            // 
            this.seekBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.seekBar.LargeChange = 1000;
            this.seekBar.Location = new System.Drawing.Point(12, 107);
            this.seekBar.Name = "seekBar";
            this.seekBar.Size = new System.Drawing.Size(677, 45);
            this.seekBar.TabIndex = 9;
            this.seekBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.seekBar.Scroll += new System.EventHandler(this.seekBar_Scroll);
            this.seekBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.seekBar_MouseDown);
            // 
            // seekerUpdate
            // 
            this.seekerUpdate.Interval = 500;
            this.seekerUpdate.Tick += new System.EventHandler(this.seekerUpdate_Tick);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(205, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 32);
            this.btnNext.TabIndex = 12;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(55, 12);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(32, 32);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(243, 12);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(32, 32);
            this.btnRandom.TabIndex = 14;
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // chkShuffle
            // 
            this.chkShuffle.AutoSize = true;
            this.chkShuffle.Location = new System.Drawing.Point(281, 12);
            this.chkShuffle.Name = "chkShuffle";
            this.chkShuffle.Size = new System.Drawing.Size(59, 17);
            this.chkShuffle.TabIndex = 15;
            this.chkShuffle.Text = "Shuffle";
            this.chkShuffle.UseVisualStyleBackColor = true;
            this.chkShuffle.CheckedChanged += new System.EventHandler(this.chkShuffle_CheckedChanged);
            // 
            // tkbVolume
            // 
            this.tkbVolume.Location = new System.Drawing.Point(479, 25);
            this.tkbVolume.Maximum = 1000;
            this.tkbVolume.Name = "tkbVolume";
            this.tkbVolume.Size = new System.Drawing.Size(246, 45);
            this.tkbVolume.TabIndex = 20;
            this.tkbVolume.TickFrequency = 100;
            this.tkbVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tkbVolume.Value = 1000;
            this.tkbVolume.Scroll += new System.EventHandler(this.tkbVolume_Scroll);
            this.tkbVolume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tkbVolume_MouseDown);
            // 
            // lblShuffleSongsLeft
            // 
            this.lblShuffleSongsLeft.AutoSize = true;
            this.lblShuffleSongsLeft.Location = new System.Drawing.Point(14, 44);
            this.lblShuffleSongsLeft.Name = "lblShuffleSongsLeft";
            this.lblShuffleSongsLeft.Size = new System.Drawing.Size(94, 13);
            this.lblShuffleSongsLeft.TabIndex = 21;
            this.lblShuffleSongsLeft.Text = "Shuffle songs left: ";
            // 
            // lblTotalSongs
            // 
            this.lblTotalSongs.AutoSize = true;
            this.lblTotalSongs.Location = new System.Drawing.Point(14, 57);
            this.lblTotalSongs.Name = "lblTotalSongs";
            this.lblTotalSongs.Size = new System.Drawing.Size(85, 13);
            this.lblTotalSongs.TabIndex = 22;
            this.lblTotalSongs.Text = "Total songs left: ";
            // 
            // lvPLaylist
            // 
            this.lvPLaylist.AllowColumnReorder = true;
            this.lvPLaylist.AllowDrop = true;
            this.lvPLaylist.AutoArrange = false;
            this.lvPLaylist.FullRowSelect = true;
            this.lvPLaylist.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3});
            this.lvPLaylist.Location = new System.Drawing.Point(3, 138);
            this.lvPLaylist.MultiSelect = false;
            this.lvPLaylist.Name = "lvPLaylist";
            this.lvPLaylist.Size = new System.Drawing.Size(722, 321);
            this.lvPLaylist.TabIndex = 24;
            this.lvPLaylist.UseCompatibleStateImageBehavior = false;
            this.lvPLaylist.View = System.Windows.Forms.View.Details;
            this.lvPLaylist.DragDrop += new System.Windows.Forms.DragEventHandler(this.playlist_DragDrop);
            this.lvPLaylist.DragEnter += new System.Windows.Forms.DragEventHandler(this.playlist_DragEnter);
            this.lvPLaylist.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPLaylist_MouseDoubleClick);
            this.lvPLaylist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvPLaylist_MouseDown);
            this.lvPLaylist.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvPLaylist_MouseMove);
            this.lvPLaylist.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvPLaylist_MouseUp);
            // 
            // chkIsAutoSave
            // 
            this.chkIsAutoSave.AutoSize = true;
            this.chkIsAutoSave.Checked = true;
            this.chkIsAutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsAutoSave.Location = new System.Drawing.Point(281, 27);
            this.chkIsAutoSave.Name = "chkIsAutoSave";
            this.chkIsAutoSave.Size = new System.Drawing.Size(76, 17);
            this.chkIsAutoSave.TabIndex = 25;
            this.chkIsAutoSave.Text = "Auto Save";
            this.chkIsAutoSave.UseVisualStyleBackColor = true;
            this.chkIsAutoSave.CheckedChanged += new System.EventHandler(this.chkAutoSave_CheckedChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(360, 27);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 26;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(360, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pbxVolume
            // 
            this.pbxVolume.Location = new System.Drawing.Point(468, 27);
            this.pbxVolume.Name = "pbxVolume";
            this.pbxVolume.Size = new System.Drawing.Size(16, 16);
            this.pbxVolume.TabIndex = 28;
            this.pbxVolume.TabStop = false;
            this.pbxVolume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxVolume_MouseDown);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 461);
            this.Controls.Add(this.pbxVolume);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.chkIsAutoSave);
            this.Controls.Add(this.lvPLaylist);
            this.Controls.Add(this.lblTotalSongs);
            this.Controls.Add(this.lblShuffleSongsLeft);
            this.Controls.Add(this.tkbVolume);
            this.Controls.Add(this.chkShuffle);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.seekBar);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lblCurrentSongLength);
            this.Controls.Add(this.lblCurrentlyPlaying);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnOpen);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.seekBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblCurrentlyPlaying;
        private System.Windows.Forms.Label lblCurrentSongLength;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TrackBar seekBar;
        private System.Windows.Forms.Timer seekerUpdate;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.CheckBox chkShuffle;
        private System.Windows.Forms.TrackBar tkbVolume;
        private System.Windows.Forms.Label lblShuffleSongsLeft;
        private System.Windows.Forms.Label lblTotalSongs;
        private System.Windows.Forms.ListView lvPLaylist;
        private System.Windows.Forms.CheckBox chkIsAutoSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pbxVolume;
    }
}

