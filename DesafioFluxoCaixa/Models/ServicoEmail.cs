using DesafioFluxoCaixa.Models.Pessoas;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioFluxoCaixa.Models
{
    public class ServicoEmail
    {
        private IConfiguration configuration;

        public ServicoEmail(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void EnviarEmail(string[] destinatario, string assunto, string userName, string aviso)
        {
            Mensagem mensagem = new(destinatario, assunto, userName, aviso);
            var mensagemEmail = CriarCorpoEmail(mensagem);
            Enviar(mensagemEmail);
        }

        private void Enviar(MimeMessage mensagemEmail)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(configuration.GetValue<string>("EmailSettings:SmtpServer"),
                        configuration.GetValue<int>("EmailSettings:Port"), false);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(configuration.GetValue<string>("EmailSettings:User"),
                        configuration.GetValue<string>("EmailSettings:Password"));
                    client.Send(mensagemEmail);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        public MimeMessage CriarCorpoEmail(Mensagem msg)
        {
            var mensagemEmail = new MimeMessage();
            mensagemEmail.From.Add(new MailboxAddress("Desafio - Fluxo De Caixa",
                configuration.GetValue<string>("EmailSettings:From")));
            mensagemEmail.To.AddRange(msg.Destinatario);
            mensagemEmail.Subject = msg.Assunto;
            mensagemEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = msg.Conteudo
            };

            return mensagemEmail;
        }

    }
}
