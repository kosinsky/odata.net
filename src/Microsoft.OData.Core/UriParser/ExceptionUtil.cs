//---------------------------------------------------------------------
// <copyright file="ExceptionUtil.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.UriParser
{
    using Microsoft.OData.Edm;
    using System.Collections.Generic;
    using ODataErrorStrings = Microsoft.OData.Strings;

    /// <summary>
    /// Helper class for throwing exceptions during URI parsing.
    /// </summary>
    internal static class ExceptionUtil
    {
        /// <summary>Creates a new "Resource Not Found" exception.</summary>
        /// <param name="identifier">segment identifier information for which resource was not found.</param>
        /// <returns>A new exception to indicate the requested resource cannot be found.</returns>
        internal static ODataException CreateResourceNotFoundError(string identifier)
        {
            // 404: Not Found
            return ResourceNotFoundError(Strings.RequestUriProcessor_ResourceNotFound(identifier));
        }

        /// <summary>Creates a new "Resource Not Found" exception.</summary>
        /// <param name="errorMessage">Plain text error message for this exception.</param>
        /// <returns>A new exception to indicate the requested resource cannot be found.</returns>
        internal static ODataException ResourceNotFoundError(string errorMessage)
        {
            // 404: Not Found
            return new ODataUnrecognizedPathException(errorMessage);
        }

        /// <summary>Creates a new exception to indicate a syntax error.</summary>
        /// <returns>A new exception to indicate a syntax error.</returns>
        internal static ODataException CreateSyntaxError()
        {
            return CreateBadRequestError(Strings.RequestUriProcessor_SyntaxError);
        }

        /// <summary>
        /// Creates a new exception to indicate BadRequest error.
        /// </summary>
        /// <param name="message">Plain text error message for this exception.</param>
        /// <returns>A new exception to indicate a bad request error.</returns>
        internal static ODataException CreateBadRequestError(string message)
        {
            // 400 - Bad Request
            return new ODataException(message);
        }

        /// <summary>
        /// Throws if the type is not related to the type of the given set.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <param name="secondType">Second type, which should be related to the first type.</param>
        /// <param name="segmentName">The segment that is checking this.</param>
        internal static void ThrowIfTypesUnrelated(IEdmType type, IEdmType secondType, string segmentName)
        {
            if (!UriEdmHelpers.IsRelatedTo(type.AsElementType(), secondType.AsElementType()))
            {
                throw new ODataException(Strings.PathParser_TypeMustBeRelatedToSet(type, secondType, segmentName));
            }
        }

        /// <summary>
        /// Exception for property not being found in the given type.
        /// </summary>
        /// <param name="propertyName">The propertyName which was not found in the type.</param>
        /// <param name="typeName">The typeName of the type in which the property was not found.</param>
        internal static ODataErrorException CreatePropertyNotFoundException(string propertyName, string typeName , bool isOpenProperty = false)
        {
            ODataError odataError = GenerateODataError(propertyName, typeName, isOpenProperty? ErrorCodes.OpenPropertyNotFoundInType: ErrorCodes.PropertyNotFoundInType);

            return new ODataErrorException(ODataErrorStrings.MetadataBinder_PropertyNotDeclared(typeName, propertyName), odataError);
        }

        private static ODataError GenerateODataError(string propertyName, string typeName, string errorCode)
        {
            return new ODataError
            {
                ErrorCode = errorCode,
                Target = propertyName,
                Details = new List<ODataErrorDetail>
                    {
                        new ODataErrorDetail {
                            ErrorCode = errorCode,
                            Target = typeName
                        }
                    }
            };
        }
    }
}