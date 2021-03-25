using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AdvancedProgrammingProject1.Views
{
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
            get { return X; }
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
