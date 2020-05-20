using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Reflection;

namespace DNNConnect.DNNResxCompare
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			if (!string.IsNullOrEmpty(Properties.Settings.Default.OldFolder))
				txtFolderOld.Text = Properties.Settings.Default.OldFolder;
			else
				txtFolderOld.Text = Application.StartupPath;

			if (!string.IsNullOrEmpty(Properties.Settings.Default.NewFolder))
				txtFolderNew.Text = Properties.Settings.Default.NewFolder;
			else
				txtFolderNew.Text = Application.StartupPath;

			if (!string.IsNullOrEmpty(Properties.Settings.Default.SaveFolder))
			{
				txtSaveAs.Text = Properties.Settings.Default.SaveFolder;
			}
		}

		private void btBrowseOld_Click(object sender, EventArgs e)
		{
			oldDialog.SelectedPath = txtFolderOld.Text;
			if (oldDialog.ShowDialog() == DialogResult.OK)
			{
				txtFolderOld.Text = oldDialog.SelectedPath;
			}
		}

		private void btBrowseNew_Click(object sender, EventArgs e)
		{
			newDialog.SelectedPath = txtFolderNew.Text;
			if (newDialog.ShowDialog() == DialogResult.OK)
			{
				txtFolderNew.Text = newDialog.SelectedPath;
			}
		}

		private void btSaveAs_Click(object sender, EventArgs e)
		{
			saveDialog.InitialDirectory = txtSaveAs.Text;
			if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				txtSaveAs.Text = saveDialog.FileName;
			}
		}

		private void btGo_Click(object sender, EventArgs e)
		{
			try
			{
				Comparer c = new Comparer();
				Cursor.Current = Cursors.WaitCursor;
				c.Compare(txtFolderOld.Text, txtFolderNew.Text, txtSaveAs.Text);
				Cursor.Current = Cursors.Default;
				MessageBox.Show("Done!", "Resource Compare", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtFolderNew.Text))
			{
				Properties.Settings.Default.NewFolder = txtFolderNew.Text;
			}
			if (!string.IsNullOrEmpty(txtFolderOld.Text))
			{
				Properties.Settings.Default.OldFolder = txtFolderOld.Text;
			}
			if (!string.IsNullOrEmpty(txtSaveAs.Text))
			{
				Properties.Settings.Default.SaveFolder = txtSaveAs.Text;
			}

			Properties.Settings.Default.Save();
		}
	}
}
