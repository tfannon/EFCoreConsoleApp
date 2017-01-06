using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreConsoleApp
{
    public class Program
    {
        //this assumes a sql server database server already setup with the migration run on the database
        public static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                Console.WriteLine(Environment.GetEnvironmentVariable("DBServer"));
                if (!db.Blogs.Any())
                {
                    Console.WriteLine("Creating some blog entries");
                    for (var i = 0; i < 10; i++)
                    {
                        var semiRandomName = Guid.NewGuid().ToString().Substring(0, 8);
                        var blog = new Blog {Url = $"http://{semiRandomName}.com"};
                        db.Blogs.Add(blog);
                    }
                    db.SaveChanges();
                }
                Console.WriteLine("Reading blog entries");
                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine($"{blog.BlogId} : {blog.Url}");
                }
                Console.WriteLine("Press return");
                Console.ReadLine();
            }
        }
    }
}
