using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class ConfigManager : MonoBehaviour {
	public string configdata;
	public string line;
	public bool exitloop = false;
	public string Load(string tag)//function used pull values from the config file
	{
		string filename = Application.dataPath +"/config.ini"; //The path of the config file
		StreamReader theReader = new StreamReader(filename, Encoding.Default);//Reader used to read in lines
		using (theReader)
		{
			exitloop = false;
			do { //loops until tag is found
				line = theReader.ReadLine ();//reads in a line
				if (line == tag) {//finds tag of variable
					line = theReader.ReadLine ();//loads next value after tag
					configdata = line;
					exitloop = true;
				}
				else if (line == null) {//makes sure the line isn't null
					configdata = "1";
					exitloop = true;
				}
			} while(exitloop == false);
			theReader.Close ();//closes the reader
			return configdata;

		}
	}
}
	