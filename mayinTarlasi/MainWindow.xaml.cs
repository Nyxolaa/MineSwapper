using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace mayinTarlasi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public enum GameLevel {  Easy=10, Normal=20, Hard=40 }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //foreach (var level in Enum.GetValues(typeof(GameLevel)))
            //{
            //    comboGameLevel.Items.Add(level.ToString());
            //}
            Isload = true;
            CreateGameArea(10);
        }
        bool Isload = false;
        private void comboGameLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(comboGameLevel.SelectedItem.ToString());

            if (Isload)
            {
                ComboBoxItem item = ((ComboBoxItem)comboGameLevel.SelectedItem);

                string str = item.Content.ToString();
                int number = Convert.ToInt32(item.Tag);
                //MessageBox.Show(str + " " + number);

                CreateGameArea(number);
            }
        }

        private void CreateGameArea(int totalMine)
        {
            int count = 10;

            if (totalMine == 10)
            {
                count = 9;
            }
            else if (totalMine == 20)
            {
                count = 15;
            }
            else if (totalMine == 45)
            {
                count = 19;
            }

            wrpPanel.Children.Clear();

            wrpPanel.Width = count * 25;

            for (int i = 0; i < count*count; i++)
            {
                Button btn = new Button();
                btn.Height = 25;
                btn.Width = 25;
                btn.Tag = false;
                btn.Click += Btn_Click;

                if (i %2 == 0)
                    btn.Background = Brushes.MediumPurple;

                wrpPanel.Children.Add(btn);
            }

            placeMines(totalMine, count * count);
        }

        private void Btn_Click (object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if ((bool)btn.Tag == true)
            {
                btn.Background = Brushes.Red;
                MessageBox.Show("GAME OVER !!!");
                
                //yeniden baslat
                ShowAllMines();
                MessageBox.Show("");
                CreateGameArea(10);
            }
            else
            {
                    btn.Background = Brushes.Teal;
            }
        }

        private void placeMines(int totalMines, int totalPlace)
        {
            Random rnd = new Random();
            int counter = 0;

            do
            {
                Button btn = (Button)wrpPanel.Children[rnd.Next(0, totalPlace)];

                    if ((bool)btn.Tag==false)
                    {
                        btn.Tag = true;
                    //btn.Background = Brushes.Red;
                    counter++;
                    }

            } while (counter < totalMines);
        }

        private void ShowAllMines()
        {
            foreach (Button btn in wrpPanel.Children)
            {
                if ((bool)btn.Tag == true)
                {
                    btn.Background = Brushes.Red;
                }
            }
        }
    }
}
