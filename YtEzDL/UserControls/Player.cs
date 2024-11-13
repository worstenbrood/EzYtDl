using System;
using System.Threading.Tasks;
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
            this._toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _metroTrackBar
            // 
            this._metroTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._metroTrackBar.BackColor = System.Drawing.SystemColors.MenuBar;
            this._metroTrackBar.Location = new System.Drawing.Point(3, 25);
            this._metroTrackBar.Name = "_metroTrackBar";
            this._metroTrackBar.Size = new System.Drawing.Size(552, 19);
            this._metroTrackBar.TabIndex = 0;
            this._metroTrackBar.Text = "Player";
            this._metroTrackBar.Value = 0;
            // 
            // toolStrip
            // 
            this._toolStrip.AutoSize = false;
            this._toolStrip.BackColor = System.Drawing.Color.White;
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripButtonPlay,
            this._toolStripButtonPause,
            this._toolStripLabel});
            this._toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(558, 22);
            this._toolStrip.Stretch = true;
            this._toolStrip.TabIndex = 3;
            // 
            // toolStripButtonPlay
            // 
            this._toolStripButtonPlay.AutoSize = false;
            this._toolStripButtonPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonPlay.Image = global::YtEzDL.Properties.Resources.Play;
            this._toolStripButtonPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonPlay.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._toolStripButtonPlay.Name = "_toolStripButtonPlay";
            this._toolStripButtonPlay.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._toolStripButtonPlay.Size = new System.Drawing.Size(22, 20);
            this._toolStripButtonPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._toolStripButtonPlay.Click += new System.EventHandler(this.toolStripButtonPlay_Click);
            // 
            // toolStripButtonPause
            // 
            this._toolStripButtonPause.AutoSize = false;
            this._toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonPause.Image = global::YtEzDL.Properties.Resources.Pause;
            this._toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonPause.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this._toolStripButtonPause.Name = "_toolStripButtonPause";
            this._toolStripButtonPause.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._toolStripButtonPause.Size = new System.Drawing.Size(22, 20);
            this._toolStripButtonPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // toolStripLabel
            // 
            this._toolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._toolStripLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.Size = new System.Drawing.Size(558, 47);
            this.UseCustomBackColor = true;
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        private readonly object _lock = new object();
        private MetroTrackBar _metroTrackBar;
        private TrackData _currentTrack;
        private ToolStrip _toolStrip;
        private ToolStripButton _toolStripButtonPlay;
        private ToolStripButton _toolStripButtonPause;
        private ToolStripLabel _toolStripLabel;
        private readonly Timer _timer = new Timer();

        private void Execute(Action action)
        {
            Invoke(new MethodInvoker(action.Invoke));
        }

        private void ExecuteAsync(Action action)
        {
            BeginInvoke(new MethodInvoker(action.Invoke));
        }

        private void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            ExecuteAsync(() =>
            {
                _metroTrackBar.Value = 0;
            });
        }

        public Player()
        {
            InitializeComponent();

            _timer.Interval = 1000;
            _timer.Tick += (o, a) => Task.Run(() => TimerTick(o, a));
            
            AudioPlayer.Instance.PlaybackStopped += PlaybackStopped;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ExecuteAsync(() => _metroTrackBar.Value += 1);
        }

        private void Toggle()
        {
            switch (AudioPlayer.Instance.PlaybackState)
            {
                case PlaybackState.Playing:
                    _timer.Enabled = true;
                    _toolStripButtonPlay.Enabled = false;
                    _toolStripButtonPause.Enabled = true;
                    break;

                case PlaybackState.Paused:
                case PlaybackState.Stopped:
                    _timer.Enabled = false;
                    _toolStripButtonPlay.Enabled = true;
                    _toolStripButtonPause.Enabled = false;
                    break;

            }
        }

        public void Play(TrackData trackData)
        {
            lock (_lock)
            {
                _currentTrack = trackData;
                AudioPlayer.Instance.Play(_currentTrack.WebpageUrl);
                ExecuteAsync(() =>
                {
                    _metroTrackBar.Maximum = (int)_currentTrack.Duration;
                    _toolStripLabel.Text = $"{_currentTrack.Title} ({TimeSpan.FromSeconds(_currentTrack.Duration):h\\:mm\\:ss})";
                    Toggle();
                });
            }
        }

        public void Play(TimeSpan position)
        {
            lock (_lock)
            {
                AudioPlayer.Instance.Play(position);
                ExecuteAsync(Toggle);
            }
        }

        public void Pause()
        {
            lock (_lock)
            {
                AudioPlayer.Instance.Pause();
                ExecuteAsync(Toggle);
            }
        }

        public new void Dispose()
        {
            lock (_lock)
            {
                AudioPlayer.Instance.Stop();
            }
            _timer.Dispose();
            base.Dispose();
        }
        
        private void toolStripButtonPlay_Click(object sender, EventArgs e)
        {
            switch (AudioPlayer.Instance.PlaybackState)
            {
                case PlaybackState.Paused:
                    AudioPlayer.Instance.Resume();
                    break;

                case PlaybackState.Stopped:
                    AudioPlayer.Instance.Play();
                    break;
            }

            ExecuteAsync(Toggle);
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            if (AudioPlayer.Instance.PlaybackState == PlaybackState.Playing)
            {
                AudioPlayer.Instance.Pause();
                ExecuteAsync(Toggle);
            }
        }
    }
}
