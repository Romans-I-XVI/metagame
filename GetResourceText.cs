using System;
using System.IO;

namespace metagame
{
	public class GetResource
	{
		public string GetResourceTextFile(string filename)
		{
			string result = string.Empty;

			using (Stream stream = this.GetType().Assembly.GetManifestResourceStream("metagame."+filename))
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					result = sr.ReadToEnd();
				}
			}
			return result;
		}
	}
}

