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
using System.Windows.Shapes;
using MyClinic.Connection;

namespace MyClinic.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddAnimals1.xaml
    /// </summary>
    public partial class AddAnimals1 : Window
    {
        public static List<Animals> animals { get; set; }
        public static List<Type_Animals> type_Animals { get; set; }
        public static List<Gender> genders { get; set; }

        public AddAnimals1()
        {
            InitializeComponent();
            animals = new List<Animals>(DbVetClinica.vet.Animals.ToList());
            type_Animals = new List<Type_Animals>(DbVetClinica.vet.Type_Animals.ToList());
            genders = new List<Gender>(DbVetClinica.vet.Gender.ToList());
            this.DataContext = this;


        }

        

        private void SaveBook_Click(object sender, RoutedEventArgs e)
        {
            Animals book = new Animals();
            if (СlinicTb.Text != null && PolCm.SelectedItem != null &&
                RostTb.Text != null && VidCm.SelectedItem != null &&
                VesTb.Text != null  
                   )
            {
             
                book.clinic = (СlinicTb.Text.Trim());
                book.Id_Gender = (PolCm.SelectedItem as Gender).Id;
                book.Id_TypeG = (VidCm.SelectedItem as Type_Animals).Id;
                book.Whole = (VesTb.Text.Trim());
                book.Height = (RostTb.Text.Trim());
                
                DbVetClinica.vet.Animals.Add(book);
                DbVetClinica.vet.SaveChanges();
                MessageBox.Show("Успешно добавлен любимчик!");
                Close();
            }
        }
    }
}
