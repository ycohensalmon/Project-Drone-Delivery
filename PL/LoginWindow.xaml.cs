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

namespace PL
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        BlApi.IBL myBl;
        public LoginWindow()
        {
            myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (UserNameTextBox.Text == "admin" && PasswordBox.Password == "123")
            {
                new StationsListWindow().Show();
                Close();
            }
            else
                WrongPassword.Text = "username or password are incorrect";
        }

        private void UserNameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            WrongPassword.Text = "";
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            WrongPassword.Text = "";
        }

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
