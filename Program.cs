using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DNNConnect.DNNResxCompare
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			switch (args.Length)
			{
				case 0:
					// interactive mode;
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new Form1());
					break;
				case 1:
					// help?
					string message = "";
					if (args[0] == "/?" || args[0] == "-h" || args[0] == "/h")
					{
						message = "DNNRexCompare \"PreviousFolder\" \"NextFolder\" \"OutputFile\"\n\n" +
							"-PreviousFolder: Directory containing the base version of DotNetNuke to compare\n" +
							"-NextFolder: Directory containing the new version of DotNetNuke to compare to\n" +
							"-OutputFile: Full path and name of the xml file where the output report will be saved\n";
					}
					else
					{
						message = "Incorrect parameters. Use /? for help.";
					}
					MessageBox.Show(message, "DNNResxCompare");
					break;
				case 3:
					// command line mode
					Comparer c = new Comparer();
					c.Compare(args[0], args[1], args[2]);

					break;
			}
		}
	}
}
