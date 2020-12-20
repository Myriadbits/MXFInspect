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
    public class LogicalTreeListView : TreeListView
    {
        public bool FillersHidden { get; private set; } = true;

        public bool FilteredByType { get; private set; } = false;

        public OLVColumn ColumnOffset { get; set; } = new OLVColumn();
        public OLVColumn ColumnMXFLogicalObject { get; set; } = new OLVColumn();

        public LogicalTreeListView() : base()
        {
            SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            SetupColumns();

            // Set tree delegates
            this.CanExpandGetter = TreeNode_HasChildren;
            this.ChildrenGetter = TreeNode_ChildGetter;
            this.ParentGetter = TreeNode_ParentGetter;

            // Set Event Handlers
            this.FormatCell += Tree_FormatCell;
            this.Expanding += Tree_Expanding;
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

        public void FillTree(IEnumerable<object> objects)
        {
            // Clear tree and set objects
            this.Items.Clear();
            this.SetObjects(objects);
        }

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

        public void RevealAndSelectObject(MXFLogicalObject objToSelect)
        {
            //if(this.Objects.OfType<MXF>)
            //var logicalObj = this.Objects.OfType<MXFLogicalObject>().FirstOrDefault(o => o.Object == objToSelect);
            //this.Objects.
            if (objToSelect != null)
            {
                // Open entire parent tree
                // Open entire parent tree and select object
                this.Reveal(objToSelect, true);

                this.EnsureModelVisible(objToSelect);
                this.RefreshObject(objToSelect);
            }
        }

        #region private methods

        private void SetupColumns()
        {
            this.AllColumns.Add(ColumnOffset);
            this.AllColumns.Add(ColumnMXFLogicalObject);

            this.Columns.AddRange(new ColumnHeader[] { ColumnOffset, ColumnMXFLogicalObject });

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
            this.ColumnMXFLogicalObject.AspectName = "ToString";
            this.ColumnMXFLogicalObject.FillsFreeSpace = true;
            this.ColumnMXFLogicalObject.Hyperlink = true;
            this.ColumnMXFLogicalObject.Text = "Name";
            this.ColumnMXFLogicalObject.Width = 276;
            this.ColumnMXFLogicalObject.Renderer = TreeColumnRenderer;

            Pen pen = new Pen(Color.Black, 1.001f);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.TreeColumnRenderer.LinePen = pen;
        }

        private bool TreeNode_HasChildren(object x)
        {
            if (x is MXFLogicalObject obj)
            {
                return obj.Children.Any();
            }
            return false;
        }

        private IEnumerable TreeNode_ChildGetter(object x)
        {
            if (x is MXFLogicalObject obj)
            {
                return obj.Children;
            }
            return null;
        }

        private object TreeNode_ParentGetter(object model)
        {
            if (model is MXFLogicalObject obj)
            {
                return obj.Parent;
            }
            return null;
        }

        private void Tree_Expanding(object sender, TreeBranchExpandingEventArgs e)
        {
            MXFObject selObject = e.Model as MXFObject;
            if (selObject != null && !selObject.IsLoaded)
            {
                Cursor.Current = Cursors.WaitCursor;
                selObject.Load();
                Cursor.Current = Cursors.Default;
            }
        }

        private void Tree_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.Column == ColumnOffset)
            {
                // Physical Address/Offset
                e.SubItem.ForeColor = Color.Gray;
            }
            else if (e.Column == ColumnMXFLogicalObject)
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

        #endregion
    }
}
