using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace DesafioFluxoCaixa.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatario, string assunto, string userName, string aviso)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(userName,d)));

            Assunto = assunto;
            Conteudo = aviso;

        }
    }
}