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
using System.Data.Entity;


namespace MyClinic.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditPriem.xaml
    /// </summary>
    public partial class EditPriem : Window
    {
        public static Reception reception1 = new Reception();
        //private VetClinicaEntities vet = new VetClinicaEntities();
        public EditPriem(Reception reception)
        {
            InitializeComponent();
            reception1= reception;
           
            reception = DbVetClinica.vet.Reception
                .Include(r => r.Animals)
                .FirstOrDefault(r => r.Id == reception.Id);
            DatePicker.SelectedDate = reception.Date_admission;
            ClinicTb.Text = reception.Animals?.clinic ?? "";
            CommentTb.Text = reception.Comment;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Укажите дату приема");
                return;
            }
            if (string.IsNullOrWhiteSpace(ClinicTb.Text))
            {
                MessageBox.Show("Введите кличку животного");
                return;
            }
            try
            {
               
                reception1.Date_admission = DatePicker.SelectedDate.Value;

                if (reception1.Animals != null)
                {
                    reception1.Animals.clinic = ClinicTb.Text;
                }
                else
                {
                    
                    reception1.Animals = new Animals { clinic = ClinicTb.Text };
                }

                reception1.Comment = CommentTb.Text;
               
                DbVetClinica.vet.SaveChanges();

                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }

        }
    }
}
