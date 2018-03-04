using MobileKnowHau.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileKnowHau
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Definicoes : ContentPage
    {
        String fichPermi = "ficheiroPermi";
        bool isToggled;
        public Definicoes()
        {
            InitializeComponent();
            //Help.Text = "<b>Register new user</b>\n  This option allows the registration of a user in form that he can enter the aplication.Some personal data are required in the registration \nLogin - Entry form on the application.It's necessary to enter registered credentials \nHistory- Here you can see your content history, and you can select a certain content as favorite or visualize it's informations or delete it from the history \nFavorites- Check the information of the contents selected as favorites";
            var s = new FormattedString();
            s.Spans.Add(new Span { Text = "Register new User:", FontAttributes = FontAttributes.Bold });
            s.Spans.Add(new Span { Text = "\nThis option allows the registration of a user in form that he can enter the aplication.Some personal data are required in the registration" });
            s.Spans.Add(new Span { Text = "\n\nLogin:", FontAttributes = FontAttributes.Bold });
            s.Spans.Add(new Span { Text = "\nEntry form on the application.It's necessary to enter registered credentials" });
            s.Spans.Add(new Span { Text = "\n\nHistory:", FontAttributes = FontAttributes.Bold });
            s.Spans.Add(new Span { Text = "\nHere you can see your content history, and you can select a certain content as favorite or visualize it's informations or delete it from the history" });
            s.Spans.Add(new Span { Text = "\n\nFavorites:", FontAttributes = FontAttributes.Bold });
            s.Spans.Add(new Span { Text = "\nCheck the information of the contents selected as favorites" });
            Help.FormattedText = s;
        }



        private async void LogOff()
        {
            await PCLHelper.DeleteFile("User.txt");
            CoreApplication.Exit();
        }
    }
}