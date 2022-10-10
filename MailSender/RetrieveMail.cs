using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System.Configuration;


namespace MailSender
{
    public class RetrieveMail
    {
        public void RetrieveMessage()
        {
            //Je crée l'objet sendEmail pour utiliser la fonction sendMail
            SendEmail sendEmail = new SendEmail();

            //Je configure le IMAP pour récuperer les mails depuis OVH mailing
            //Avec ConfigurationManager je récupère les données de mon App.Config
            string imapAdress = ConfigurationManager.AppSettings["ImapAdress"];
            int portNumber = int.Parse(ConfigurationManager.AppSettings["PortNumber"]);
            string mailReceveur = ConfigurationManager.AppSettings["MailReceveur"];
            string mdpReceveur = ConfigurationManager.AppSettings["MdpReceveur"];
            var date = DateTime.Now;
            var value = new[] { "Test", "Big", "Small", };
            int msgrecus = 0;

            //Je crée une instance de ImapClient
            using (var client = new ImapClient())
            {
                client.Connect(imapAdress, portNumber, true);
                client.Authenticate(mailReceveur, mdpReceveur);
                //J'utilise la fonction Inbox de MailKit pour ouvrir ma boite de mail
                client.Inbox.Open(FolderAccess.ReadOnly);

                // je cherche tous les mails avec comme paramètre mon DateTime.Now
                var emails = client.Inbox.Search(SearchQuery.DeliveredOn(date));

                //je boucle dans mes mails et utilise GetMessage
                foreach (var mail in emails)
                {
                    var message = client.Inbox.GetMessage(mail);

                    //Je compare l'objet du mail avec mon Array spécifique
                    if (value.Any(message.Subject.Contains))
                    {
                        msgrecus++;
                    }
                }
                //Je compte le nombre de mails reçus, si c'est 4 mails avec le nom spécifique, je fais rien.
                if (msgrecus == 4)
                {
                    Console.WriteLine("ok pas besoin d'envoyer un mail");
                }
                else
                {
                    //Sinon j'envoi un mail avec ma méthode SendMessage()
                    Console.WriteLine("ENVOYER VITE!");
                    //CODE ICI POUR ENVOYER MAIL

                    sendEmail.SendMessage();


                }
                client.Disconnect(true);
            }
        }
    }
}
