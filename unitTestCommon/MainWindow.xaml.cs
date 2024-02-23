using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Common;

namespace unitTestCommon
{
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent ();

            Left = 1000;
        }

        private void Window_Loaded (object sender, RoutedEventArgs e)
        {
           List<NameValuePair> pairs = new List<NameValuePair> ();

           Console.WriteLine ("Unit test for Common library");

            try
            {
                pairs.Add (new NameValuePair ("aaa", 123.456));
                pairs.Add (new NameValuePair ("bbb", new Point (55, 66)));
                pairs.Add (new NameValuePair ("ccc", true));
                pairs.Add (new NameValuePair ("ddd", "another string"));

                Console.WriteLine (pairs.Count.ToString () + " pairs");

                foreach (NameValuePair p in pairs)
                {
                    Console.WriteLine (p);

                    switch (p.Value.GetType ().Name)
                    {
                        case "Double":
                            double d = (double) p.Value;
                            Console.WriteLine ("DOUBLE " + d.ToString ());
                            break;

                        case "Point":
                            Point pt = (Point)p.Value;
                            Console.WriteLine ("POINT " + pt.ToString ());
                            break;

                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine ("Exception: " + ex.Message);
            }
        }
    }
}
