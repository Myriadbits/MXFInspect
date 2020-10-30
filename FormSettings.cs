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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
	public partial class FormSettings : Form
	{
		public FormSettings()
		{
			InitializeComponent();
		}

		private void FormSettings_Load(object sender, EventArgs e)
		{
			LoadSettings();
		}

		private void LoadSettings()
		{
			Settings settings = MXFInspect.Properties.Settings.Default;
			if (settings.PartialLoadThresholdMB == 100) this.cmbThreshold.SelectedIndex = 1;
			else if (settings.PartialLoadThresholdMB == 500) this.cmbThreshold.SelectedIndex = 2;
			else if (settings.PartialLoadThresholdMB == 1000) this.cmbThreshold.SelectedIndex = 3;
			else if (settings.PartialLoadThresholdMB == 5000) this.cmbThreshold.SelectedIndex = 4;
			else this.cmbThreshold.SelectedIndex = 0;

			this.pbColorPartition.BackColor = settings.Color_Partition;
			this.pbColorEssence.BackColor = settings.Color_Essence;
			this.pbColorIndex.BackColor = settings.Color_IndexTable;
			this.pbColorRIP.BackColor = settings.Color_RIP;
			this.pbColorSystemItem.BackColor = settings.Color_SystemItem;
			this.pbColorMeta.BackColor = settings.Color_MetaData;
			this.pbColorFiller.BackColor = settings.Color_Filler;
			this.pbColorSpecial.BackColor = settings.Color_Special;

			this.chkPartialLoadMsg.Checked = settings.PartialLoadWarning;
		}

		private void ChangeColor(PictureBox pb)
		{
			this.colorDialog1.Color = pb.BackColor;
			if (this.colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				pb.BackColor = this.colorDialog1.Color;
			}
		}

		private void bntOk_Click(object sender, EventArgs e)
		{
			Settings settings = MXFInspect.Properties.Settings.Default;
			
			settings.Color_Partition = this.pbColorPartition.BackColor;
			settings.Color_Essence = this.pbColorEssence.BackColor;
			settings.Color_IndexTable = this.pbColorIndex.BackColor;
			settings.Color_RIP = this.pbColorRIP.BackColor;
			settings.Color_SystemItem = this.pbColorSystemItem.BackColor;
			settings.Color_MetaData = this.pbColorMeta.BackColor;
			settings.Color_Filler = this.pbColorFiller.BackColor;
			settings.Color_Special = this.pbColorSpecial.BackColor;

			if (this.cmbThreshold.SelectedIndex == 1) settings.PartialLoadThresholdMB = 100;
			else if (this.cmbThreshold.SelectedIndex == 2) settings.PartialLoadThresholdMB = 500;
			else if (this.cmbThreshold.SelectedIndex == 3) settings.PartialLoadThresholdMB = 1000;
			else if (this.cmbThreshold.SelectedIndex == 4) settings.PartialLoadThresholdMB = 5000;
			else settings.PartialLoadThresholdMB = 0;

			settings.PartialLoadWarning = this.chkPartialLoadMsg.Checked;

			MXFInspect.Properties.Settings.Default.Save();
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void pbColorIndex_Click(object sender, EventArgs e) { ChangeColor(this.pbColorIndex); }
		private void pbColorEssence_Click(object sender, EventArgs e) { ChangeColor(this.pbColorEssence); }
		private void pbColorPartition_Click(object sender, EventArgs e) { ChangeColor(this.pbColorPartition); }
		private void pbColorRIP_Click(object sender, EventArgs e) { ChangeColor(this.pbColorRIP); }
		private void pbColorSystemItem_Click(object sender, EventArgs e) { ChangeColor(this.pbColorSystemItem); }
		private void pbColorMeta_Click(object sender, EventArgs e){ ChangeColor(this.pbColorMeta); }
		private void pbColorFiller_Click(object sender, EventArgs e){ ChangeColor(this.pbColorFiller); }
		private void pbColorSpecial_Click(object sender, EventArgs e){ ChangeColor(this.pbColorSpecial); }
	
		private void btnReset_Click(object sender, EventArgs e)
		{
			MXFInspect.Properties.Settings.Default.Reset();
			LoadSettings();
		}

		
		
	}
}
