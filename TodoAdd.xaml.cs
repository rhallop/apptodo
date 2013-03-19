using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TodoApp
{
    public partial class TodoAdd : PhoneApplicationPage
    {
        public TodoAdd()
        {
            InitializeComponent();
            string[] categorys = { "Informaatika", "Matemaatika", "Lineaaralgebra", "Füüsika", "Keemia" };
            lpicker.ItemsSource = categorys;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DateTime todoDate = dpicker.Value.Value;
            DateTime todoTime = tpicker.Value.Value;
            DateTime deadline = new DateTime(todoDate.Year, todoDate.Month, todoDate.Day, todoTime.Hour, todoTime.Minute, todoTime.Second); 

            App.TodoViewModel.SaveTodo(new Todo{Title = tbtitle.Text, Content = tbcontent.Text, IsDone = false, Created = DateTime.Now, Deadline = deadline, Category = lpicker.SelectedItem.ToString()});

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}