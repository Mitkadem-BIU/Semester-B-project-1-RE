
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace AdvancedProgrammingProject1
{
    /*
     * Model's class of the main controller.
     * For now I've only read the xml for the csv columns.
     * I put the file as a member so that we would be able to access it later if needed.
     * you should do the same for the CSV.
     */
    class MainControllerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string csvName;
        string xmlName;
        XmlDocument doc;
        List<string> flightAttrNames;

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
        public MainControllerModel()
        {
            doc = new XmlDocument();
            flightAttrNames = new List<string>();
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
                if(node.Name == "chunk")
                    flightAttrNames.Add(node.FirstChild.FirstChild.Value);
        }

        public void ReadCSV(string csvName)
        {
            /*
             * read CSV file to some kind of time series,
             * you can probably use what we did last semester.
             */

        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}