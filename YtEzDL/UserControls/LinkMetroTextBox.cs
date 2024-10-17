using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace YtEzDL.UserControls
{
    internal class LinkMetroTextBox : MetroTextBox
    {
        private LinkLabel _linkLabel;

        public LinkMetroTextBox()
        {
            _linkLabel = new LinkLabel();

            this.Controls.Add(_linkLabel);

            _linkLabel.AutoSize = true;
            _linkLabel.Left = -1;
            _linkLabel.Top = 1;
            _linkLabel.Visible = true;
            _linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(ll_LinkClicked);
        }

        private void ll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Debug.WriteLine(e.Link);
        }
    }
}
