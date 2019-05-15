using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.OData.UriParser
{
    public interface IFunctionCallNode
    {
        IEnumerable<QueryNode> Parameters { get; }
    }
}
