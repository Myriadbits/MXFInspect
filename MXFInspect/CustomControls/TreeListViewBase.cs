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
    public abstract class TreeListViewBase<T> : TreeListView where T : Node<T> 
    {
        public bool ShowOffsetAsHex { get; protected set; }

        private OLVColumn ColumnFirst { get; set; } = new OLVColumn();
        public OLVColumn ColumnOffset { get; set; } = new OLVColumn();
        public OLVColumn ColumnMXFObject { get; set; } = new OLVColumn();

        protected TreeListViewBase() : base()
        {
            SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            // since after the migration to .NET5 a click on the 
            // first column triggers an exception, we make a fake column 
            // with its width equal to 0 (hack)
            // TODO same hack is needed in report tree list
            this.ColumnFirst.Text = "Index";
            this.ColumnFirst.MaximumWidth = 0;
            this.ColumnFirst.Width = 0;

            this.AllColumns.Add(ColumnFirst);
            this.AllColumns.Add(ColumnOffset);
            this.AllColumns.Add(ColumnMXFObject);

            this.ColumnFirst.Hideable = false;
            this.ColumnOffset.Hideable = false;
            this.ColumnMXFObject.Hideable = false;

            //this.Columns.AddRange(new ColumnHeader[] { ColumnFirst, ColumnOffset, ColumnMXFObject });

            // Set tree delegates
            this.CanExpandGetter = TreeNode_HasChildren;
            this.ChildrenGetter = TreeNode_ChildGetter;
            this.ParentGetter = TreeNode_ParentGetter;

            // Set Event Handlers
            this.Expanding += Tree_Expanding;
            this.FormatCell += Tree_FormatCell;
        }

        public void FillTree(IEnumerable<object> objects)
        {
            // Clear tree and set objects
            this.Items.Clear();
            this.SetObjects(objects);
        }

        public void RevealAndSelectObject(T objToSelect)
        {
            if (objToSelect != null)
            {
                // Expand entire parent tree and select object
                this.Reveal(objToSelect, true);
                this.EnsureModelVisible(objToSelect);
            }
        }

        public void SetOffsetStyle(bool showOffsetAsHex)
        {
            ShowOffsetAsHex = showOffsetAsHex;
            this.ColumnOffset.AspectToStringFormat = ShowOffsetAsHex ? "0x{0:X}" : "";
            this.RebuildColumns();
            this.Refresh();
        }

        protected abstract void Tree_FormatCell(object sender, FormatCellEventArgs e);

        #region private methods

        private bool TreeNode_HasChildren(object x)
        {
            if (x is T obj)
            {
                return obj.Children.Any();
            }
            return false;
        }

        private IEnumerable TreeNode_ChildGetter(object x)
        {
            if (x is T obj)
            {
                return obj.Children;
            }
            return Enumerable.Empty<object>();
        }

        private object TreeNode_ParentGetter(object model)
        {
            if (model is T obj)
            {
                return obj.Parent;
            }
            return null;
        }

        private void Tree_Expanding(object sender, TreeBranchExpandingEventArgs e)
        {
            T selObject = e.Model as T;
            if (selObject is ILazyLoadable loadable)
            {
                Cursor.Current = Cursors.WaitCursor;
                loadable.Load();
                Cursor.Current = Cursors.Default;
            }
        }


        #endregion

    }
}
