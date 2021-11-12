using System;
using System.IO;
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
using Anketa_01._01__1_.pages;

namespace Anketa_01._01__1_
{
    public static class User
    {
        public static Frame frmMain;
    }
    public static class DB
    {
        public static Entities1 Base;
        public static auth currentUser;
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            User.frmMain = frmMain;
            frmMain.Navigate(new Page2());
            DB.Base = new Entities1();
        }

        private void frmMain_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
