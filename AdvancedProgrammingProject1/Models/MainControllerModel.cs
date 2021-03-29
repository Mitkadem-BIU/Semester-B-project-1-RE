using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml;
using System.Globalization;
using LumenWorks.Framework.IO.Csv;
using System.Threading;
using System;

namespace AdvancedProgrammingProject1
{
	/*
	 * Model's class of the main controller.
	 * For now I've only read the xml for the csv columns.
	 * I put the file as a member so that we would be able to access it later if needed.
	 * you should do the same for the CSV.
	 */
	public class MainControllerModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		string csvName;
		string xmlName;
		bool stop;
		XmlDocument doc;
		List<string> flightAttrNames;
		DataTable csvTable;
		int lineCounter;
		DataRow currentLine;
		string altimeter;
		string airspeed;
		string altitude;
		string roll;
		string pitch;
		string verticalSpeed;
		string groundSpeed;
		string heading;
		public string Csv
		{
			get { return csvName; }
			set {
				csvName = value;
				ReadCSV(csvName);
			}
		}

		public string Xml
		{
			get { return xmlName; }
			set {
				xmlName = value;
				ReadXML(xmlName);
			}
		}

		public bool Stop
		{
			get { return stop; }
			set { stop = value; }
		}

		public string Altimeter
		{
			get { return altimeter; }
			set {
				altimeter = value;
				NotifyPropertyChanged("altimeter");
			}
		}
		public string Airspeed
		{
			get { return airspeed; }
			set
			{
				airspeed = value;
				NotifyPropertyChanged("airspeed");
			}
		}
		public string Altitude
		{
			get { return altitude; }
			set
			{
				altitude = value;
				NotifyPropertyChanged("altitude");
			}
		}
		public string Roll
		{
			get { return roll; }
			set
			{
				roll = value;
				NotifyPropertyChanged("roll");
			}
		}
		public string Pitch
		{
			get { return pitch; }
			set
			{
				pitch = value;
				NotifyPropertyChanged("pitch");
			}
		}
		public string VerticalSpeed
		{
			get { return verticalSpeed; }
			set
			{
				verticalSpeed = value;
				NotifyPropertyChanged("verticalSpeed");
			}
		}
		public string GroundSpeed
		{
			get { return groundSpeed; }
			set
			{
				groundSpeed = value;
				NotifyPropertyChanged("groundSpeed");
			}
		}
		public string Heading
		{
			get { return heading; }
			set
			{
				heading = value;
				NotifyPropertyChanged("heading");
			}
		}


		public DataRow Line
		{
			get { return currentLine; }
			set { currentLine = value; }
		}

		public MainControllerModel()
		{
			doc = new XmlDocument();
			flightAttrNames = new List<string>();
			csvTable = new DataTable();
			stop = true;
			lineCounter = 0;
			currentLine = null;
		}

		public void ReadXML(string xmlName)
		{
			/*
			 * read CSV's columns names out of 
			 * (in input / output, in each chunk, the name is a column name)
			*/
			doc.Load(xmlName);
			XmlNode outputNode = doc.GetElementsByTagName("generic").Item(0).ChildNodes.Item(0);

			// Add parts to the list.
			foreach (XmlNode node in outputNode)
				if (node.Name == "chunk")
				{
					flightAttrNames.Add(node.FirstChild.FirstChild.Value);
				}
		}

		public void ReadCSV(string csvName)
		{
			/*
			 * read CSV file to some kind of time series,
			 * you can probably use what we did last semester.
			 */
			using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(csvName)), false))
			{
				csvTable.Load(csvReader);
			} 

			for (int i = 0; i < csvTable.Columns.Count; i++)
			{
				try
				{
					csvTable.Columns[i].ColumnName = flightAttrNames[i];
				}
				catch (DuplicateNameException)
				{
					csvTable.Columns[i].ColumnName = flightAttrNames[i] + "_1";
				}
				//Console.WriteLine(csvTable.Columns[i].ColumnName);
			}
		}

		public void ReadLine()
		{
			currentLine = csvTable.Rows[lineCounter];
			if (lineCounter <= csvTable.Rows.Count)
				lineCounter += 1;
			// Console.WriteLine(currentLine["aileron"]);
			Altimeter = ((string)currentLine["altimeter_indicated-altitude-ft"]);

			Airspeed = ((string)currentLine["altimeter_indicated-altitude-ft"]);
			Altitude = ((string)currentLine["altimeter_indicated-altitude-ft"]);
			Roll = ((string)currentLine["altimeter_indicated-altitude-ft"]);
			Pitch = ((string)currentLine["altimeter_indicated-altitude-ft"]);
			VerticalSpeed = ((string)currentLine["altimeter_indicated-altitude-ft"]);
			GroundSpeed = ((string)currentLine["altimeter_indicated-altitude-ft"]);
			Heading = ((string)currentLine["altimeter_indicated-altitude-ft"]);
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Start()
		{
			new Thread(delegate ()
			{
				while (!stop)
				{
					// read line and change all properties
					ReadLine();
					// Console.WriteLine("read line");
					Thread.Sleep(100);// read the data in 4Hz
				}
			}).Start();
			//again
		}

		public void StopMethod()
		{
			stop = true;
		}
	}
}