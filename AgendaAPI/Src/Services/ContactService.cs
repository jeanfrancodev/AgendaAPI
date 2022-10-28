using AgendaAPI.Src.Dtos.ContactDto;
using AgendaAPI.Src.Exceptions;
using AgendaAPI.Src.Models;
using AgendaAPI.Src.Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AgendaAPI.Src.Services
{
    public class ContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;

        }

        public async Task<List<Contact>> ListContacts()
        {
            List<Contact> contacts = await _contactRepository.ListContacts();

            return contacts;
        }

        public async Task Create(ContactDto dto)
        {
            Regex regexToOnlyNumbers = new(@"^\d+$"); // Regex para validar números do inicio ao fim
            bool isNumberValid = regexToOnlyNumbers.IsMatch(dto.Phone);

            if (string.IsNullOrWhiteSpace(dto.Phone) || isNumberValid == false)
            {
                throw new ErrorException("Número inválido");
            }

            // Consultar no banco se já tem esse número cadastrado
            Contact contact = await _contactRepository.GetByPhone(dto.Phone);

            if (contact != null)
            {
                throw new ErrorException("Este contato já existe em sua agenda!");
            }

            Contact newContact = new()
            {
                Company = dto.Company,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Nickname = dto.Nickname,
                Phone = dto.Phone
            };

            await _contactRepository.Create(newContact);
        }

        public async Task Delete(int id)
        {

            Contact contact = await _contactRepository.GetById(id);

            if (contact == null)
            {
                throw new Exception("Contato inexistente!");
            }
           await _contactRepository.Delete(contact);
        }

        public async Task Update(ContactDto contactInfo, int id)
        {
            Contact contact = await _contactRepository.GetById(id);

            if (contact == null)
            {
                throw new Exception("Contato inexistente!");
            }

            Regex regexToOnlyNumbers = new(@"^\d+$"); // Regex para validar números do inicio ao fim
            bool isNumberValid = regexToOnlyNumbers.IsMatch(contactInfo.Phone);

            if (string.IsNullOrWhiteSpace(contactInfo.Phone) || isNumberValid == false)
            {
                throw new ErrorException("Número inválido");
            }

            // Consultar no banco se já tem esse número cadastrado
            Contact newContactNumberExists = await _contactRepository.GetByPhone(contactInfo.Phone);

            if (newContactNumberExists != null)
            {
                throw new ErrorException("Este contato já existe em sua agenda!");
            }

            var aux = contact;
            aux.FirstName = contactInfo.FirstName;
            aux.LastName = contactInfo.LastName;
            aux.Nickname = contactInfo.Nickname;
            aux.Phone = contactInfo.Phone;
            aux.Email = contactInfo.Email;
            aux.Company = contactInfo.Company;

            await _contactRepository.Update(aux);
        }

        public async Task<List<Contact>> Search(string filterString)
        {
            List<Contact> contacts = await _contactRepository.Search(filterString);

            return contacts;
        }
    }
}

