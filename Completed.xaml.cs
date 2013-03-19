using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;

namespace TodoApp
{
    public partial class Completed : PhoneApplicationPage
    {
        public Completed()
        {
            InitializeComponent();

            lbtodos.ItemsSource = completedTodos();
        }

        private void lbtodos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/TodoDetails.xaml?selectedItem=" + ((Todo)lbtodos.SelectedItem).Id, UriKind.Relative));
        }

        public ObservableCollection<Todo> completedTodos()
        {
            ObservableCollection<Todo> temp = new ObservableCollection<Todo>();

            foreach (var t in App.TodoViewModel.listTodo)
            {
                if (t.IsDone == true)
                {
                    temp.Add(t);
                }
            }

            return temp;
        }
    }
}