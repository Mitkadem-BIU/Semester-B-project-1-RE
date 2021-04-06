using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml;
using LumenWorks.Framework.IO.Csv;
using System.Threading;
using System;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

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
		string ip;
		int port;
		XmlDocument doc;
		List<string> flightAttrNames;
		DataTable csvTable;
		int lineCounter;
		DataRow currentLine;
		bool pause;
		double altimeter;
		double airSpeed;
		double altitude;
		double roll;
		double pitch;
		double verticalSpeed;
		double groundSpeed;
		double heading;
		double speed;
		
		public double Speed
		{
			get { return speed; }
			set { speed = value; }
		}
		public PlotModel PlotModel { get; private set; }
		public AttrPlotModel AP { get; }
		public ControlBarModel CB { get; }
	
		public float Time
		{
			get { return (float)LineCounter / 10; }
			set { LineCounter = (int)(value * 10);
			//	NotifyPropertyChanged("time");
			}
		}
		public DataTable CSVTable
		{
			get { return csvTable; }
			set { csvTable = value; }
		}
		public string Csv
		{
			get { return csvName; }
			set
			{
				csvName = value;
				ReadCSV(csvName);
			}
		}

		public string Xml
		{
			get { return xmlName; }
			set
			{
				xmlName = value;
				ReadXML(xmlName);
			}
		}

		public bool Stop
		{
			get { return stop; }
			set { stop = value; }
		}

		public bool Pause
		{
			get { return pause; }
			set
			{
				pause = value;
				NotifyPropertyChanged("pause");
			}
		}

		public string IP
		{
			get { return ip; }
			set { ip = value; }
		}

		public int Port
		{
			get { return port; }
			set { port = value; }
		}

		public FGModel Client { get; }
		public List<string> FlightAttrNames
		{
			get { return flightAttrNames; }
		}

		public double Altimeter
		{
			get { return altimeter; }
			set
			{
				altimeter = value;
				NotifyPropertyChanged("altimeter");
			}
		}
		public double AirSpeed
		{
			get { return airSpeed; }
			set
			{
				airSpeed = value;
				NotifyPropertyChanged("airSpeed");
			}
		}
		public double Altitude
		{
			get { return altitude; }
			set
			{
				altitude = value;
				NotifyPropertyChanged("altitude");
			}
		}
		public double Roll
		{
			get { return roll; }
			set
			{
				roll = value;
				NotifyPropertyChanged("roll");
			}
		}
		public double Pitch
		{
			get { return pitch; }
			set
			{
				pitch = value;
				NotifyPropertyChanged("pitch");
			}
		}
		public double VerticalSpeed
		{
			get { return verticalSpeed; }
			set
			{
				verticalSpeed = value;
				NotifyPropertyChanged("verticalSpeed");
			}
		}
		public double GroundSpeed
		{
			get { return groundSpeed; }
			set
			{
				groundSpeed = value;
				NotifyPropertyChanged("groundSpeed");
			}
		}
		public double Heading
		{
			get { return heading; }
			set
			{
				heading = value;
				NotifyPropertyChanged("heading");
			}
		}


		public DataRow Row
		{
			get { return currentLine; }
			set
			{
				currentLine = value;
				NotifyPropertyChanged("Row");
			}
		}

		public int LineCounter
		{
			get { return lineCounter; }
			set
			{
				lineCounter = value;
				NotifyPropertyChanged("lineCounter");
				NotifyPropertyChanged("time");
			}
		}


		public MainControllerModel()
		{
			speed = 1;
			doc = new XmlDocument();
			flightAttrNames = new List<string>();
			csvTable = new DataTable();
			stop = true;
			lineCounter = 0;
			currentLine = null;
			Client = new FGModel(this);
			AP = new AttrPlotModel(this);
			CB = new ControlBarModel(this);
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
			}
		}

		public void ReadLine()
		{
			if (lineCounter < csvTable.Rows.Count)
			{
				Row = csvTable.Rows[lineCounter];
				lineCounter++;

				Altimeter = Double.Parse((string)currentLine["altimeter_indicated-altitude-ft"]);
				AirSpeed = Double.Parse((string)currentLine["airspeed-indicator_indicated-speed-kt"]);
				Altitude = Double.Parse((string)currentLine["altitude-ft"]);
				Roll = Double.Parse((string)currentLine["attitude-indicator_indicated-roll-deg"]);
				Pitch = Double.Parse((string)currentLine["attitude-indicator_indicated-pitch-deg"]);
				VerticalSpeed = Double.Parse((string)currentLine["vertical-speed-indicator_indicated-speed-fpm"]);
				GroundSpeed = Double.Parse((string)currentLine["gps_indicated-ground-speed-kt"]);
				Heading = Double.Parse((string)currentLine["indicated-heading-deg"]);
			}
			else
			{
				//StopMethod();
				pause = true;
			}
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Run()
		{
			new Thread(delegate ()
			{
				while (!stop)
				{
					if (!pause)
					{
						
						LineCounter = LineCounter;
						// read line and change all properties
						ReadLine();
						Thread.Sleep((int)(100 / speed));// read the data in 10Hz

					}
				}
			}).Start();
		}

		public void StopMethod()
		{
			Stop = true;
			Client.Disconnect();
		}
	}
}