using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.OData.UriParser
{
    public interface IFunctionCallNode
    {
        IEnumerable<IEdmFunction> Functions { get; }

        IEnumerable<QueryNode> Parameters { get; }

        string Name { get; }

        QueryNode Source { get; }
    }
}
