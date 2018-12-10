using System.Windows;

namespace ScheduleUI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      NewMemberDialog dialog = new NewMemberDialog();
      bool? result = dialog.ShowDialog();
      int t = 0;
      if (result == true)
        t = 1;
      else
        t = 2; 
    }
  }
}
