﻿#region license
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
using System.Runtime.Versioning;
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
            this.ColumnOffset.Width = 85;
            this.ColumnOffset.MinimumWidth = 50;
            this.ColumnOffset.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.ColumnOffset.Renderer = null;
            // 
            // olvColumn2
            // 
            this.ColumnMXFObject.AspectName = "ToString";
            this.ColumnMXFObject.FillsFreeSpace = true;
            this.ColumnMXFObject.Hyperlink = true;
            this.ColumnMXFObject.Text = "Object";
            this.ColumnMXFObject.Width = 276;
            this.ColumnMXFObject.Renderer = TreeColumnRenderer;

            if (OperatingSystem.IsWindows())
            {
                base.SetupTreeColumnRenderer();
            }

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
                e.SubItem.ForeColor = GetColor(obj.Object);
            }

        }

        protected override void CalculateOffsetMaxDigitCount()
        {
            var logicalRoot = this.Objects?.OfType<MXFLogicalObject>()?.FirstOrDefault()?.Root();
            if (logicalRoot != null)
            {
                var descendants = logicalRoot.Descendants();
                if (descendants.Any())
                {
                    long maxOffset = descendants.Max(o => o.Object.Offset);
                    maxDigitCount = Helper.GetDigitCount(maxOffset);
                }
                maxDigitCount = 0;
            }
            maxDigitCount = 0;
        }
    }
}
