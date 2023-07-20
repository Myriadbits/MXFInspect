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
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public class PhysicalTreeListView : TreeListViewBase<MXFObject>
    {
        public bool FillersHidden { get; private set; } = true;

        public bool FilteredByType { get; private set; } = false;

        public PhysicalTreeListView() : base()
        {
            SetupColumns();

            SetHyperLinkStyle(Properties.Settings.Default.Color_Reference);
            //// Set tree delegates / event handlers
            this.HyperlinkClicked += Tree_HyperlinkClicked;
            this.IsHyperlink += Tree_IsHyperlink;

        }

        public void HideFillers(bool hide)
        {
            // save object so it can be reselected, once the filter has been applied
            var obj = this.SelectedObject;
            this.FillersHidden = hide;

            if (!FilteredByType)
            {
                this.ModelFilter = hide ? new ExcludeFillerFilter() : null;
            }

            this.SelectObject(obj);
            this.EnsureModelVisible(obj);
        }

        public void SetTypeFilter(bool filtered)
        {
            var selObj = this.SelectedObject as MXFObject;
            FilteredByType = filtered;

            if (filtered)
            {
                if (selObj != null)
                {
                    this.ModelFilter = new TypeFilter(selObj.GetType(), false);
                }
            }
            else
            {
                this.ModelFilter = this.FillersHidden ? new ExcludeFillerFilter() : null;

            }

            this.SelectObject(selObj);
            this.EnsureModelVisible(selObj);
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
                MXFObject obj = e.Model as MXFObject;
                if (OperatingSystem.IsWindows())
                {
                    SetFontFormat(e, obj);
                }
                e.SubItem.ForeColor = GetColor(obj);
            }
        }

        public void SetHyperLinkStyle(Color color)
        {
            // setup hyperlink style
            this.HyperlinkStyle = new HyperlinkStyle
            {
                Normal = new CellStyle
                {
                    ForeColor = color
                },
                Visited = new CellStyle
                {
                    ForeColor = color
                },
            };
        }

        #region private methods

        [SupportedOSPlatform("windows")]
		private void SetFontFormat(FormatCellEventArgs e, MXFObject obj)
        {
            switch (obj)
            {
                case ILazyLoadable l when !l.IsLoaded:
                    e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
                    break;
                case MXFUnparseablePack:
                    e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
                    break;
                default:
                    e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Regular);
                    break;
            }
        }

        protected override void CalculateOffsetMaxDigitCount()
        {
            long maxOffset = this.Objects?.OfType<MXFObject>()?.FirstOrDefault()?.GetTreeMaxOffset() ?? 0;
            maxDigitCount = Helper.GetDigitCount(maxOffset);
        }

        private void SetupColumns()
        {
            // Set the column styles
            // 
            // Offset column
            // 
            this.ColumnOffset.AspectName = "Offset";
            this.ColumnOffset.Text = "Offset";
            this.ColumnOffset.Width = 85;
            this.ColumnOffset.MinimumWidth = 50;
            this.ColumnOffset.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.ColumnOffset.Renderer = null;
            // 
            // MXFObject Column
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
		}

        private void Tree_IsHyperlink(object sender, IsHyperlinkEventArgs e)
        {
            if (e.Model is IResolvable resolvable && resolvable.GetReference() != null)
            {
                e.IsHyperlink = true;
            }
            else e.IsHyperlink = false;
        }

        private void Tree_HyperlinkClicked(object sender, HyperlinkClickedEventArgs e)
        {
            var resolvable = e.Model as IResolvable;
            this.RevealAndSelectObject(resolvable.GetReference());
        }

        #endregion
    }
}
