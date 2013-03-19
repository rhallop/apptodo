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
    public partial class TodoDetails : PhoneApplicationPage
    {
        public TodoDetails()
        {
            InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    todo.DataContext = App.TodoViewModel.findTodoById(index);
                    //ictodo.ItemsSource = App.TodoViewModel.listTodo[index];
                }
            }
        }
    }
}