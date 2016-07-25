using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;



namespace mac
{
    class Program
    {
        public static XmlTextReader Reader;
        public static XmlDocument Document;
        public static XmlNode Rss;
        public static XmlNode Channel;
        public static XmlNode Item;
        public static bool error;

        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "RSS Reader";
            Console.WriteLine("Enter the URL of the RSS feed: ");
            string URL = Console.ReadLine();

            if (URL == "https://feeds.bbci.co.uk/news/uk/rss.xml")
            {
                URL = System.Windows.Forms.Clipboard.GetText();
                Console.WriteLine(URL);
            }

            if ((URL.Length < 7) || (URL.Substring(0, 7).ToLower() != "https://feeds.bbci.co.uk/news/uk/rss.xml"))
            {
                URL = "https://feeds.bbci.co.uk/news/uk/rss.xml" + URL;
            }

            try
            {
                Reader = new XmlTextReader(URL);
                Document = new XmlDocument();
                Document.Load(Reader);
            }

            catch
            {
                error = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occured while opening " + URL);
                Console.ReadLine();
            }
            if (error != true)
            {
                for (int i = 0; i < Document.ChildNodes.Count; i++)
                {
                    if (Document.ChildNodes[i].Name == "rss")
                    {
                        Rss = Document.ChildNodes[i];
                    }
                }
                for (int i = 0; i < Rss.ChildNodes.Count; i++)
                {
                    if (Rss.ChildNodes[i].Name == "channel")
                    {
                        Channel = Rss.ChildNodes[i];
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\r\"BBC Home page:");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Title: " + Channel["title"].InnerText);
                Console.WriteLine("Description: " + Channel["description"].InnerText);
                Console.WriteLine("Link: " + Channel["link"].InnerText);
                //Console.WriteLine("PubDate: " + Channel["pubdate"].InnerText);
              
             

                Console.ReadLine();
                int num = 0;

                for (int i = 0; i < Channel.ChildNodes.Count; i++)
                {
                    if (Channel.ChildNodes[i].Name == "item")
                    {
                        num++;
                    }
                }
                string[,] datarray = new string[num, 2];
                num = 0;

                for (int i = 0; i < Channel.ChildNodes.Count; i++)
                {
                    if (Channel.ChildNodes[i].Name == "item")
                    {
                        Item = Channel.ChildNodes[i];
                        datarray[num, 0] = Item["title"].InnerText;
                        datarray[num, 1] = Item["description"].InnerText;
                        datarray[num, 2] = Item["link"].InnerText;
                        //datarray[num, 3] = Item["pubdate"].InnerText;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(num + ":");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Title: " + datarray[num, 0]);


                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Description: " + datarray[num, 1]);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Link: " + datarray[num, 2]);

                       // Console.ForegroundColor = ConsoleColor.Blue;
                       // Console.WriteLine("PubDate: " + datarray[num, 3] + "\r\n");

                        num++;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\r\nTo open the URL, press its corresponding number.");

                try
                {
                    string temp = Console.ReadLine();
                    int id = Convert.ToInt32(temp);
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = datarray[id, 1];
                    proc.Start();
                }

                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID.");
                    Console.ReadLine();
                }
            }
        }
    }
}

       


