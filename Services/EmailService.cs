using Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmail
    {
        private readonly MyData _myData;
        public EmailService(MyData myData)
        {
            _myData= myData;
        }
        public async Task<string> GetDBDataToSendEmail(int orderId)
        {
            var order = await (_myData.orders.AsNoTracking()
                                .Include(x => x.OrderItem)
                                .ThenInclude(x=>x.Product)
                                .Include(x=>x.User)
                                .Where(s=>s.Id == orderId)
                                )
                                .SingleAsync();
            
            var message = await AddToEmail(order);
            return message;
        }
        private async Task<string> AddToEmail(Order order)
        {
            string emailTemplate = "";
            try
            {

                string filePath = Directory.GetCurrentDirectory() + "\\Templates\\EmailTemplate.html";
                emailTemplate = File.ReadAllText(filePath);
            
            } catch (Exception ex)
            {
                return ex.Message;
            }
            emailTemplate = emailTemplate.Replace("{firstName}", order.User.FirstName);
            emailTemplate = emailTemplate.Replace("{lastName}", order.User.LastName);
            emailTemplate = emailTemplate.Replace("{address}", order.User.Address);
            emailTemplate = emailTemplate.Replace("{orderedDate}", order.Date.ToString());
            emailTemplate = emailTemplate.Replace("{subTotalPrice}", order.SubTotalPrice.ToString());
            
            foreach(var item in order.OrderItem)
            {
                emailTemplate += "<tr><td>" + item.Product.Name + "</td><td>" + item.Product.UnitPrice + "</td><td>" + item.Quentity + "</td><td>" + item.TotalPrice + "</td>";
            }
            emailTemplate += "</tbody></table><p>Thank You!</p></body></html>";
            
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("hirushop123@gmail.com"));
            email.To.Add(MailboxAddress.Parse(order.User.Email));
            email.Subject = "order is Success";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailTemplate
            };
            var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("hirushop123@gmail.com", "swzultpjgujvumfp");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            
            return "Email is sent";
        }
    }
}
