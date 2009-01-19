
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Atlanta.Presentation
{

    public class Main : UserControl
    {

        public Main()
        {
            Loaded += new RoutedEventHandler(Main_Loaded);
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
        }

    }
}