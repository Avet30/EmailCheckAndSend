namespace MailSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Je crée l'objet retrieveMail
            RetrieveMail retrieveMail = new RetrieveMail();

            //J'utilise la métode RetrieveMessage s
            retrieveMail.RetrieveMessage();
        }
    }
}