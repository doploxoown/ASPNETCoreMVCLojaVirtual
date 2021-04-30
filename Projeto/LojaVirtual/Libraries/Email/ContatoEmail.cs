using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato pContato)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("lojavirtualdoplox@gmail.com", ""),
                EnableSsl = true
            };

            string corpoMsg = string.Format("<h2>Contato - Loja Virtual</h2><br/>" +
                "<b>Nome: <b/>{0} <br/>" +
                "<b>E-mail: <b/>{1} <br/>" +
                "<b>Texto: <b/>{2}<br/>" +
                "E-mail enviado automaticamento do site Loja Virtual.",
                pContato.Nome,
                pContato.Email,
                pContato.Texto                
                );

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("lojavirtualdoplox@gmail.com");
            mensagem.To.Add("thi.mininel@gmail.com");
            mensagem.Subject = "Contato - Loja Virtual - Email: " + pContato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            smtp.Send(mensagem);
        }
    }
}
