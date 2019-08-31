using GetContactMobileN.Extensions;
using GetContactMobileN.Interfaces;
using GetContactMobileN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace GetContactMobileN.ViewModels
{
    public class ContactViewModel
    {
        public IList<Contact> Items { get; private set; }
        public int ItemsCount { get; private set; }
        public List<ObservableGroupCollections<string, Contact>> GroupData { get; set; }
        public string MyNumber { get; set; } = "0532 686 12 05";

        public ContactViewModel()
        {
            var rawitems = DependencyService.Get<IContactService>().GetContactList().ToList();
            Items = rawitems.OrderBy(c => c.Name).ToList();
            GroupData = Items.OrderBy(p => p.Name).GroupBy(p => p.Name[0].ToString()).Select(p => new ObservableGroupCollections<string, Contact>(p)).ToList();
            ItemsCount = rawitems.Count;
            
            
           
        }
    }
}
