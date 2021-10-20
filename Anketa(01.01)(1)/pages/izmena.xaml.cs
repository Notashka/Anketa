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

namespace Anketa_01._01__1_.pages
{
    /// <summary>
    /// Логика взаимодействия для Page4.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        private auth Izmenenie;
        public Page5(auth Izmenenie)
        {
            InitializeComponent();
            listGenders.ItemsSource = DB.Base.genders.ToList();
            listGenders.SelectedValuePath = "id";
            listGenders.DisplayMemberPath = "gender";
            this.Izmenenie = Izmenenie;
            txtLogin.Text = Izmenenie.login;
            txtPass.Password = Izmenenie.password;
            nameTextBox.Text = Izmenenie.users.name;
            dateBirth.SelectedDate = Izmenenie.users.dr;
            listGenders.SelectedIndex = Izmenenie.users.gender - 1;
            List<users_to_traits> trs = Izmenenie.users.users_to_traits.ToList();
            foreach (users_to_traits tr in trs)
            {
                string trName = DB.Base.traits.FirstOrDefault(x => x.id == tr.id_trait).trait;
                if ((string)good.Content == trName)
                {
                    good.IsChecked = true;
                }
                if ((string)zloi.Content == trName)
                {
                    zloi.IsChecked = true;
                }
                if ((string)psix.Content == trName)
                {
                    psix.IsChecked = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string password;
            if (txtLogin.Text != "" && DB.Base.auth.FirstOrDefault(x => x.login == txtLogin.Text) == null || txtLogin.Text == Izmenenie.login)
            {
                if (nameTextBox.Text != "")
                {
                    if (txtPass.Password == "")
                    {
                        password = Izmenenie.password;
                    }
                    else
                    {
                        password = txtPass.Password;
                    }
                    //Изменение данных выбранного пользователя
                    Izmenenie.login = txtLogin.Text;
                    Izmenenie.password = txtPass.Password;
                    Izmenenie.users.name = nameTextBox.Text;
                    Izmenenie.users.dr = (DateTime)dateBirth.SelectedDate;
                    Izmenenie.users.gender = (int)listGenders.SelectedValue;
                    List<users_to_traits> trOnDelete = DB.Base.users_to_traits.Where(x => x.id_user == Izmenenie.id).ToList();
                    foreach (users_to_traits tr in trOnDelete)
                    {
                        DB.Base.users_to_traits.Remove(tr);
                    }
                    DB.Base.SaveChanges();
                    if (good.IsChecked == true)
                    {
                        users_to_traits tr = new users_to_traits();
                        tr.id_user = Izmenenie.id;
                        tr.id_trait = DB.Base.traits.FirstOrDefault(x => x.trait == good.Content.ToString()).id;
                        DB.Base.users_to_traits.Add(tr);
                    }
                    if (zloi.IsChecked == true)
                    {
                        users_to_traits tr = new users_to_traits();
                        tr.id_user = Izmenenie.id;
                        tr.id_trait = DB.Base.traits.FirstOrDefault(x => x.trait == zloi.Content.ToString()).id;
                        DB.Base.users_to_traits.Add(tr);
                    }
                    if (psix.IsChecked == true)
                    {
                        users_to_traits tr = new users_to_traits();
                        tr.id_user = Izmenenie.id;
                        tr.id_trait = DB.Base.traits.FirstOrDefault(x => x.trait == psix.Content.ToString()).id;
                        DB.Base.users_to_traits.Add(tr);
                    }
                    DB.Base.SaveChanges();
                    MessageBox.Show("Изменение данных выполнено успешно");
                    User.frmMain.GoBack();
                }
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            User.frmMain.GoBack();
        }
    }
}
