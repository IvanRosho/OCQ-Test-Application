using System.Windows;

namespace OCQ_Test_Application
{
    /// <summary>
    /// Interaction logic for PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        public PersonWindow()
        {
            InitializeComponent();
            Closing += (DataContext as ViewModels.PersonViewModel).OnWindowClosing;
        }
    }
}
