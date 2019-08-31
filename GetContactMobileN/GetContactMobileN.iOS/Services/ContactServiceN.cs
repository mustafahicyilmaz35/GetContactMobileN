using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts;
using Foundation;
using GetContactMobileN.Interfaces;
using GetContactMobileN.iOS.Services;
using GetContactMobileN.Models;
using UIKit;

[assembly:Xamarin.Forms.Dependency(typeof(ContactServiceN))]
namespace GetContactMobileN.iOS.Services
{
    public class ContactServiceN : IContactService
    {
        public List<Contact> GetContactList()
        {
            var keysToFetch = new[] { CNContactKey.GivenName, CNContactKey.FamilyName, CNContactKey.PhoneNumbers, CNContactKey.EmailAddresses };
            NSError error;
            var contactList = new List<CNContact>();

            using (var store = new CNContactStore())
            {
                var allContainers = store.GetContainers(null, out error);
                foreach (var container in allContainers)
                {
                    try
                    {
                        using (var predicate = CNContact.GetPredicateForContactsInContainer(container.Identifier))
                        {
                            var containerResults = store.GetUnifiedContacts(predicate, keysToFetch, out error);
                            contactList.AddRange(containerResults);
                        }
                    }
                    catch (Exception)
                    {
                        // ignore missed contacts from errors
                        throw;
                    }
                }
            }
            var contacts = new List<Contact>();

           
            foreach (var item in contactList)
            {
                var numbers = item.PhoneNumbers;
                if (numbers != null)
                {
                    foreach (var item2 in numbers)
                    {
                        
                        
                            contacts.Add(new Contact
                            {
                                Name = item.GivenName,
                                Number = item2.Value.StringValue,
                                
                            });
                        
                    }

                }
            }
            return contacts;
        }

        public Task<List<Contact>> GetContactListAsync()
        {
            return Task.Run(() => GetContactList());
        }
        
    }
}