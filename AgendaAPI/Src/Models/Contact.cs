namespace AgendaAPI.Src.Models
{
    
/// <summary>
/// <para>Resumo: Classe responsavel por representar Contact no banco.</para>
/// <para>Criado por: Jean Oliveira Franco</para>
/// <para>Versão: 1.0</para>
/// <para>Data: 25/10/2022</para>
/// </summary>
    public class Contact
    {
        public int Id  { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
