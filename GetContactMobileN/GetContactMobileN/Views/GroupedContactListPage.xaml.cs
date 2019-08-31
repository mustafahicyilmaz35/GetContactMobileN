using GetContactMobileN.Models;
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
    public partial class GroupedContactListPage : ContentPage
    {
        public GroupedContactListPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lst_Group_Contact.BindingContext = new ContactViewModel();
        }

        void onItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                Navigation.PushAsync(new PersonViewPage {

                    BindingContext = e.SelectedItem as Contact

                });
            }
        }

        void onSearchPressed(object sender, EventArgs e)
        {

            var names = (BindingContext as ContactViewModel).GroupData;
            var keyWord = searh_NameListView.Text;
            

        }
    }
}