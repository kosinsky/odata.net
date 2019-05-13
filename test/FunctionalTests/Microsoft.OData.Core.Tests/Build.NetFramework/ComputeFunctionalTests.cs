using FluentAssertions;
using Microsoft.OData.Edm;
using Microsoft.OData.Tests.UriParser;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.OData.Tests.ScenarioTests.UriParser
{
    /// <summary>
    /// This file contains functional tests for the ODataUriParser.ParseCompute.
    /// </summary>
    public class ComputeFunctionalTests
    {

        [Fact]
        public void ComputeWithCollectionResourceBoundFunction()
        {
            ComputeClause compute = ParseCompute("Fully.Qualified.Namespace.GetPriorAddresses as PriorAddresses", HardCodedTestModel.TestModel, HardCodedTestModel.GetPersonType());
            ComputeExpression expression = compute.ComputedItems.Single();
            expression.Alias.Should().Be("PriorAddresses");
            expression.Expression.As<CollectionResourceFunctionCallNode>().Name.Should().Be("Fully.Qualified.Namespace.GetPriorAddresses");
        }

        private static ComputeClause ParseCompute(string text, IEdmModel edmModel, IEdmType edmType, IEdmNavigationSource edmEntitySet = null)
        {
            return new ODataQueryOptionParser(edmModel, edmType, edmEntitySet, new Dictionary<string, string>() { { "$compute", text } }) { Resolver = new ODataUriResolver() { EnableCaseInsensitive = false } }.ParseCompute();
        }
    }
}
