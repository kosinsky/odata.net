//---------------------------------------------------------------------
// <copyright file="ComputeTransformationNode.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.UriParser.Aggregation
{
    using Microsoft.OData.UriParser;

    /// <summary>
    /// Node representing a Compute transformation.
    /// </summary>
    public sealed class ComputeTransformationNode : TransformationNode
    {
        private readonly ComputeClause computeClause;

        /// <summary>
        /// Create a ComputeTransformationNode.
        /// </summary>
        /// <param name="computeClause">A <see cref="ComputeClause"/> representing the metadata bound Compute expression.</param>
        public ComputeTransformationNode(ComputeClause computeClause)
        {
            ExceptionUtils.CheckArgumentNotNull(computeClause, "computeClause");

            this.computeClause = computeClause;
        }

        /// <summary>
        /// Gets the <see cref="ComputeClause"/> representing the metadata bound Compute expression.
        /// </summary>
        public ComputeClause ComputeClause
        {
            get
            {
                return this.computeClause;
            }
        }

        /// <summary>
        /// Gets the kind of the transformation node.
        /// </summary>
        public override TransformationNodeKind Kind
        {
            get
            {
                return TransformationNodeKind.Compute;
            }
        }
    }
}
