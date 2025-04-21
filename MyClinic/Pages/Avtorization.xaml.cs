using MyClinic.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Avtorization.xaml
    /// </summary>
    public partial class Avtorization : Page
    {
        public Avtorization()
        {
            InitializeComponent();
            InitializeComponent();

        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTb.Text.Trim();
            string password = passwordTb.Password;

           
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ShowError("Введите ID врача и пароль");
                return;
            }

            if (!int.TryParse(login, out int doctorId))
            {
                ShowError("ID врача должен быть числом");
                return;
            }
            try
            {
                using (var vet = new VetClinicaEntities())
                {
              

                    
                    var doctor = vet.Doctor.FirstOrDefault(d => d.Id == doctorId);

                    if (doctor != null)
                    {
                        CurrentUser.Doctor = doctor;
                        NavigationService.Navigate(new DoctorPriem());
                    }
                    else
                    {
                        ShowError("Врач с указанным ID не найден");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка подключения: {ex.Message}");
            }
        }
        // Проверка, что вводится только число (для ID врача)
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ShowError(string message)
        {
            errorTb.Text = message;
            errorTb.Visibility = Visibility.Visible;
        }
    }
    }

    

