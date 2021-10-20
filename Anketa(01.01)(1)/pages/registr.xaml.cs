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

namespace Anketa_01._01__1_.pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            listGenders.ItemsSource = DB.Base.genders.ToList();
            listGenders.SelectedValuePath = "id";
            listGenders.DisplayMemberPath = "gender";         
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            User.frmMain.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                auth logPass = new auth() { login = txtLogin.Text, password = txtPass.Password, role = 2 };
                DB.Base.auth.Add(logPass);
                DB.Base.SaveChanges();
                                     
                users data = new users();
                data.id = logPass.id;
                data.name = nameTextBox.Text;
                data.dr = dateBirth.SelectedDate.Value;
                data.gender = (int)listGenders.SelectedValue;
                DB.Base.users.Add(data);
                if (good.IsChecked == true)
                {
                    users_to_traits tr = new users_to_traits();
                    tr.id_user = logPass.id;
                    tr.id_trait = DB.Base.traits.FirstOrDefault(x => x.trait == good.Content.ToString()).id;
                    DB.Base.users_to_traits.Add(tr);
                }
                if (zloi.IsChecked == true)
                {
                    users_to_traits tr = new users_to_traits();
                    tr.id_user = logPass.id;
                    tr.id_trait = DB.Base.traits.FirstOrDefault(x => x.trait == zloi.Content.ToString()).id;
                    DB.Base.users_to_traits.Add(tr);
                }
                if (psix.IsChecked == true)
                {
                    users_to_traits tr = new users_to_traits();
                    tr.id_user = logPass.id;
                    tr.id_trait = DB.Base.traits.FirstOrDefault(x => x.trait == psix.Content.ToString()).id;
                    DB.Base.users_to_traits.Add(tr);
                }
                DB.Base.SaveChanges();
                MessageBox.Show("Данные записаны успешно");
                User.frmMain.GoBack();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }
    }
}
