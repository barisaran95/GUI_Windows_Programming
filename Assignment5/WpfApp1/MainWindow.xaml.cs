using System;
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

using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace WpfApp1
{
    public class Grid
    {
        public string surname { set; get; }
        public string name { set; get; }
        public int num1 { set; get; }
        public int num2 { set; get; }
        public int num3 { set; get; }

    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           
            InitializeComponent();
            
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string line;
            List<String[]> myList = new List<String[]>();
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\temp\oscourse\360-p6.txt");
            //myList = File.ReadLine(@"C:/Users/Baris/Desktop/Adalet.txt");
            List<Grid> objList = new List<Grid>();
            while ((line = file.ReadLine()) != null)
            {
                myList.Add(line.Split(','));
            }

            for (int i = 0; i < myList.Count; i++)
            {
                Grid grid = new Grid();
                grid.surname = myList[i][0].Trim('"');
                grid.name = myList[i][1].Trim('"');
                grid.num1 = Convert.ToInt32(myList[i][2]);
                grid.num2 = Convert.ToInt32(myList[i][3]);
                grid.num3 = Convert.ToInt32(myList[i][4]);
                objList.Add(grid);
            }
            BarisGrid.ItemsSource = objList;
        }
    }
}
