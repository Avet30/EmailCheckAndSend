using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace MailSender
{
    public class SendEmail
    {
        public void SendMessage()
        {
            //Recupere le mail via un document text afin d'éviter de diffuser son email & mdp


            string mailEnvoyeur = File.ReadAllText(@"D:\Projet VS\MailEnvoyeur.txt");

            string mdpEnvoyeur = File.ReadAllText(@"D:\Projet VS\MdpEnvoyeur.txt");

            //Creation du message
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Avet", "Avet_avetyan@hotmail.com"));
            message.To.Add(MailboxAddress.Parse(mailEnvoyeur));
            message.Subject = "EmailTesting";
            message.Body = new TextPart("plain")
            {
                Text = @"Bonjour ceci est un mail test"
            };


            //Envoi du message sur Outlook
            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.office365.com", 587, false);
                client.Authenticate(mailEnvoyeur, mdpEnvoyeur);
                client.Send(message);

                Console.WriteLine("Email sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
            Console.ReadLine();
        }
    }
}
