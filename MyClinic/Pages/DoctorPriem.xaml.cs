using MyClinic.Connection;
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

namespace MyClinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для DoctorPriem.xaml
    /// </summary>
    public partial class DoctorPriem : Page
    {
     
        public static List<Reception> receptions { get; set; }
    
        public DoctorPriem()
        {
            InitializeComponent();
        
            receptions = new List<Reception>(DbVetClinica.vet.Reception.ToList());
            this.DataContext = receptions;
            
        }





        private void FiltrDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {

                var t = FiltrDate.SelectedItem as Reception;

                if (t.Id != -1)
                    DoctorsLv.ItemsSource = receptions.Where(i => i.Id_Doctor == t.Id).ToList();
                else
                    DoctorsLv.ItemsSource = receptions.ToList();

            }
        }

        private void AddPriem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
