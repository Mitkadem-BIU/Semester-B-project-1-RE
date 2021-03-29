using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AdvancedProgrammingProject1.Views
{

    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CenterKnob_Completed(object sender, EventArgs e)
        {

        }
    }
    public class JoystickEventArgs
    {
        private double x;
        private double y;
        public JoystickEventArgs()
        {
            x = 0;
            y = 0;
        }
        public double X
        {
            get { return x; }
            set
            {
                x = value;
            }
        }
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
            }
        }
    }
}
