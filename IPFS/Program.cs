using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ipfs.Http;
using System.IO;
namespace ipfs_console
{
    class Program
    {
        static void Main(string[] args)
        {
            download().Wait();
        }
        static  async Task download()
        {
            var ipfs = new IpfsClient();
            var fsn = await ipfs.FileSystem.AddFileAsync("note.xml");
            Console.WriteLine((string)fsn.Id);
            string text = await ipfs.FileSystem.ReadAllTextAsync((string)fsn.Id);
            File.WriteAllText("saved.xml", text);
        }
    }
}
