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

namespace ScheduleUI
{
    /// <summary>
    /// Interaction logic for NewMemberDialog.xaml
    /// </summary>
    public partial class NewMemberDialog : Window
    {
        public NewMemberDialog()
        {
            InitializeComponent();
        }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
      this.Close();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {

    }
  }
}
