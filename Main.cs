using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MXFInspect
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initialize the UI
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Main_Load(object sender, EventArgs e)
		{
			this.txtPath.Text = Properties.Settings.Default.Filename;
		}

		/// <summary>
		/// Select a file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "MXF files (.mxf)|*.mxf|All Files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;
			openFileDialog.Multiselect = false;
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				// Set the filename
				txtPath.Text = openFileDialog.FileName;
					
				Properties.Settings.Default.Filename = this.txtPath.Text;
				Properties.Settings.Default.Save(); // Store last filename
			}
		}

		/// <summary>
		/// Process the MXF file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnProcess_Click(object sender, EventArgs e)
		{
			// Open the selected file to read.
			try
			{
				MXFFile file = new MXFFile(txtPath.Text);

				// Reset whole tree
				this.treeMain.Nodes.Clear();

				// Add main node
				TreeNode mainNode = this.treeMain.Nodes.Add(file.Filename);
				mainNode.Tag = file;

				// And all sub items
				foreach (MXFKLV subKLV in file.GlobalKLVs)
					AddNode(mainNode, subKLV);

				// Loop add all partitions
				TreeNode partitionsNode = mainNode.Nodes.Add(string.Format("{0} Partitions",  file.Partitions.Count));
				foreach (MXFPartition part in file.Partitions)
				{
					// And all sub items
					TreeNode partNode = AddNode(partitionsNode, part);
					if (partNode != null)
					{
						foreach (MXFKLV subKLV in part.KLVs)
							AddNode(partNode, subKLV);
					}
				}
				partitionsNode.Expand();

				if (file.RIP != null)
					AddNode(mainNode, file.RIP);

				mainNode.Expand();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error while opening the file");
			}
		}


		private TreeNode AddNode(TreeNode parentNode, object childObject)
		{
			if (this.chkHideFillers.Checked)
			{ 
				MXFKLV klv = childObject as MXFKLV;
				if (klv != null)
				{
					if (klv.Key.Type == KeyType.Filler)
						return null; // Do not add
				}
			}
			
			TreeNode node = parentNode.Nodes.Add(childObject.ToString());
			node.Tag = childObject;
			return node;
		}



		private void treeMain_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode selectedNode = this.treeMain.SelectedNode;
			if (selectedNode != null)
			{
				this.propGrid.SelectedObject = selectedNode.Tag;
			}
		}

	}
}
