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

using BrightIdeasSoftware;
using Myriadbits.MXF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public class LogicalTreeListView : TreeListViewBase<MXFLogicalObject>
    {
        public LogicalTreeListView() : base()
        {
            SetupColumns();
        }

        private void SetupColumns()
        {
            // Set the column styles
            // 
            // olvColumn1
            // 
            this.ColumnOffset.AspectName = "Object.Offset";
            this.ColumnOffset.Text = "Offset";
            this.ColumnOffset.Width = 84;
            this.ColumnOffset.Renderer = null;
            // 
            // olvColumn2
            // 
            this.ColumnMXFObject.AspectName = "ToString";
            this.ColumnMXFObject.FillsFreeSpace = true;
            this.ColumnMXFObject.Hyperlink = true;
            this.ColumnMXFObject.Text = "Name";
            this.ColumnMXFObject.Width = 276;
            this.ColumnMXFObject.Renderer = TreeColumnRenderer;

            Pen pen = new Pen(Color.Black, 1.001f);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.TreeColumnRenderer.LinePen = pen;

            this.RebuildColumns();
        }

        protected override void Tree_FormatCell(object sender, FormatCellEventArgs e)
        {

            if (e.Column == ColumnOffset)
            {
                // Physical Address/Offset
                e.SubItem.ForeColor = Color.Gray;
            }
            else if (e.Column == ColumnMXFObject)
            {
                MXFLogicalObject obj = e.Model as MXFLogicalObject;

                switch (obj?.Object?.Type)
                {
                    case MXFObjectType.Partition:
                        e.SubItem.ForeColor = Properties.Settings.Default.Color_Partition;
                        break;
                    case MXFObjectType.Essence:
                        e.SubItem.ForeColor = Properties.Settings.Default.Color_Essence;
                        break;
                    case MXFObjectType.Index:
                        e.SubItem.ForeColor = Properties.Settings.Default.Color_IndexTable;
                        break;
                    case MXFObjectType.SystemItem:
                        e.SubItem.ForeColor = Properties.Settings.Default.Color_SystemItem;
                        break;
                    case MXFObjectType.RIP:
                        e.SubItem.ForeColor = Properties.Settings.Default.Color_RIP;
                        break;
                    case MXFObjectType.Meta:
                        e.SubItem.ForeColor = Properties.Settings.Default.Color_MetaData;
                        break;
                    case MXFObjectType.Filler:
                        e.SubItem.ForeColor = Properties.Settings.Default.Color_Filler;
                        break;
                    case MXFObjectType.Special:
                        e.SubItem.ForeColor = Properties.Settings.Default.Color_Special;
                        break;
                }
            }

        }
    }
}
