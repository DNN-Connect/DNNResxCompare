using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DNNConnect.DNNResxCompare
{
	public class Comparer
	{
		private List<ResourceFile> NewFiles;
		private List<ResourceFile> DeletedFiles;
		private List<ResourceFile> ChangedFiles;
		private List<ResourceFile> BadFiles;

		string basePathOld;
		string basePathNew;
		string saveAs;

		public Comparer()
		{
		}

		public void Compare(string folderOld, string folderNew, string outputFilename)
		{
			if (string.IsNullOrEmpty(folderOld) ||
				string.IsNullOrEmpty(folderNew) ||
				string.IsNullOrEmpty(outputFilename) ||
				!Directory.Exists(folderOld) ||
				!Directory.Exists(folderNew))
			{
				throw new ArgumentException("Provide the Previous and New folders and the File name for the report.");
			}

			NewFiles = new List<ResourceFile>();
			DeletedFiles = new List<ResourceFile>();
			ChangedFiles = new List<ResourceFile>();
			BadFiles = new List<ResourceFile>();

			basePathOld = folderOld;
			basePathNew = folderNew;
			saveAs = outputFilename;
			ProcessFolder(folderOld);
			ProcessFolderNew(folderNew);

			GenerateReport();
		}

		private void ProcessFolder(string currentFolder)
		{
			DirectoryInfo dir = new DirectoryInfo(currentFolder);
			DirectoryInfo[] folders = dir.GetDirectories();

			// parse all folders and files from the "Old" base folder
			foreach (DirectoryInfo folder in folders)
			{
				if (folder.Name.ToLower() == "app_localresources" || folder.Name.ToLower() == "app_globalresources")
				{
					foreach (FileInfo file in folder.GetFiles("*.resx"))
					{
						//Ignore languagefiles "xx-XX"
						if (Path.GetFileNameWithoutExtension(file.Name).LastIndexOf("-") != Path.GetFileNameWithoutExtension(file.Name).Length - 3)
						{
							ProcessFile(file);
						}
					}
				}
				else if (folder.Name.ToLower() == "_default")
				{
					// Process templates
					foreach (FileInfo file in folder.GetFiles("*.template.en-US.resx"))
					{
						ProcessFile(file);
					}
					ProcessFolder(folder.FullName);
				}
				else
				{
					ProcessFolder(folder.FullName);
				}
			}
		}

		private void ProcessFile(FileInfo file)
		{
			// check if file exists in New folder
			string newFile = basePathNew + file.FullName.Substring(basePathOld.Length);
			if (File.Exists(newFile))
			{
				FileInfo iNewFile = new FileInfo(newFile);
				CompareFiles(file, iNewFile);
			}
			else
			{
				ResourceFile deletedfile = new ResourceFile()
				{
					FileName = file.Name,
					Path = file.DirectoryName.Substring(basePathOld.Length),
					DeletedKeys = new List<Key>()
				};
				XDocument xdoc = XDocument.Load(file.FullName);
				deletedfile.DeletedKeys.AddRange(
					from s in xdoc.Descendants("data")
					select
						new Key()
						{
							Name = s.Attribute("name").Value,
							OldValue = s.Descendants("value").First().Value
						}
				);
				DeletedFiles.Add(deletedfile);
			}
		}

		private void CompareFiles(FileInfo oldFile, FileInfo newFile)
		{
			var dsOld = new DataSet();
			var dsNew = new DataSet();
			DataTable dtOld;
			DataTable dtNew;

			try
			{
				dsOld.ReadXml(oldFile.FullName);
			}
			catch
			{
				BadFiles.Add(new ResourceFile()
				{
					FileName = "Malformed resource file: " + oldFile.FullName
				});
			}
			try
			{
				dsNew.ReadXml(newFile.FullName);
			}
			catch
			{
				BadFiles.Add(new ResourceFile()
				{
					FileName = "Malformed resource file: " + newFile
				});
			}

			dtOld = dsOld.Tables["data"];
			dtOld.TableName = "old";

			dtNew = dsNew.Tables["data"].Copy();
			dtNew.TableName = "new";

			dsOld.Tables.Add(dtNew);

			try
			{
				// if this fails -> old file contains duplicates
				var c = new UniqueConstraint("uniqueness", dtOld.Columns["name"]);
				dtOld.Constraints.Add(c);
				dtOld.Constraints.Remove("uniqueness");
			}
			catch
			{
				BadFiles.Add(new ResourceFile()
				{
					FileName = "Duplicate keys found. Invalid resource file: " + oldFile.FullName
				});
			}
			try
			{
				// if this fails -> new file contains duplicates
				var c = new UniqueConstraint("uniqueness", dtNew.Columns["name"]);
				dtNew.Constraints.Add(c);
				dtNew.Constraints.Remove("uniqueness");
			}
			catch
			{
				BadFiles.Add(new ResourceFile()
				{
					FileName = "Duplicate keys found. Invalid resource file: " + newFile
				});
			}

			ResourceFile currentResx = new ResourceFile()
			{
				FileName = oldFile.Name,
				Path = oldFile.DirectoryName.Substring(basePathOld.Length),
				NewKeys = new List<Key>(),
				DeletedKeys = new List<Key>(),
				ModifiedKeys = new List<Key>()
			};
			bool changed = false;

			// process all keys from new file
			foreach (DataRow newkey in dtNew.Rows)
			{
				string key = newkey["name"].ToString();

				DataRow[] oldkeys = dtOld.Select("name='" + key.Replace("'", "''") + "'");
				if (oldkeys == null || oldkeys.Length == 0)
				{
					// new key
					currentResx.NewKeys.Add(new Key()
					{
						Name = key,
						NewValue = newkey["value"].ToString()
					});
					changed = true;
				}
				else
				{
					DataRow oldkey = oldkeys[0];
					if (newkey["value"].ToString() != oldkey["value"].ToString())
					{
						// changed key
						currentResx.ModifiedKeys.Add(new Key()
						{
							Name = key,
							OldValue = oldkey["value"].ToString(),
							NewValue = newkey["value"].ToString()
						});
						changed = true;
					}
				}
			}

			// process all keys from old files, just check for deleted keys
			foreach (DataRow oldkey in dtOld.Rows)
			{
				string key = oldkey["name"].ToString();

				DataRow[] newkeys = dtNew.Select("name='" + key + "'");
				if (newkeys == null || newkeys.Length == 0)
				{
					// removed key
					currentResx.DeletedKeys.Add(new Key()
					{
						Name = key,
						OldValue = oldkey["value"].ToString()
					});
					changed = true;
				}
			}

			if (changed)
				ChangedFiles.Add(currentResx);
		}

		private void ProcessFolderNew(string currentFolder)
		{
			DirectoryInfo dir = new DirectoryInfo(currentFolder);
			DirectoryInfo[] folders = dir.GetDirectories();

			// parse all folders and files from the "New" base folder to find new files
			foreach (DirectoryInfo folder in folders)
			{
				if (folder.Name.ToLower() == "app_localresources" || folder.Name.ToLower() == "app_globalresources")
				{
					foreach (FileInfo file in folder.GetFiles("*.resx"))
					{
						//Ignore languagefiles "xx-XX"
						if (Path.GetFileNameWithoutExtension(file.Name).LastIndexOf("-") != Path.GetFileNameWithoutExtension(file.Name).Length - 3)
						{
							if (IsNewFile(file))
							{
								ResourceFile newfile = new ResourceFile()
								{
									FileName = file.Name,
									Path = file.DirectoryName.Substring(basePathNew.Length),
									NewKeys = new List<Key>()
								};
								XDocument xdoc = XDocument.Load(file.FullName);
								newfile.NewKeys.AddRange(
									from s in xdoc.Descendants("data")
									select
										new Key()
										{
											Name = s.Attribute("name").Value,
											NewValue = s.Descendants("value").First().Value
										}
								);
								NewFiles.Add(newfile);
							}
						}
					}
				}
				else
				{
					ProcessFolderNew(folder.FullName);
				}
			}
		}

		private bool IsNewFile(FileInfo file)
		{
			// check if file exists in Old folder
			string oldFile = basePathOld + file.FullName.Substring(basePathNew.Length);
			return !File.Exists(oldFile);
		}

		private void GenerateReport()
		{
			XmlDocument xDoc = new XmlDocument();
			XmlWriterSettings set = new XmlWriterSettings();
			set.Indent = true;
			set.IndentChars = " ";
			XmlWriter xWrit = XmlWriter.Create(saveAs, set);
			xWrit.WriteStartDocument();

			// xsl formating
			String strPI = "type='text/xsl' href='Diff.xsl'";
			xWrit.WriteProcessingInstruction("xml-stylesheet", strPI);
			xWrit.WriteStartElement("resourceCompare");

			// Start -> summary
			xWrit.WriteStartElement("summary");

			xWrit.WriteStartElement("totalDeleted");
			xWrit.WriteAttributeString("files", DeletedFiles.Count.ToString());
			xWrit.WriteEndElement();

			xWrit.WriteStartElement("totalNew");
			xWrit.WriteAttributeString("files", NewFiles.Count.ToString());
			xWrit.WriteAttributeString("keys", NewFiles.Sum(n => n.NewKeys.Count).ToString());
			xWrit.WriteAttributeString("words", NewFiles.Sum(n => n.NewKeys.Sum(k => CountWords(k.NewValue))).ToString());
			xWrit.WriteEndElement();

			xWrit.WriteStartElement("totalModified");
			xWrit.WriteAttributeString("files", ChangedFiles.Count.ToString());
			xWrit.WriteAttributeString("keys", (ChangedFiles.Sum(n => n.NewKeys.Count) +
												ChangedFiles.Sum(m => m.ModifiedKeys.Count)).ToString());
			xWrit.WriteAttributeString("words", (ChangedFiles.Sum(n => n.NewKeys.Sum(nk => CountWords(nk.NewValue))) +
												ChangedFiles.Sum(m => m.ModifiedKeys.Sum(mk => CountWords(mk.NewValue)))).ToString());
			xWrit.WriteEndElement();


			xWrit.WriteEndElement();
			// End -> summary


			// Start -> new files
			if (NewFiles.Count > 0)
			{
				xWrit.WriteStartElement("newFiles");
				foreach (ResourceFile file in NewFiles)
				{
					xWrit.WriteStartElement("file");
					xWrit.WriteElementString("name", file.FileName);
					xWrit.WriteElementString("path", file.Path);

					if (file.NewKeys.Count > 0)
					{
						xWrit.WriteStartElement("newEntries");
						foreach (Key key in file.NewKeys)
						{
							xWrit.WriteStartElement("key");
							xWrit.WriteElementString("name", key.Name);
							xWrit.WriteElementString("newValue", key.NewValue);
							xWrit.WriteEndElement();
						}
						xWrit.WriteEndElement(); // newEntries
					}

					xWrit.WriteEndElement();
				}
				xWrit.WriteEndElement();
			}
			// End -> newFiles


			// Start -> deleted files
			if (DeletedFiles.Count > 0)
			{
				xWrit.WriteStartElement("deletedFiles");
				foreach (ResourceFile file in DeletedFiles)
				{
					xWrit.WriteStartElement("file");
					xWrit.WriteElementString("name", file.FileName);
					xWrit.WriteElementString("path", file.Path);

					if (file.DeletedKeys.Count > 0)
					{
						xWrit.WriteStartElement("deletedEntries");
						foreach (Key key in file.DeletedKeys)
						{
							xWrit.WriteStartElement("key");
							xWrit.WriteElementString("name", key.Name);
							xWrit.WriteElementString("oldValue", key.OldValue);
							xWrit.WriteEndElement();
						}
						xWrit.WriteEndElement(); // deletedEntries
					}
					xWrit.WriteEndElement();
				}
				xWrit.WriteEndElement();
			}
			// End -> deleted Files

			// Start -> changed files
			if (ChangedFiles.Count > 0)
			{
				xWrit.WriteStartElement("modifiedFiles");
				foreach (ResourceFile file in ChangedFiles)
				{
					xWrit.WriteStartElement("file");
					xWrit.WriteElementString("name", file.FileName);
					xWrit.WriteElementString("path", file.Path);

					if (file.NewKeys.Count > 0)
					{
						xWrit.WriteStartElement("newEntries");
						foreach (Key key in file.NewKeys)
						{
							xWrit.WriteStartElement("key");
							xWrit.WriteElementString("name", key.Name);
							xWrit.WriteElementString("newValue", key.NewValue);
							xWrit.WriteEndElement();
						}
						xWrit.WriteEndElement(); // newEntries
					}

					if (file.ModifiedKeys.Count > 0)
					{
						xWrit.WriteStartElement("modifiedEntries");
						foreach (Key key in file.ModifiedKeys)
						{
							xWrit.WriteStartElement("key");
							xWrit.WriteElementString("name", key.Name);
							xWrit.WriteElementString("oldValue", key.OldValue);
							xWrit.WriteElementString("newValue", key.NewValue);
							xWrit.WriteEndElement();
						}
						xWrit.WriteEndElement(); // modifiedEntries
					}

					if (file.DeletedKeys.Count > 0)
					{
						xWrit.WriteStartElement("deletedEntries");
						foreach (Key key in file.DeletedKeys)
						{
							xWrit.WriteStartElement("key");
							xWrit.WriteElementString("name", key.Name);
							xWrit.WriteElementString("oldValue", key.OldValue);
							xWrit.WriteEndElement();
						}
						xWrit.WriteEndElement(); // deletedEntries
					}
					xWrit.WriteEndElement(); // file
				}
				xWrit.WriteEndElement();
			}
			// End -> modifiedEntries

			xWrit.WriteEndElement(); // resourceCompare
			xWrit.WriteEndDocument();

			xWrit.Flush();
			xWrit.Close();
		}

		private int CountWords(string value)
		{
			MatchCollection collection = Regex.Matches(value, @"[\S]+");
			return collection.Count;
		}
	}
}
