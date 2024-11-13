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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip.SuspendLayout();
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
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.White;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPlay,
            this.toolStripButtonPause,
            this.toolStripLabel});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(558, 22);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 3;
            // 
            // toolStripButtonPlay
            // 
            this.toolStripButtonPlay.AutoSize = false;
            this.toolStripButtonPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPlay.Image = global::YtEzDL.Properties.Resources.Play;
            this.toolStripButtonPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlay.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripButtonPlay.Name = "toolStripButtonPlay";
            this.toolStripButtonPlay.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.toolStripButtonPlay.Size = new System.Drawing.Size(22, 20);
            this.toolStripButtonPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonPlay.Click += new System.EventHandler(this.toolStripButtonPlay_Click);
            // 
            // toolStripButtonPause
            // 
            this.toolStripButtonPause.AutoSize = false;
            this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPause.Image = global::YtEzDL.Properties.Resources.Pause;
            this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPause.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.toolStripButtonPause.Name = "toolStripButtonPause";
            this.toolStripButtonPause.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.toolStripButtonPause.Size = new System.Drawing.Size(22, 20);
            this.toolStripButtonPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // toolStripLabel
            // 
            this.toolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel.Name = "toolStripLabel";
            this.toolStripLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.toolStripLabel.Size = new System.Drawing.Size(0, 0);
            // 
            // Player
            // 
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this._metroTrackBar);
            this.Name = "Player";
            this.Size = new System.Drawing.Size(558, 47);
            this.UseCustomBackColor = true;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        private readonly object _lock = new object();
        private MetroTrackBar _metroTrackBar;
        private TrackData _currentTrack;
        private ToolStrip toolStrip;
        private ToolStripButton toolStripButtonPlay;
        private ToolStripButton toolStripButtonPause;
        private ToolStripLabel toolStripLabel;
        private Timer _timer = new Timer();

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
            _metroTrackBar.Maximum = 300;
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
                    toolStripButtonPlay.Enabled = false;
                    toolStripButtonPause.Enabled = true;
                    break;

                case PlaybackState.Paused:
                case PlaybackState.Stopped:
                    _timer.Enabled = false;
                    toolStripButtonPlay.Enabled = true;
                    toolStripButtonPause.Enabled = false;
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
                    toolStripLabel.Text = _currentTrack.Title;
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
