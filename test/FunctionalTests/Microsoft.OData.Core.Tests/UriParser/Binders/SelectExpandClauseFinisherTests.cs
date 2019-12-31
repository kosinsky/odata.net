//---------------------------------------------------------------------
// <copyright file="SelectExpandClauseFinisherTests.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.UriParser;
using Microsoft.OData.Edm;
using Xunit;

namespace Microsoft.OData.Tests.UriParser.Binders
{
    public class SelectExpandClauseFinisherTests
    {
        [Fact]
        public void ExpandedNavigationPropertiesAreImplicitlyAddedAsPathSelectionItemsIfSelectIsPopulated()
        {
            IEdmNavigationProperty navigationProperty = ModelBuildingHelpers.BuildValidNavigationProperty();
            SelectExpandClause clause = new SelectExpandClause(new SelectItem[]
            {
                new PathSelectItem(new ODataSelectPath(new PropertySegment(ModelBuildingHelpers.BuildValidPrimitiveProperty()))),
                new ExpandedNavigationSelectItem(new ODataExpandPath(new NavigationPropertySegment(navigationProperty, ModelBuildingHelpers.BuildValidEntitySet())), ModelBuildingHelpers.BuildValidEntitySet(), new SelectExpandClause(new List<SelectItem>(), false)), 
            },
            false /*allSelected*/);
            SelectExpandClauseFinisher.AddExplicitNavPropLinksWhereNecessary(clause);

            Assert.Equal(3, clause.SelectedItems.Count());
            var pathSelectItem = Assert.IsType<PathSelectItem>(clause.SelectedItems.Last(x => x is PathSelectItem));
            var propertySegment = Assert.IsType<NavigationPropertySegment>(pathSelectItem.SelectedPath.LastSegment);
        }

        [Fact]
        public void ExpandedNavigationPropertiesAreNotAddedAsPathSelectionItemsIfSelectIsNotPopulated()
        {
            SelectExpandClause clause = new SelectExpandClause(new SelectItem[]
            {
                new ExpandedNavigationSelectItem(new ODataExpandPath(new NavigationPropertySegment(ModelBuildingHelpers.BuildValidNavigationProperty(), ModelBuildingHelpers.BuildValidEntitySet())), ModelBuildingHelpers.BuildValidEntitySet(), new SelectExpandClause(new List<SelectItem>(), false)), 
            },
            false /*allSelected*/);
            SelectExpandClauseFinisher.AddExplicitNavPropLinksWhereNecessary(clause);
            Assert.Single(clause.SelectedItems);
        }
    }
}
