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
        public void ComputeWithCollectionBoundFunction()
        {
            ComputeClause compute = ParseCompute("Fully.Qualified.Namespace.GetPriorAddresses as PriorAddresses", HardCodedTestModel.TestModel, HardCodedTestModel.GetPersonType());
            ComputeExpression expression = compute.ComputedItems.Single();
            expression.Alias.Should().Be("PriorAddresses");
            expression.Expression.As<CollectionFunctionCallNode>().Name.Should().Be("Fully.Qualified.Namespace.GetPriorAddresses");
        }

        [Fact]
        public void ComputeWithCollectionResourceBoundFunction()
        {
            ComputeClause compute = ParseCompute("Fully.Qualified.Namespace.GetHotPeople(limit=1) as Hot", HardCodedTestModel.TestModel, HardCodedTestModel.GetPersonType());
            ComputeExpression expression = compute.ComputedItems.Single();
            expression.Alias.Should().Be("Hot");
            expression.Expression.As<CollectionResourceFunctionCallNode>().Name.Should().Be("Fully.Qualified.Namespace.GetHotPeople");
        }

        [Fact]
        public void ComputeWithComplexBoundFunction()
        {
            ComputeClause compute = ParseCompute("Fully.Qualified.Namespace.GetPriorAddress as PriorAddress", HardCodedTestModel.TestModel, HardCodedTestModel.GetPersonType());
            ComputeExpression expression = compute.ComputedItems.Single();
            expression.Alias.Should().Be("PriorAddress");
            expression.Expression.As<SingleValueFunctionCallNode>().Name.Should().Be("Fully.Qualified.Namespace.GetPriorAddress");
        }

        [Fact]
        public void ComputeWithEntityBoundFunction()
        {
            ComputeClause compute = ParseCompute("Fully.Qualified.Namespace.GetMyPerson as Person", HardCodedTestModel.TestModel, HardCodedTestModel.GetDogType());
            ComputeExpression expression = compute.ComputedItems.Single();
            expression.Alias.Should().Be("Person");
            expression.Expression.As<SingleResourceFunctionCallNode>().Name.Should().Be("Fully.Qualified.Namespace.GetMyPerson");
        }

        [Fact]
        public void ComputeWithEnumBoundFunction()
        {
            ComputeClause compute = ParseCompute("Fully.Qualified.Namespace.GetPetCount(colorPattern='BlueYellowStriped') as Person", HardCodedTestModel.TestModel, HardCodedTestModel.GetPersonType());
            ComputeExpression expression = compute.ComputedItems.Single();
            expression.Alias.Should().Be("Person");
            expression.Expression.As<SingleResourceFunctionCallNode>().Name.Should().Be("Fully.Qualified.Namespace.GetPetCount");
            expression.Expression.As<SingleResourceFunctionCallNode>().Parameters.First().As<NamedFunctionParameterNode>().Value.GetEdmType().FullTypeName().Should().Be("Fully.Qualified.Namespace.ColorPattern");
        }

        private static ComputeClause ParseCompute(string text, IEdmModel edmModel, IEdmType edmType, IEdmNavigationSource edmEntitySet = null)
        {
            return new ODataQueryOptionParser(edmModel, edmType, edmEntitySet, new Dictionary<string, string>() { { "$compute", text } }) { Resolver = new ODataUriResolver() { EnableCaseInsensitive = false } }.ParseCompute();
        }
    }
}
