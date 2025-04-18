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
using MyClinic.Connection;

namespace MyClinic.Pages
{
    /// <summary>
    /// Логика взаимодействия для Anim.xaml
    /// </summary>
    public partial class Anim : Page
    {
        public static List<Animals> animals { get; set; }
       
        public Anim()
        {
            
            InitializeComponent();
            animals = new List<Animals>(DbVetClinica.vet.Animals.ToList());
           
            this.DataContext = this;
        }

        private void Vh_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DoctorPriem());
        }

        private void TicketSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TicketSearchTb.Text.Trim().ToLower(); // Приводим к нижнему регистру
            if (string.IsNullOrWhiteSpace(search))
            {
                // Если строка поиска пустая, показываем всех животных
                AnimLv.ItemsSource = animals;
            }
            else
            {
                // Ищем по частичному совпадению клички (clinic)
                AnimLv.ItemsSource = animals
                    .Where(a => a.clinic != null &&
                                a.clinic.ToLower().Contains(search))
                    .ToList();
            }
        }
    }
}
