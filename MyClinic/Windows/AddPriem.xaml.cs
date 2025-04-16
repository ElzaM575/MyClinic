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
    /// Логика взаимодействия для AddPriem.xaml
    /// </summary>
    public partial class AddPriem : Window
    {
        private List<Animals> animalsList;
        public AddPriem()
        {
            InitializeComponent();
             animalsList= new List<Animals>(DbVetClinica.vet.Animals.ToList());
            this.DataContext = this;
        }

        private void SavePriemBtn_Click(object sender, RoutedEventArgs e)
        {
            Reception res = new Reception();
            res.Date_admission = DateDp.SelectedDate;
            Animals animals = new Animals();
            animals.clinic = AuthorCm.Text.Trim();
            res.Comment = KomTb.Text.Trim();
            //reader.Birthdate = BirthDateDp.SelectedDate;
            //reader.Phone = PhoneTb.Text.Trim();
            res.IsDelete = false;
            DbVetClinica.vet.Reception.Add(res);
            DbVetClinica.vet.SaveChanges();
            //Connection.biblioteq.SaveChanges();
            MessageBox.Show("Прием успешно добавлен.");
            Close();
        }

        private void AuthorCm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddClBtn_Click(object sender, RoutedEventArgs e)
        {
            AddAnimls addAuthorWindow = new AddAnimls();
            addAuthorWindow.Show();
        }
    }
}
