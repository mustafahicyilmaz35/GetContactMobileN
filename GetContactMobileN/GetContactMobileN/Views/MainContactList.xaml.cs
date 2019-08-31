using GetContactMobileN.Interfaces;
using GetContactMobileN.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetContactMobileN.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainContactList : ContentPage
    {
        public MainContactList()
        {
            InitializeComponent();
            lst_Contacts.ItemsSource = DependencyService.Get<IContactService>().GetContactList();
        }
       
    }

       
}