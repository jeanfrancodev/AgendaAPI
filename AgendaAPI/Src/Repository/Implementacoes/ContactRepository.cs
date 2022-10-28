using AgendaAPI.Src.Dtos.ContactDto;
using AgendaAPI.Src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaAPI.Src.Repository.Implementacoes
{
    public class ContactRepository : IContactRepository
    {
        #region Atributos
        private readonly AgendaContext _context;
        #endregion

        #region Construtores
        public ContactRepository(AgendaContext context)
        {
            _context = context;
        }
        #endregion
        #region Métodos

        public async Task<List<Contact>> ListContacts()
        {
            return await _context.Contact.ToListAsync();
        }

        public async Task<Contact> GetByPhone(string phone)
        {
           return await _context.Contact.FirstOrDefaultAsync(e => e.Phone == phone);
        }

        public async Task<Contact> GetById(int id)
        {
            return await _context.Contact.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(Contact contact)
       {
            await _context.Contact.AddAsync(contact);
            await _context.SaveChangesAsync();
       }
             
        public async Task Update (Contact contact)
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Contact>> Search(string filterString)
        {
            return await _context.Contact
                .Where(e => filterString == null ||
                    (
                        e.Phone.Contains(filterString) ||
                        e.Email.Contains(filterString) ||
                        e.Company.Contains(filterString) ||
                        e.LastName.Contains(filterString) ||
                        e.Nickname.Contains(filterString) ||
                        e.FirstName.Contains(filterString)
                    )
                )
                .ToListAsync();
        }
        public async Task Delete(Contact contact)
        {
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
