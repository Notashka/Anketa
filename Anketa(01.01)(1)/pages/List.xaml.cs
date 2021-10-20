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
using Anketa_01._01__1_.pages;

namespace Anketa_01._01__1_
{
    /// <summary>
    /// Логика взаимодействия для List.xaml
    /// </summary>
    public partial class List : Page
    {
       
        List<users> users;
        public List()
        {
            InitializeComponent();
            users = DB.Base.users.ToList();
            lbUsersList.ItemsSource = users;
        }

        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = Convert.ToInt32(lb.Uid);
            lb.ItemsSource = DB.Base.users_to_traits.Where(x => x.id_user == index).ToList();
            lb.DisplayMemberPath = "traits.trait";
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            int OT = Convert.ToInt32(txtOT.Text) - 1;
            int DO = Convert.ToInt32(txtDO.Text);
            List<users> lu1 = users.Skip(OT).Take(DO - OT).ToList();
            lbUsersList.ItemsSource = lu1;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lbUsersList.ItemsSource = users;
        }

        private void btnNaz_Click(object sender, RoutedEventArgs e)
        {
            User.frmMain.GoBack();
        }

        private void Udal_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить пользователя из системы?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            Button b = (Button)sender;
            int id = Convert.ToInt32(b.Uid);
            auth SelectedUser = DB.Base.auth.FirstOrDefault(x => x.id == id);
            DB.Base.auth.Remove(SelectedUser);
            DB.Base.SaveChanges();
            MessageBox.Show("Выбранный пользователь удален");
            TimeSpan.FromSeconds(3);
            lbUsersList.ItemsSource = DB.Base.users.ToList();
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {          
            Button b = (Button)sender;
            int id = Convert.ToInt32(b.Uid);
            auth tUser = DB.Base.auth.FirstOrDefault(x => x.id == id);
            User.frmMain.Navigate(new Page5(tUser));
            lbUsersList.ItemsSource = DB.Base.users.ToList();
        }
    }
}
