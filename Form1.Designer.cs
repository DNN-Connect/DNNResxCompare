namespace DNNConnect.DNNResxCompare
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.oldDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.newDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btBrowseOld = new System.Windows.Forms.Button();
            this.btBrowseNew = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btSaveAs = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btGo = new System.Windows.Forms.Button();
            this.txtFolderOld = new System.Windows.Forms.TextBox();
            this.txtFolderNew = new System.Windows.Forms.TextBox();
            this.txtSaveAs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // oldDialog
            // 
            this.oldDialog.Description = "Folder Containing the Previous Version";
            this.oldDialog.ShowNewFolderButton = false;
            // 
            // newDialog
            // 
            this.newDialog.Description = "Folder Containing the New Version";
            this.newDialog.ShowNewFolderButton = false;
            // 
            // saveDialog
            // 
            this.saveDialog.DefaultExt = "xml";
            this.saveDialog.Filter = "XML|*.xml";
            this.saveDialog.Title = "Save Report As ...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Previous Version Folder:";
            // 
            // btBrowseOld
            // 
            this.btBrowseOld.Location = new System.Drawing.Point(415, 8);
            this.btBrowseOld.Name = "btBrowseOld";
            this.btBrowseOld.Size = new System.Drawing.Size(75, 23);
            this.btBrowseOld.TabIndex = 1;
            this.btBrowseOld.Text = "Browse";
            this.btBrowseOld.UseVisualStyleBackColor = true;
            this.btBrowseOld.Click += new System.EventHandler(this.btBrowseOld_Click);
            // 
            // btBrowseNew
            // 
            this.btBrowseNew.Location = new System.Drawing.Point(415, 37);
            this.btBrowseNew.Name = "btBrowseNew";
            this.btBrowseNew.Size = new System.Drawing.Size(75, 23);
            this.btBrowseNew.TabIndex = 3;
            this.btBrowseNew.Text = "Browse";
            this.btBrowseNew.UseVisualStyleBackColor = true;
            this.btBrowseNew.Click += new System.EventHandler(this.btBrowseNew_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "New Version Folder:";
            // 
            // btSaveAs
            // 
            this.btSaveAs.Location = new System.Drawing.Point(415, 80);
            this.btSaveAs.Name = "btSaveAs";
            this.btSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btSaveAs.TabIndex = 5;
            this.btSaveAs.Text = "Browse";
            this.btSaveAs.UseVisualStyleBackColor = true;
            this.btSaveAs.Click += new System.EventHandler(this.btSaveAs_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Save Report As:";
            // 
            // btGo
            // 
            this.btGo.Location = new System.Drawing.Point(140, 106);
            this.btGo.Name = "btGo";
            this.btGo.Size = new System.Drawing.Size(75, 23);
            this.btGo.TabIndex = 6;
            this.btGo.Text = "Go !";
            this.btGo.UseVisualStyleBackColor = true;
            this.btGo.Click += new System.EventHandler(this.btGo_Click);
            // 
            // txtFolderOld
            // 
            this.txtFolderOld.Location = new System.Drawing.Point(140, 8);
            this.txtFolderOld.Name = "txtFolderOld";
            this.txtFolderOld.Size = new System.Drawing.Size(269, 20);
            this.txtFolderOld.TabIndex = 7;
            // 
            // txtFolderNew
            // 
            this.txtFolderNew.Location = new System.Drawing.Point(140, 37);
            this.txtFolderNew.Name = "txtFolderNew";
            this.txtFolderNew.Size = new System.Drawing.Size(269, 20);
            this.txtFolderNew.TabIndex = 8;
            // 
            // txtSaveAs
            // 
            this.txtSaveAs.Location = new System.Drawing.Point(140, 80);
            this.txtSaveAs.Name = "txtSaveAs";
            this.txtSaveAs.Size = new System.Drawing.Size(269, 20);
            this.txtSaveAs.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Use /? for help on command line arguments";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 169);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSaveAs);
            this.Controls.Add(this.txtFolderNew);
            this.Controls.Add(this.txtFolderOld);
            this.Controls.Add(this.btGo);
            this.Controls.Add(this.btSaveAs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btBrowseNew);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btBrowseOld);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DotNetNuke Resource File Compare";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog oldDialog;
		private System.Windows.Forms.FolderBrowserDialog newDialog;
		private System.Windows.Forms.SaveFileDialog saveDialog;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btBrowseOld;
		private System.Windows.Forms.Button btBrowseNew;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btSaveAs;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btGo;
		private System.Windows.Forms.TextBox txtFolderOld;
		private System.Windows.Forms.TextBox txtFolderNew;
		private System.Windows.Forms.TextBox txtSaveAs;
		private System.Windows.Forms.Label label4;
	}
}

