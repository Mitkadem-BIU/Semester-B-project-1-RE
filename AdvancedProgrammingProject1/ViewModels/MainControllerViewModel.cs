using System;
using System.ComponentModel;
using System.Net.Sockets;


namespace AdvancedProgrammingProject1
{
    // Model View Class. Nothing special over here.
    public class MainControllerViewModel : INotifyPropertyChanged
    {
        TcpClient myClient;
        public bool isConnected = false;
        private MainControllerModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public string VM_csv
        {
            get { return model.Csv; }
            set { model.Csv = value; }
        }

        public string VM_xml
        {
            get { return model.Xml; }
            set { model.Xml = value; }
        }

        public bool VM_stop
        {
            get { return model.Stop; }
            set { model.Stop = value; }
        }

        public string VM_altimeter
        {
            get { return model.Altimeter; }
            set { model.Altimeter = value; }
        }
        public string VM_airspeed
        {
            get { return model.Airspeed; }
            set { model.Airspeed = value; }
        }
        public string VM_altitude
        {
            get { return model.Altitude; }
            set { model.Altitude = value; }
        }
        public string VM_roll
        {
            get { return model.Roll; }
            set { model.Roll = value; }
        }
        public string VM_pitch
        {
            get { return model.Pitch; }
            set { model.Pitch = value; }
        }
        public string VM_verticalSpeed
        {
            get { return model.VerticalSpeed; }
            set { model.VerticalSpeed = value; }
        }
        public string VM_groundSpeed
        {
            get { return model.GroundSpeed; }
            set { model.GroundSpeed = value; }
        }
        public string VM_heading
        {
            get { return model.Heading; }
            set { model.Heading = value; }
        }

        public void Start()
        {
            model.Start();
        }

        public MainControllerViewModel(MainControllerModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propertyName) {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            model.NotifyPropertyChanged(propertyName);
        }

        public void connect(string ip, int port)
        {
            //myClient = new TcpClient(ip, port);
            //isConnected = true;

         //   Socket fg = new Socket(ip, port);
         //   BufferedReader in= new BufferedReader(new FileReader("reg_flight.csv"));
         //   PrintWriter out= new PrintWriter(fg.getOutputStream());
         //   String line;
       //     while ((line =in.readLine())!= null) {
        //    out.println(line);
        //     out.flush();
        //        Thread.sleep(100);
        //    }
        //    out.close();
       //     in.close();
      //      fg.close();




        }

        public void disconnect()
        {
            if (isConnected)
            {
                this.myClient.Close();
                isConnected = false;
            }
        }
    }
}