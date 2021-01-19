using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Model
{
    public class InputFile
    {
        private string path;
        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        public InputFile(string path)
        {
            this.Path = path;
            this.Name = this.Path.Split('\\').ToList<string>().LastOrDefault().Replace(".txt", "");
        }
    }
}
