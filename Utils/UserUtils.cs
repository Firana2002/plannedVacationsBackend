using System;
using System.Net;
using System.Net.Mail;

namespace VacationPlanner.Api.Utils
{
    public class UserUtils
    {
        public string GenerateTemporaryPassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(password);
        }

        public void SendEmail(string toEmail, string firstName, string temporaryPassword)
        {
            var fromAddress = new MailAddress("", firstName);
            var toAddress = new MailAddress(toEmail);
            var fromPassword = "";
            const string subject = "Ваш временный пароль";
            string body = $"Здравствуйте, {firstName}!\n\nВаш временный пароль: {temporaryPassword}\n\nПожалуйста, измените его при первом входе.";

            var smtp = new SmtpClient
            {
                Host = "smtp.mail.ru",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
