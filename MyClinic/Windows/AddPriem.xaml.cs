using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyClinic.Connection;

namespace MyClinic.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddPriem.xaml
    /// </summary>
    public partial class AddPriem : Window
    {
        public static List<Animals> animals { get; set; }
        public static List<Reception> receptions { get; set; }
        public AddPriem()
        {
            InitializeComponent();
             animals= new List<Animals>(DbVetClinica.vet.Animals.ToList());
            receptions = new List<Reception>(DbVetClinica.vet.Reception.Where(i => i.IsDelete == false).ToList());
            this.DataContext = this;
        }

        private void SavePriemBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Reception res = new Reception();
            res.Date_admission = DateDp.SelectedDate;

            Animals animals = new Animals();
            animals.clinic = ClCm.Text.Trim();
            res.Comment = KomTb.Text.Trim();
            res.IsDelete = false;
            DbVetClinica.vet.Reception.Add(res);
            DbVetClinica.vet.SaveChanges();
       
            MessageBox.Show("Прием успешно добавлен.");
            Close();
        }

        private void AuthorCm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddClBtn_Click(object sender, RoutedEventArgs e)
        {
            AddAnimals1 addAuthorWindow = new AddAnimals1();
            addAuthorWindow.Show();
        }

        private void UddClBtn_Click(object sender, RoutedEventArgs e)
        {
            ClCm.ItemsSource = new List<Animals>(DbVetClinica.vet.Animals.ToList());
          
        }
    }
}
