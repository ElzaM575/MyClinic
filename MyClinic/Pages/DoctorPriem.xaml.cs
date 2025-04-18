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
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace MyClinic.Pages

{
    /// <summary>
    /// Логика взаимодействия для DoctorPriem.xaml
    /// </summary>
    public partial class DoctorPriem : Page
    {

        public static ObservableCollection <Reception> receptions { get; set; }
        public static List<Animals> animals { get; set; }


        public DoctorPriem()
        {
            InitializeComponent();
            animals = new List<Animals>(DbVetClinica.vet.Animals.ToList());
            receptions = new ObservableCollection<Reception>(DbVetClinica.vet.Reception.Where(i => i.IsDelete == false).ToList());
            this.DataContext = this;
            FiltrDate.ItemsSource = receptions
          .Select(r => r.Date_admission)
          .Distinct()
          .OrderBy(d => d)
          .ToList();
            if (CurrentUser.Doctor == null)
            {
                MessageBox.Show("Доступ запрещен. Требуется авторизация.");
                NavigationService.Navigate(new Avtorization());
                return;
            }

            // Загрузка данных
            
            DisplayDoctorInfo();
        }
        private void DisplayDoctorInfo()
        {
            if (CurrentUser.Doctor != null)
            {
                doctorInfoTb.Text = $"Врач: {CurrentUser.Doctor.LastName} {CurrentUser.Doctor.Name[0]}. {CurrentUser.Doctor.SerName[0]}.";
            }
        }
        private void FiltrDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FiltrDate.SelectedItem is DateTime selectedDate)
            {
                DoctorsLv.ItemsSource = receptions
                    .Where(i => i.Date_admission == selectedDate.Date)
                    .ToList();
            }
            else
            {
                DoctorsLv.ItemsSource = receptions.ToList();
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


        private void UpdatePriem_Click(object sender, RoutedEventArgs e)
        {

            DoctorsLv.ItemsSource = new List<Reception>(DbVetClinica.vet.Reception.Where(i => i.IsDelete == false).ToList());

        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {

            FiltrDate.SelectedItem = null;
            DoctorsLv.ItemsSource = receptions.ToList();
        }
        private Reception _lastDeletedReception;
        private void DPriem_Click(object sender, RoutedEventArgs e)
        {
            if (_lastDeletedReception != null)
            {
                try
                {
                    // Восстанавливаем прием
                    DbVetClinica.vet.Reception.Add(_lastDeletedReception);
                    DbVetClinica.vet.SaveChanges();

                    // Обновляем список
                    UpdatePriem_Click(null, null);



                    MessageBox.Show("Удаление отменено. Прием восстановлен.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при отмене удаления: {ex.Message}");
                }
            }
        }

        private void Upriem_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorsLv.SelectedItem is Reception selectedReception)
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить прием от {selectedReception.Date_admission:dd.MM.yyyy}?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Сохраняем удаленный прием для возможного восстановления
                        _lastDeletedReception = selectedReception;

                        // Вариант 1: Мягкое удаление (рекомендуется)
                        selectedReception.IsDelete = true;

                        // Вариант 2: Физическое удаление (раскомментировать если нужно)
                        // DbVetClinica.vet.Reception.Remove(selectedReception);

                        DbVetClinica.vet.SaveChanges();
                        // Обновляем список
                        UpdatePriem_Click(null, null);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите прием для удаления!");
            }
        }

        private void Vh_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Avtorization());
        }



        private void RedPriem_Click_1(object sender, RoutedEventArgs e)
        {
            if (DoctorsLv.SelectedItem is Reception selectedReception)
            {
                var editWindows = new EditPriem(selectedReception);
               
                if (editWindows.ShowDialog() == true)
                {
                    // Полностью перезагружаем данные из базы
                    receptions = new ObservableCollection<Reception>(
                        DbVetClinica.vet.Reception
                            .Include(r => r.Animals)
                            .Where(i => i.IsDelete == false)
                            .ToList());
                    DoctorsLv.ItemsSource = receptions;
                    // Обновляем ListView
                    //DoctorsLv.ItemsSource = receptions;

                    // Обновляем фильтры, если они активны
                    if (FiltrDate.SelectedItem != null)
                        FiltrDate_SelectionChanged(null, null);
                    if (!string.IsNullOrWhiteSpace(TicketSearchTb.Text))
                        TicketSearchTb_TextChanged(null, null);

                    MessageBox.Show("Прием успешно изменен");
                }
            }
            else
            {
                MessageBox.Show("Выберите прием для редактирования");
            }
        }

        private void GivPriem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Anim());
        }
    }
}
