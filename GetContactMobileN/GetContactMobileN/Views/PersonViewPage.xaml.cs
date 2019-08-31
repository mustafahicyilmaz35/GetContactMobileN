using GetContactMobileN.Models;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;
using Xamarin.Forms.Xaml;

namespace GetContactMobileN.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonViewPage : ContentPage
    {
        public PersonViewPage()
        {
            InitializeComponent();
        }

        void onEmailClicked(object sender, EventArgs e)
        {
            var sendMail = CrossMessaging.Current.EmailMessenger;
            if(sendMail.CanSendEmail)
            {
                if(Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                {
                    if(sendMail.CanSendEmailBodyAsHtml || sendMail.CanSendEmailAttachments)
                    {
                        var emailWithHtmlBody = new EmailMessageBuilder().To((BindingContext as Contact).Email).Subject(entry_Subject.Text).Body(entry_message.Text).Build();
                        sendMail.SendEmail(emailWithHtmlBody);
                    }
                }
            }
        }

        async void onSendSMSClicked(object sender, EventArgs e)
        {
            await SendSMS(entry_message.Text,(BindingContext as Contact).Number);
        }

        void onWhatsappClicked(object sender, EventArgs e)
        {
            try
            {
                Chat.Open((BindingContext as Contact).Number, entry_message.Text);
            }
            catch (Exception ex)
            {

                DisplayAlert("Hata", "Whatsapp Uygulaması Bulunamadı.", "OK");
            }
        }



        public async Task SendSMS(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {

                await DisplayAlert("Hata", "Cihazda SMS özelliği yok", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Mesaj Gönderilemedi", "OK");
            }
        }
    }
}