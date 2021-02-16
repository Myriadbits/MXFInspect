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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

            // setup hyperlink style
            this.HyperlinkStyle = new HyperlinkStyle
            {
                Normal = new CellStyle
                {
                    ForeColor = DefaultForeColor
                },
                Visited = new CellStyle
                {
                    ForeColor = DefaultForeColor
                },
            };

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
        
        // TODO should this method really remain here? move it to the extensions class?
        // TODO what if there are no partitions at all?
        public MXFObject GetFirstPartition()
        {
            var mxfObjects = this.Objects.OfType<MXFObject>();
            return mxfObjects
                        .FirstOrDefault()?
                        .Root()
                        .Descendants()
                        .OfType<MXFPartition>()
                        .OrderBy(p => p.Offset)
                        .FirstOrDefault();

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

                if (obj is ILazyLoadable loadable && !loadable.IsLoaded)
                {
                    e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Italic);
                }
                else
                {
                    e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Regular);
                }

                switch (obj.Type)
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

        #region private methods

        private void SetupColumns()
        {
            // Set the column styles
            // 
            // Offset column
            // 
            this.ColumnOffset.AspectName = "Offset";
            this.ColumnOffset.Text = "Offset";
            this.ColumnOffset.Width = 84;
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

            Pen pen = new Pen(Color.Black, 1.001f);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.TreeColumnRenderer.LinePen = pen;
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
