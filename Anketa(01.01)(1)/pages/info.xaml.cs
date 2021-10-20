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

namespace Anketa_01._01__1_
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4(auth CurrentUser)
        {
            InitializeComponent();
            tbName.Content = CurrentUser.users.name;
            tbDR.Content = CurrentUser.users.dr.ToString("yyyy MMMM dd");
            tbGender.Content = CurrentUser.users.genders.gender;
            DataContext = DB.currentUser;
            List<users_to_traits> LUTT = DB.Base.users_to_traits.Where(x => x.id_user == CurrentUser.id).ToList();
            foreach (users_to_traits UT in LUTT)
            {
                lbInfo.Content += UT.traits.trait + "; ";
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            User.frmMain.GoBack();
        }
    }        
}
