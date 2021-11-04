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
using DLLpro;

namespace Anketa_01._01__1_.pages
{
    public partial class DLL : Page
    {
        List<users> users;
        public DLL()
        {
            InitializeComponent();
            users = DB.Base.users.ToList();
        }

        private void nameserch_Click(object sender, RoutedEventArgs e)
        {
            vivodname.Text = null;
            List<string> nmsr = new List<string>();
            foreach (users us in users)
            {
                nmsr.Add(us.name);
            }

            if (nmsr.Count != 0)
            {
                nmsr = Metod.metodnmsr(nmsr, vvodname.Text);
                foreach (string nm in nmsr)
                {
                    vivodname.Text += nm + "\n";
                }
            }
            else
            {
                MessageBox.Show("Таких пользователей нет");
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            User.frmMain.GoBack();
        }

        private void vvodname_TextChanged(object sender, TextChangedEventArgs e)
        {
            vivodname.Text = null;
        }

        private void vivodage_TextChanged(object sender, TextChangedEventArgs e)
        {
            vivodage.Text = null;
        }

        private void ageavg_Click(object sender, RoutedEventArgs e)
        {
            vivodage.Text = null;
            int i = 0;
            DateTime[] Massive = new DateTime[users.Count];
            foreach (users us in users)
            {
                Massive[i] = us.dr;
                i++;
            }
            vivodage.Text = $"Средний возраст пользователей: {Metod.AgeAVG(Massive)}";
        }
    }
}
