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
using Serilog;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Myriadbits.MXFInspect
{
    public abstract class TreeListViewBase<T> : TreeListView where T : Node<T>
    {
        protected int maxDigitCount = 0;

        public bool ShowOffsetAsHex { get; protected set; }

        private OLVColumn ColumnFirst { get; set; } = new OLVColumn();
        public OLVColumn ColumnOffset { get; set; } = new OLVColumn();
        public OLVColumn ColumnMXFObject { get; set; } = new OLVColumn();

        protected TreeListViewBase() : base()
        {
            //SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            // since after the migration to .NET5 a click on the 
            // first column triggers an exception, we make a fake column 
            // with its width equal to 0 (hack)
            // same hack is needed in report tree list
            this.ColumnFirst.MaximumWidth = 0;
            this.ColumnFirst.Width = 0;
            this.ColumnFirst.Text = "Index";

            this.AllColumns.Add(ColumnFirst);
            this.AllColumns.Add(ColumnOffset);
            this.AllColumns.Add(ColumnMXFObject);

            this.ColumnFirst.Hideable = false;
            this.ColumnOffset.Hideable = false;
            this.ColumnOffset.TextAlign = HorizontalAlignment.Right;
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

		[SupportedOSPlatform("windows")]
		protected void SetupTreeColumnRenderer()
		{
			Pen pen = new Pen(Color.Black, 1.001f);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			this.TreeColumnRenderer.LinePen = pen;
		}

		public void FillTree(IEnumerable<T> objects)
        {
            // Clear tree and set objects
            this.Items.Clear();
            this.SetObjects(objects);
            this.CalculateOffsetMaxDigitCount();
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
            this.ColumnOffset.AspectToStringConverter = delegate (object x)
            {
                long offset;
                try
                {
                    offset = Convert.ToInt64(x);
                }
                catch (InvalidCastException ex)
                {

                    Log.ForContext(typeof(TreeListViewBase<T>)).Error($"Exception occured during conversion of offset: {@ex}", ex);
                    return "";
                }
                return ShowOffsetAsHex ? "0x" + offset.ToString("X"+ maxDigitCount) : $"{offset:N0}";
            };

            this.RebuildColumns();
            this.Refresh();
        }

        protected abstract void Tree_FormatCell(object sender, FormatCellEventArgs e);

        protected abstract void CalculateOffsetMaxDigitCount();

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


        protected static Color GetColor(MXFObject obj)
        {
            if (obj.IsPartition())
            {
                return Properties.Settings.Default.Color_Partition;
            }
            else if (obj.IsFiller())
            {
                // filler is a subytpe of metadatabaseclass and therefore must be handled first
                return Properties.Settings.Default.Color_Filler;
            }
            else if (obj.IsEssenceElement())
            {
                return Properties.Settings.Default.Color_Essence;
            }
            else if (obj.IsIndexLike() || obj.IsIndexCollection())
            {
                return Properties.Settings.Default.Color_IndexTable;
            }
            else if (obj.IsSystemItem())
            {
                return Properties.Settings.Default.Color_SystemItem;
            }
            else if (obj.IsRIPOrRIPEntry())
            {
                return Properties.Settings.Default.Color_RIP;
            }
            else if (obj.IsMetadataLike())
            {
                return Properties.Settings.Default.Color_MetaData;
            }
            else if (false)
            {
                //case MXFObjectType.Special:
                //    return Properties.Settings.Default.Color_Special;
            }
            else
            {
                return Color.FromArgb(0, 0, 0);
            };
        }

        #endregion

    }
}
