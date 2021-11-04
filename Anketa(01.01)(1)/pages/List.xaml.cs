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
        List<users> l1;
        public List()
        {
            InitializeComponent();
            users = DB.Base.users.ToList();
            lbUsersList.ItemsSource = users;
            lbGenderFilter.ItemsSource = DB.Base.genders.ToList();
            lbGenderFilter.SelectedValuePath = "id";
            lbGenderFilter.DisplayMemberPath = "gender";
            l1 = users;
        }

        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = Convert.ToInt32(lb.Uid);
            lb.ItemsSource = DB.Base.users_to_traits.Where(x => x.id_user == index).ToList();
            lb.DisplayMemberPath = "traits.trait";
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            //по строкам
            try
            {
                int Ot = Convert.ToInt32(txtOT.Text) - 1;
                int Do = Convert.ToInt32(txtDO.Text);
                l1 = users.Skip(Ot).Take(Do - Ot).ToList();
            }
            catch
            { /*ничего не надо делать, если этот фильтр не применен*/ }

            //по полу
            try
            {
                if (lbGenderFilter.SelectedIndex != -1)
                    l1 = l1.Where(x => x.gender == Convert.ToInt32(lbGenderFilter.SelectedValue)).ToList();
            }
            catch { }
            //по имени
            try
            {
                if (txtNameFilter.Text != "")
                {
                    l1 = l1.Where(x => x.name.Contains(txtNameFilter.Text)).ToList();
                }
            }
            catch { }

            lbUsersList.ItemsSource = l1;
        }

        //private void btnGo_Click(object sender, RoutedEventArgs e)
        //{
        //    int OT = Convert.ToInt32(txtOT.Text) - 1;
        //    int DO = Convert.ToInt32(txtDO.Text);
        //    List<users> lu1 = users.Skip(OT).Take(DO - OT).ToList();
        //    lbUsersList.ItemsSource = lu1;
        //}

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            //lbUsersList.ItemsSource = users;
            lbUsersList.ItemsSource = users;//в качестве источника данных исходный список
            lbGenderFilter.SelectedIndex = -1; //сбрасываем выбранный элемент списка
            txtNameFilter.Text = "";//сбрасываем фильтр на строку
            txtOT.Text = "";
            txtDO.Text = "";
        }

        private void btnNaz_Click(object sender, RoutedEventArgs e)
        {
            User.frmMain.GoBack();
        }

        private void btnDll_Click(object sender, RoutedEventArgs e)
        {
            User.frmMain.Navigate(new DLL());
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

        private void tbStart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
        int currpage = 1;
        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TextBlock tb = (TextBlock)sender;
                int countList = users.Count;
                int countzapis = Convert.ToInt32(txtPageCount.Text);
                int countpages = countList / countzapis;

                switch (tb.Uid)
                {
                    case "prev":
                        currpage--;
                        break;
                    case "1":
                        if (currpage < 3) currpage = 1;
                        else if (currpage > countpages) currpage = countpages - 4;
                        else currpage -= 2;
                        break;
                    case "2":
                        if (currpage < 3) currpage = 2;
                        else if (currpage > countpages) currpage = countpages - 3;
                        else currpage -= 1;
                        break;
                    case "3":
                        if (currpage < 3) currpage = 3;
                        else if (currpage > countpages) currpage = countpages - 2;
                        break;
                    case "4":
                        if (currpage < 3) currpage = 4;
                        else if (currpage > countpages) currpage = countpages - 1;
                        else currpage++;
                        break;
                    case "5":
                        if (currpage < 3) currpage = 5;
                        else if (currpage > countpages) currpage = countpages;
                        else currpage += 2;
                        break;
                    case "next":
                        currpage++;
                        break;
                    default:
                        currpage = 1;
                        break;
                }

                if (currpage < 1) currpage = 1;
                if (currpage >= countpages) currpage = countpages;

                if (currpage < 3)
                {
                    txt1.Text = " 1 ";
                    txt2.Text = " 2 ";
                    txt3.Text = " 3 ";
                    txt4.Text = " 4 ";
                    txt5.Text = " 5 ";
                }
                else if (currpage > countpages - 2)
                {
                    txt1.Text = " " + (countpages - 4).ToString() + " ";
                    txt2.Text = " " + (countpages - 3).ToString() + " ";
                    txt3.Text = " " + (countpages - 2).ToString() + " ";
                    txt4.Text = " " + (countpages - 1).ToString() + " ";
                    txt5.Text = " " + (countpages).ToString() + " ";

                }
                else
                {
                    txt1.Text = " " + (currpage - 2).ToString() + " ";
                    txt2.Text = " " + (currpage - 1).ToString() + " ";
                    txt3.Text = " " + (currpage).ToString() + " ";
                    txt4.Text = " " + (currpage + 1).ToString() + " ";
                    txt5.Text = " " + (currpage + 2).ToString() + " ";

                }
                txtCurentPage.Text = "Текущая страница: " + (currpage).ToString();

                l1 = users.Skip(currpage * countzapis - countzapis).Take(countzapis).ToList();
                lbUsersList.ItemsSource = l1;
            }
            catch { }
        }
        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtPageCount.Text == "")
                { l1 = users.ToList(); }
                else
                    l1 = users.Take(Convert.ToInt32(txtPageCount.Text)).ToList();
                lbUsersList.ItemsSource = l1;
            }
            catch { }
        }

    }
}
