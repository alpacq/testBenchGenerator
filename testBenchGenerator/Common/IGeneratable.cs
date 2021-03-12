using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public interface IGeneratable
    {
        bool CanGenerate { get; }

        bool Generate();
    }
}
