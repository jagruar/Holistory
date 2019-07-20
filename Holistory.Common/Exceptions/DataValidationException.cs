using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Holistory.Common.Exceptions
{
    public class DataValidationException : ValidationException
    {
        public DataValidationException(string message)
            : base(message)
        {
        }

        public DataValidationException(string paramName, string errorMessage)
            : base(new ValidationFailure[] { new ValidationFailure(paramName, errorMessage) })
        {
        }

        public DataValidationException(IEnumerable<ValidationFailure> errors)
            : base(errors)
        {
        }

        public DataValidationException(string message, IEnumerable<ValidationFailure> errors)
            : base(message, errors)
        {
        }

        public DataValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Throws a <see cref="DataValidationException"/> if the string value is null or empty.
        /// </summary>
        /// <param name="valueToValidate">The value to check for being null.</param>
        /// <param name="paramName">The name of the parameter. Use nameof(param).</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The value supplied if it is valid, otherwise throws an exception.</returns>
        public static string ThrowIfNullOrWhitespaceString(string valueToValidate, string paramName, string errorMessage)
        {
            return string.IsNullOrWhiteSpace(valueToValidate) ? throw new DataValidationException(paramName, errorMessage) : valueToValidate;
        }

        /// <summary>
        /// Throws a <see cref="DataValidationException"/> if the value is null.
        /// </summary>
        /// <typeparam name="T">The type of the value to check.</typeparam>
        /// <param name="valueToValidate">The value to check for being null.</param>
        /// <param name="paramName">The name of the parameter. Use nameof(param).</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The value supplied if it is valid, otherwise throws an exception.</returns>
        public static T ThrowIfNull<T>(T valueToValidate, string paramName, string errorMessage)
            where T : class
        {
            return valueToValidate ?? throw new DataValidationException(paramName, errorMessage);
        }

        /// <summary>
        /// Throws a <see cref="DataValidationException"/> if the value is true.
        /// </summary>
        /// <param name="isTrue">Whether to throw an exception.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void ThrowIfTrue(bool isTrue, string errorMessage)
        {
            if (isTrue)
            {
                throw new DataValidationException(errorMessage);
            }
        }
    }
}
