using Microsoft.Extensions.Configuration;
using PRM392_ShopClothes_Model.Model.Response;
using PRM392_ShopClothes_Repository.Repository;
using PRM392_ShopClothes_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PRM392_ShopClothes_Service.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task SendConfirmedOrderEmailAsync(string toEmail, OrderResponse orderResponse)
        {
            try
            {
                var subject = "Your Order is Confirmed";

                // Build the email message with order details
                var message = new StringBuilder();
                message.AppendLine("<h1>Order Confirmation</h1>");
                message.AppendLine($"<p>Your Order with order code -<strong>{orderResponse.OrderCode}</strong> - has been confirmed successfully.</p>");
                message.AppendLine("<h2>Order Details:</h2>");
                message.AppendLine($"<p><strong>Create Date:</strong> {orderResponse.OrderDate}</p>");
                message.AppendLine($"<p><strong>Estimated delivery time:</strong> {orderResponse.ShippedDate}</p>");
                message.AppendLine("<h3>Order Items:</h3>");

                foreach (var orderDetail in orderResponse.OrderDetails)
                {
                    message.AppendLine("<p>");
                    message.AppendLine($"<strong>Item Name:</strong> {orderDetail.ProductName}<br/>");
                    message.AppendLine($"<strong>Size:</strong> {orderDetail.Size}<br/>");
                    message.AppendLine($"<strong>Quantity:</strong> {orderDetail.Quantity}<br/>");
                    message.AppendLine($"<strong>Price per Unit:</strong> {orderDetail.UnitPrice} VND<br/>");
                    message.AppendLine("</p>");
                }
                message.AppendLine($"<p><strong>Total Price:</strong> {orderResponse.Total} VND</p>");
                message.AppendLine("<p>Thank you for placing your order with us. We appreciate your business and look forward to serving you again.</p>");
                message.AppendLine("<p>Best regards,<br/>Shop Clothes</p>");

                await SendEmailAsync(toEmail, subject, message.ToString());
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error sending order confirmation email: {ex.Message}");
                // Throw a custom exception or handle the error as appropriate for your application
                throw new Exception("Failed to send order confirmation email.", ex);
            }
        }

        public async Task SendRejectedOrderEmailAsync(string toEmail, OrderResponse orderResponse)
        {
            try
            {
                var subject = "Your Order is Rejected";

                var message = new StringBuilder();
                message.AppendLine("<h1>Order Rejection</h1>");
                message.AppendLine($"<p>Your Order with order code - {orderResponse.OrderCode} - has been rejected.</p>");
                message.AppendLine("<h2>Order Details:</h2>");
                message.AppendLine($"<p><strong>Create Date:</strong> {orderResponse.OrderDate}</p>");
                message.AppendLine("<h3>Order Items:</h3>");

                foreach (var orderDetail in orderResponse.OrderDetails)
                {
                    message.AppendLine("<p>");
                    message.AppendLine($"<strong>Item Name:</strong> {orderDetail.ProductName}<br/>");
                    message.AppendLine($"<strong>Size:</strong> {orderDetail.Size}<br/>");
                    message.AppendLine($"<strong>Quantity:</strong> {orderDetail.Quantity}<br/>");
                    message.AppendLine($"<strong>Price per Unit:</strong> {orderDetail.UnitPrice} VND<br/>");
                    message.AppendLine("</p>");
                }
                message.AppendLine($"<p><strong>Total Price:</strong> {orderResponse.Total} VND</p>");
                message.AppendLine("<p>We apologize for any inconvenience caused.We're committed to resolving this quickly. Your refund will be processed within 24 hours.</p>");
                message.AppendLine("<p>Best regards,<br/>Shop Clothes</p>");

                await SendEmailAsync(toEmail, subject, message.ToString());
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error sending booking confirmation email: {ex.Message}");
                // Throw a custom exception or handle the error as appropriate for your application
                throw new Exception("Failed to send booking confirmation email.", ex);
            }
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(_configuration["EmailSettings:SmtpUsername"], _configuration["EmailSettings:SmtpPassword"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:FromEmail"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
