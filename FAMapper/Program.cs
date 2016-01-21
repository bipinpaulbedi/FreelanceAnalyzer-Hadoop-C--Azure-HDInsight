using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

namespace FAMapper
{
    class Program
    {        
        static void Main(string[] args)
        {

            string[] feelingcollection = { "sorry", "apology" };
            if (args.Length > 0)
            {
                Console.SetIn(new StreamReader(args[0]));
            }

            string line;
            string xmlnodeString = string.Empty;           
            bool datafound = false;
            XmlDocument comment = new XmlDocument();

            while ((line = Console.ReadLine()) != null)
            {
                if (line.StartsWith("<comments>"))
                {
                    datafound = true;
                    continue;
                }

                if (datafound)
                {
                    xmlnodeString += line;
                    if (xmlnodeString.EndsWith("/>"))
                    {
                        comment.LoadXml(xmlnodeString);
                        if(feelingcollection.Any(x => comment.FirstChild.Attributes["Text"].Value.Contains(x)))                            
                            Console.WriteLine(comment.FirstChild.Attributes["PostId"].Value);                        
                        xmlnodeString = String.Empty;
                    }
                }
                if(line.EndsWith("</comments>"))
                    datafound = false;
            }
        }
    }
}
