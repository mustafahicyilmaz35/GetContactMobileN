using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts;
using Foundation;
using GetContactMobileN.Interfaces;
using GetContactMobileN.iOS.Services;
using GetContactMobileN.Models;
using UIKit;

//[assembly:Xamarin.Forms.Dependency(typeof(ContactService))]
namespace GetContactMobileN.iOS.Services
{
    public class ContactService : IContactService
    {
        public List<Contact> GetContactList()
        {
            var response = new List<Contact>();
            try
            {
                //We can specify the properties that we need to fetch from contacts  
                var keysToFetch = new[] {
            CNContactKey.PhoneNumbers, CNContactKey.GivenName, CNContactKey.FamilyName, CNContactKey.EmailAddresses
        };
                //Get the collections of containers  
                var containerId = new CNContactStore().DefaultContainerIdentifier;
                //Fetch the contacts from containers  
                using (var predicate = CNContact.GetPredicateForContactsInContainer(containerId))
                {
                    CNContact[] contactList;
                    using (var store = new CNContactStore())
                    {
                        contactList = store.GetUnifiedContacts(predicate, keysToFetch, out
                            var error);
                    }
                    //Assign the contact details to our view model objects  
                    response.AddRange(from item in contactList
                                      where item?.EmailAddresses != null
                                      select new Contact
                                      {
                                          Numbers = item.PhoneNumbers,
                                          Number = item.PhoneNumbers.LastOrDefault().ToString(),
                                          Name = item.GivenName,
                                          Emails = item.EmailAddresses.Select(m => m.Value.ToString()).ToList(),
                                          Email = item.EmailAddresses.LastOrDefault().ToString()
                                          
                                      });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return response;
        }

        public Task<List<Contact>> GetContactListAsync()
        {
            return Task.Run(() => GetContactList());
        }
    }
}

