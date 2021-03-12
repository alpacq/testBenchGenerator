using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public interface IDesignable
    {
        bool CanDesign { get; }

        void Design();
    }
}
