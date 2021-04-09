using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AdvancedProgrammingProject1
{
    public partial class Joystick : UserControl
    {
        JoystickViewModel vm;

        public Joystick()
        {
            vm = (Application.Current as App).JVM;
            InitializeComponent();
            DataContext = vm;
        }
    }
}
