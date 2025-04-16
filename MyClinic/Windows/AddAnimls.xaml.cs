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
    /// Логика взаимодействия для AddAnimls.xaml
    /// </summary>
    public partial class AddAnimls : Window
    {
        public static List<Animals> animals { get; set; }
        public AddAnimls()
        {
            InitializeComponent();
            animals = new List<Animals>(DbVetClinica.vet.Animals.ToList());
            this.DataContext = this;
        }
    }
}
