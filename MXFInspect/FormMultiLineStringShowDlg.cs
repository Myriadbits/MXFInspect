#region license
//
// MXFInspect - Myriadbits MXF Viewer. 
// Inspect MXF Files.
// Copyright (C) 2015 Myriadbits, Jochem Bakker
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// For more information, contact me at: info@myriadbits.com
//
#endregion

using Myriadbits.MXFInspect.Properties;
using System;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public partial class FormMultiLineStringShowDlg : Form
    {
        public string MultiLineText
        {
            get
            {
                return this.textBox1.Text;
            }
            set { this.textBox1.Text = value; }
        }

        public FormMultiLineStringShowDlg(string text)
        {
            InitializeComponent();
            MultiLineText = text;
        }
    }
}
