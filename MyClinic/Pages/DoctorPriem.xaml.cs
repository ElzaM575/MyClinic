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
using MyClinic.Windows;
namespace MyClinic.Pages

{
    /// <summary>
    /// Логика взаимодействия для DoctorPriem.xaml
    /// </summary>
    public partial class DoctorPriem : Page
    {
     
        public static List<Reception> receptions { get; set; }
        public static List<Animals> animals { get; set; }
    
        public DoctorPriem()
        {
            InitializeComponent();
        
            receptions = new List<Reception>(DbVetClinica.vet.Reception.Where(i=>i.IsDelete==false).ToList());
            this.DataContext = this;

        }

        private void FiltrDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {

                
                

            }
        }

        private void AddPriem_Click(object sender, RoutedEventArgs e)
        {
           AddPriem addPriem = new AddPriem();
            addPriem.Show();
        }

        private void TicketSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TicketSearchTb.Text.Trim();
            if (search == " ")
                DoctorsLv.ItemsSource = receptions.ToList();
            else
                DoctorsLv.ItemsSource = receptions.Where(i => i.Animals.clinic.ToString() == search).ToList();
        }
       
    }
}
