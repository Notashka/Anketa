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
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public async void Load()
        {
            while (true)
            {
                dgUsers.ItemsSource = DB.Base.auth.ToList();
                await Task.Delay(1000);
            }
        }

        public Page3()
        {
            InitializeComponent();
            dgUsers.ItemsSource = DB.Base.auth.ToList();
            Load();
        } 

        private void btnSaveCahanges_Click(object sender, RoutedEventArgs e)
        {
            DB.Base.SaveChanges();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            auth SelectedUser = (auth)dgUsers.SelectedItem;//сохраняем выбранную строку datagrid в отдельный объект
            DB.Base.auth.Remove(SelectedUser);//удаляем эту строку из модели
            DB.Base.SaveChanges();//синхронизируем изменения с сервером
            MessageBox.Show("Выбранный пользователь удален");//обратная связь с оператором программы
            dgUsers.ItemsSource = DB.Base.auth.ToList();//обновить строки в datagrid
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            User.frmMain.GoBack();
        }

        private auth SelectedUser()
        {
            return (auth)dgUsers.SelectedItem;
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            User.frmMain.Navigate(new Page5(SelectedUser()));
        }
    }
}
