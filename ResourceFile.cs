using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNNConnect.DNNResxCompare
{
	public class Key
	{
		public string Name { get; set; }
		public string OldValue { get; set; }
		public string NewValue { get; set; }
	}

	public class ResourceFile
	{
		public string FileName { get; set; }
		public string Path { get; set; }
		public List<Key> NewKeys { get; set; }
		public List<Key> DeletedKeys { get; set; }
		public List<Key> ModifiedKeys { get; set; }

		public string FullName
		{
			get
			{
				return System.IO.Path.Combine(Path, FileName);
			}
		}
	}
}
