using GetContactMobileN.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GetContactMobileN.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetContactListAsync();
        List<Contact> GetContactList();
    }
}
