using LibraryKodigos.KUtils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LibraryKodigos.KEmails
{
    public class KSendEmail
    {
        public static KRetorno SendEmail(KModelEmail kModelEmail)
        {
            KRetorno retorno = new KRetorno();
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(kModelEmail.EmailFrom),
                    Subject = kModelEmail.Subject,
                    Body = kModelEmail.Body,
                    IsBodyHtml = kModelEmail.IsHtml
                };

                kModelEmail.To.ForEach(t =>
                {
                    mail.To.Add(t.Email);
                });

                using var smtp = new SmtpClient(kModelEmail.STMP)
                {
                    EnableSsl = true,
                    Port = kModelEmail.Port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    // seu usuário e senha para autenticação
                    Credentials = new NetworkCredential(kModelEmail.From.Email, kModelEmail.From.Pass)
                };

                // envia o e-mail
                smtp.Send(mail);

                retorno.Sucesso = true;
            }
            catch (Exception e)
            {
                retorno = KDesignException.GetException(e);
            }
            return retorno;

        }
    }
}
