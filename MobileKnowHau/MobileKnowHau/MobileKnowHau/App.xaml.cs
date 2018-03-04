using Plugin.LocalNotifications.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;


namespace MobileKnowHau
{
    public partial class App : Application
    {
        public App()
        {
            // InitializeComponent();
            
            /*ILocalNotifications localNotifications = DependencyService.Get<ILocalNotifications>();
            Button showNotificationButton = new Button();
            showNotificationButton.Text = "show local notification";
            showNotificationButton.Clicked += (sender, e) => localNotifications.Show("teste", "local notif alert", 1);
            Button cancelNotification = new Button();
            cancelNotification.Text = "cancel not";
            cancelNotification.Clicked += (sender, e) => localNotifications.Cancel(1);*/
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
       

    }
}
