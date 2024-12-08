using System;
using System.Windows.Forms;
using MetroFramework.Controls;
using NAudio.Wave;
using YtEzDL.Audio;
using YtEzDL.DownLoad;

namespace YtEzDL.UserControls
{
    public class Player : MetroUserControl
    {
        private void InitializeComponent()
        {
            this._metroTrackBar = new MetroFramework.Controls.MetroTrackBar();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._toolStripButtonPlay = new System.Windows.Forms.ToolStripButton();
            this._toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this._toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this._toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _metroTrackBar
            // 
            this._metroTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._metroTrackBar.BackColor = System.Drawing.SystemColors.MenuBar;
            this._metroTrackBar.Enabled = false;
            this._metroTrackBar.Location = new System.Drawing.Point(3, 26);
            this._metroTrackBar.Name = "_metroTrackBar";
            this._metroTrackBar.Size = new System.Drawing.Size(552, 25);
            this._metroTrackBar.TabIndex = 0;
            this._metroTrackBar.Text = "Player";
            this._metroTrackBar.Value = 0;
            this._metroTrackBar.ValueChanged += new System.EventHandler(this.MetroTrackBarOnValueChanged);
            this._metroTrackBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MetroTrackBarOnScroll);
            // 
            // _toolStrip
            // 
            this._toolStrip.AutoSize = false;
            this._toolStrip.BackColor = System.Drawing.Color.White;
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripButtonPlay,
            this._toolStripButtonPause,
            this._toolStripButtonStop,
            this._toolStripLabel});
            this._toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(558, 26);
            this._toolStrip.Stretch = true;
            this._toolStrip.TabIndex = 3;
            // 
            // _toolStripButtonPlay
            // 
            this._toolStripButtonPlay.AutoSize = false;
            this._toolStripButtonPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonPlay.Image = global::YtEzDL.Properties.Resources.Play;
            this._toolStripButtonPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonPlay.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._toolStripButtonPlay.Name = "_toolStripButtonPlay";
            this._toolStripButtonPlay.Size = new System.Drawing.Size(24, 24);
            this._toolStripButtonPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._toolStripButtonPlay.ToolTipText = "Play";
            this._toolStripButtonPlay.Click += new System.EventHandler(this.toolStripButtonPlay_Click);
            // 
            // _toolStripButtonPause
            // 
            this._toolStripButtonPause.AutoSize = false;
            this._toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonPause.Image = global::YtEzDL.Properties.Resources.Pause;
            this._toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonPause.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._toolStripButtonPause.Name = "_toolStripButtonPause";
            this._toolStripButtonPause.Size = new System.Drawing.Size(24, 24);
            this._toolStripButtonPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._toolStripButtonPause.ToolTipText = "Pause";
            this._toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // _toolStripButtonStop
            // 
            this._toolStripButtonStop.AutoSize = false;
            this._toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonStop.Image = global::YtEzDL.Properties.Resources.Stop;
            this._toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonStop.Name = "_toolStripButtonStop";
            this._toolStripButtonStop.Size = new System.Drawing.Size(24, 24);
            this._toolStripButtonStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._toolStripButtonStop.ToolTipText = "Stop";
            this._toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // _toolStripLabel
            // 
            this._toolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._toolStripLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._toolStripLabel.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._toolStripLabel.Name = "_toolStripLabel";
            this._toolStripLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this._toolStripLabel.Size = new System.Drawing.Size(0, 0);
            // 
            // Player
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._metroTrackBar);
            this.Name = "Player";
            this.Size = new System.Drawing.Size(558, 55);
            this.UseCustomBackColor = true;
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        private void MetroTrackBarOnValueChanged(object sender, EventArgs e)
        {
            _toolTip.SetToolTip(_metroTrackBar, $"Time: {TimeSpan.FromSeconds(_metroTrackBar.Value):h\\:mm\\:ss}");
        }

        private void MetroTrackBarOnScroll(object sender, ScrollEventArgs e)
        {
            _toolTip.SetToolTip(_metroTrackBar, $"Time: {TimeSpan.FromSeconds(e.NewValue):h\\:mm\\:ss}");
        }

        private readonly object _lock = new object();
        private MetroTrackBar _metroTrackBar;
        private TrackData _currentTrack;
        private ToolStrip _toolStrip;
        private ToolStripButton _toolStripButtonPlay;
        private ToolStripButton _toolStripButtonPause;
        private ToolStripLabel _toolStripLabel;
        private readonly AudioPlayer _player;
        private ToolStripButton _toolStripButtonStop;
        private readonly ToolTip _toolTip = new ToolTip();

        protected override void OnLoad(EventArgs e)
        {
            Toggle();
            _toolStripButtonPlay.Enabled = false;
            base.OnLoad(e);
        }

        private void Execute(Action action)
        {
            Invoke(new MethodInvoker(action.Invoke));
        }

        private void ExecuteAsync(Action action)
        {
            BeginInvoke(new MethodInvoker(action.Invoke));
        }

        private double _bytesRead;

        private void PlayerStreamRead(object o, Streams.ReadEventArgs args)
        {
            _bytesRead += args.BytesRead;
            if (_bytesRead >= AudioPlayer.Format.AverageBytesPerSecond)
            {
                _bytesRead -= AudioPlayer.Format.AverageBytesPerSecond;
                Execute(() => _metroTrackBar.Value++);
            }
        }

        private void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            try
            {
                Execute(() =>
                {
                    _metroTrackBar.Value = 0;
                    _bytesRead = 0;
                    Toggle();
                });
            }
            catch (Exception)
            {
                //  Ignore
            }
        }

        public Player()
        {
            InitializeComponent();

            _player = new AudioPlayer();
            _player.PlaybackStopped += PlaybackStopped;
            _player.StreamRead += PlayerStreamRead;
            Toggle();
        }
        
        private void Toggle()
        {
            switch (_player.PlaybackState)
            {
                case PlaybackState.Playing:
                    _toolStripButtonPlay.Enabled = false;
                    _toolStripButtonPause.Enabled = true;
                    _toolStripButtonStop.Enabled = true;
                    break;

                case PlaybackState.Paused:
                    _toolStripButtonPlay.Enabled = true;
                    _toolStripButtonPause.Enabled = false;
                    _toolStripButtonStop.Enabled = true;
                    break;

                case PlaybackState.Stopped:
                    _toolStripButtonPlay.Enabled = true;
                    _toolStripButtonPause.Enabled = false;
                    _toolStripButtonStop.Enabled = false;
                    break;
            }
        }

        public void Play(TrackData trackData)
        {
            lock (_lock)
            {
                _currentTrack = trackData;
                _player.Play(_currentTrack.WebpageUrl);
                Execute(() =>
                {
                    _metroTrackBar.Value = 0;
                    _metroTrackBar.Maximum = (int)_currentTrack.Duration;
                    _toolStripLabel.Text = $"{_currentTrack.Title} ({TimeSpan.FromSeconds(_currentTrack.Duration):h\\:mm\\:ss})";
                    Toggle();
                });
            }
        }

        public void Play(TimeSpan position)
        {
            _player.Play(position);
            Execute(Toggle);
        }

        public void Pause()
        {
            _player.Pause();
            Execute(Toggle);
        }
        
        public new void Dispose()
        {
            lock (_lock)
            {
                _player.StreamRead -= PlayerStreamRead;
                _player.PlaybackStopped -= PlaybackStopped;
                _player.Dispose();
            }
            base.Dispose();
        }
        
        private void toolStripButtonPlay_Click(object sender, EventArgs e)
        {
            switch (_player.PlaybackState)
            {
                case PlaybackState.Paused:
                    _player.Resume();
                    break;

                case PlaybackState.Stopped:
                    _player.Play();
                    break;
            }

            ExecuteAsync(Toggle);
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            if (_player.PlaybackState == PlaybackState.Playing)
            {
                _player.Pause();
                Execute(Toggle);
            }
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            if (_player.PlaybackState == PlaybackState.Playing)
            {
                _player.Reset();
                // Toggle done by event
            }
        }
    }
}
