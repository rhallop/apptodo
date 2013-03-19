using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Scheduler;

namespace TodoApp
{
    public partial class TodoModify : PhoneApplicationPage
    {
        public int todoid = -1;

        public TodoModify()
        {
            InitializeComponent();
            string[] categorys = { "Informaatika", "Matemaatika", "Lineaaralgebra", "Füüsika", "Keemia" };
            lpicker.ItemsSource = categorys;
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    todoid = int.Parse(selectedIndex);
                    todo.DataContext = App.TodoViewModel.findTodoById(todoid);
                    lpicker.SelectedItem = App.TodoViewModel.findTodoById(todoid).Category;
                }
            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DateTime todoDate = dpicker.Value.Value;
            DateTime todoTime = tpicker.Value.Value;
            DateTime deadline = new DateTime(todoDate.Year, todoDate.Month, todoDate.Day, todoTime.Hour, todoTime.Minute, todoTime.Second);

            App.TodoViewModel.findTodoById(todoid).Title = tbtitle.Text;
            App.TodoViewModel.findTodoById(todoid).Content = tbcontent.Text;
            App.TodoViewModel.findTodoById(todoid).Deadline = deadline;
            App.TodoViewModel.findTodoById(todoid).Category = lpicker.SelectedItem.ToString();

            ScheduledActionService.Remove(App.TodoViewModel.findTodoById(todoid).Title);
            Reminder rem = new Reminder(App.TodoViewModel.findTodoById(todoid).Title);
            rem.BeginTime = deadline;
            ScheduledActionService.Add(rem);

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}