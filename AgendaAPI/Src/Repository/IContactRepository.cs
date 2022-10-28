using AgendaAPI.Src.Dtos.ContactDto;
using AgendaAPI.Src.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaAPI.Src.Repository
{

    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de Contact</para>
    /// <para>Criado por: Jean Oliveira Franco</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 25/10/2022</para>
    /// </summary>
    public interface IContactRepository
    {
        Task<List<Contact>> ListContacts();

        Task<List<Contact>> Search(string filterString);

        Task Update(Contact contact);

        Task Create(Contact contact);

        Task Delete(Contact contact);

        Task<Contact> GetByPhone(string Phone);

        Task<Contact> GetById(int Id);

    }
}
