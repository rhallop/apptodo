using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TodoApp.Resources;
using System.Windows.Media;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Scheduler;

namespace TodoApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            /*
            Reminder rem = new Reminder("Tere");
            rem.Content = "Sisu";
            rem.BeginTime = DateTime.Now.AddSeconds(5);
            ScheduledActionService.Add(rem);
            */

            FlipTileData primaryTileData = new FlipTileData();
            primaryTileData.Count = App.TodoViewModel.countByDeadlines(DateTime.Now);

            ShellTile primaryTile = ShellTile.ActiveTiles.First();
            primaryTile.Update(primaryTileData);

            InitializeComponent();
            DataContext = App.TodoViewModel;

            lbtodos.ItemsSource = App.TodoViewModel.todosNotDone();


            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void lbtodos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbtodos.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/TodoDetails.xaml?selectedItem=" + ((Todo)lbtodos.SelectedItem).Id, UriKind.Relative));
            
            // Reset selected item to null (no selection)
            lbtodos.SelectedItem = null;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TodoAdd.xaml", UriKind.Relative));
        }

        private void Mark_Click(object sender, RoutedEventArgs e)
        {
            int index = lbtodos.Items.IndexOf((sender as MenuItem).DataContext);
            //App.TodoViewModel.listTodo[index].IsDone = true;
            App.TodoViewModel.markTodoAsDone(index);
            lbtodos.ItemsSource = App.TodoViewModel.todosNotDone();
        }
    
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //int index = lbtodos.Items.IndexOf((sender as MenuItem).DataContext);
            int index = ((Todo)(sender as MenuItem).DataContext).Id;
            NavigationService.Navigate(new Uri("/TodoModify.xaml?selectedItem=" + index, UriKind.Relative));
            //MessageBox.Show(lbtodos.SelectedIndex.ToString());
        }

        private void Completed_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Completed.xaml", UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}