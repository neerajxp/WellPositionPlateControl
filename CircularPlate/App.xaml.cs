using System.Windows;

namespace CircularPlate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Window1 m1 = new Window1();
            MainWindow m1 = new MainWindow();
            m1.Show();
        }
    }
}