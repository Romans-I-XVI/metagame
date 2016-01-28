using System;
using System.Xml;
using System.IO;

namespace metagame
{
	public class Letter
	{
		public string letter_string;
		public int line_position = 0;
		public int[,] letter_positions = new int[5,5];

		// Construct the letter object based on the Char and corresponding positions defined in letter_positions.xml
		public Letter (char letter)
		{
			this.letter_string = letter.ToString();
			GetResource get_resource = new GetResource ();
			string local_xml_file_string = get_resource.GetResourceTextFile ("letter_positions.xml");
			XmlDocument xml = new XmlDocument();
			xml.LoadXml(local_xml_file_string);
			XmlNode xNode = xml.SelectSingleNode("/Letters/"+this.letter_string);
			for (int r = 0; r <= 4; r++)
			{
				string row_string = xNode ["row_"+r.ToString()].InnerText;
				char[] row_chars = row_string.ToCharArray ();
				for (int c = 0; c <= 4; c++)
					this.letter_positions [r, c] = int.Parse (row_chars [c].ToString ());
			}
		}

		// Shift the active line position if all blocks have been covered
		public void update_line_position()
		{
			bool all_blocks_covered = true;
			for (int i = 0; i <= 4; i++) {
				if (this.letter_positions [this.line_position, i] == 1) {
					all_blocks_covered = false;
				}
			}
			if (all_blocks_covered)
				this.line_position++;
		}

		// reset the active line colors
		public void reset_active_line()
		{
			for (int i = 0; i <= 4; i++) {
				if (this.letter_positions[this.line_position, i] == 2) 
					this.letter_positions[this.line_position, i] = 1;
			}
		}
	}
}

