using System;
using System.IO;

namespace FAReducer
{
    class Program
    {
        static void Main(string[] args)
        {
            string postId, lastPostId = null;
            int count = 0;

            if (args.Length > 0)
            {
                Console.SetIn(new StreamReader(args[0]));
            }

            while ((postId = Console.ReadLine()) != null)
            {
                if (postId != lastPostId)
                {
                    if (lastPostId != null)
                        Console.WriteLine("{0}[{1}]", lastPostId, count);
                    count = 1;
                    lastPostId = postId;
                }
                else
                    count += 1;
            }
            Console.WriteLine(count);
        }
    }
}
