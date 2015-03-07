using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: CLSCompliant(true)]
namespace Achtergrondtaken
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                writeComment();

            if (args.Contains("-InboxOpvolgingTaak"))
            {
                InboxOpvolgingTaak inboxOpvolgingTaak = new InboxOpvolgingTaak();
                inboxOpvolgingTaak.Start();
                return;
            }

            if (args.Contains("-InboxSamenvattingTaak"))
            {
                InboxSamenvattingTaak inboxSamenvattingTaak = new InboxSamenvattingTaak();
                inboxSamenvattingTaak.Start();
                return;
            }

            if (args.Contains("-AnalyseerAudioTaak"))
            {
                AnalyseerAudioTaak analyseerAdioTaak = new AnalyseerAudioTaak();
                analyseerAdioTaak.Start();
                //Console.ReadLine();
                return;
            }

        }

        private static void writeComment()
        {
            Console.WriteLine("PrekenWeb Achtergrondtaken");
            Console.WriteLine("(c) {0} Kees Schollaart", DateTime.Now.Year);
            Console.WriteLine("Gebruik de volgende switches:");
            Console.WriteLine(" ");
            Console.WriteLine("    -InboxOpvolgingTaak");
            Console.WriteLine("     Verstuurt de nog te versturen mails uitgaande van inbox opvolging.");
            Console.WriteLine(" ");
            Console.WriteLine("    -InboxSamenvattingTaak");
            Console.WriteLine("     Verstuurt samenvatting van het mailcontact via PrekenWeb.nl.");
            Console.WriteLine(" ");
            Console.WriteLine("    -AnalyseerAudioTaak");
            Console.WriteLine("     Bekijkt de mp3tjes en vult de duur in bij de gerelateerde preek.");
            Console.WriteLine(" ");
            Console.WriteLine("Druk op enter...");
            Console.ReadLine();
        }
    }
}
