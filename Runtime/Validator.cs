using System;
using System.Collections.Generic;
using System.Linq;

namespace Validation
{
    public partial class Validator
    {
        /// <summary>
        /// The full list of errors currently available
        /// </summary>
        public List<ValidationError> Errors { get; set; }

        public bool IsValid => Errors.Count == 0;

        public Validator()
        {
            Errors = new List<ValidationError>();
            MessageContainer = new MessageContainer();
        }

        public Validator(MessageContainer container)
        {
            Errors = new List<ValidationError>();
            MessageContainer = container;
        }

        public MessageContainer MessageContainer { get; set; }

        public Validator WithMessage(string message)
        {
            if (Errors.Count > 0)
                Errors.Last().Message = message;

            return this;
        }

        public Validator AddError(string name, string message)
        {
            ValidationError error = new ValidationError(name, message);
            Errors.Add(error);

            return this;
        }

        protected Validator NoError()
        {
            return this;
        }

        #region " Validation Errors "


        /// <summary>
        /// Returns a list of errors with the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<ValidationError> ErrorByName(string name) => Errors.Where(o => o.Name == name).ToList();

        /// <summary>
        /// This will return a unique set of Errors by Name and return the first instance of each error.
        /// </summary>
        public List<ValidationError> UniqueErrors => Errors
                    .GroupBy(o => o.Name)
                    .Select(o => o.First())
                    .ToList();

        #endregion


        #region IsNot

        public Validator IsNotNull(object value)
        {
            return IsNotNull("", value);
        }

        public Validator IsNotNull(string name, object value)
        {
            return IsNotNull(name, value, string.Format(MessageContainer.IsNotNullMessage, name));
        }

        public Validator IsNotNull(string name, object value, string message)
        {
            // do the check
            if (value.IsNotNull())
            {
                return NoError();
            }
            else
            {
                return AddError(name, message);
            }
        }

        #endregion

        #region " Is "

        public Validator Is(Func<bool> func)
        {
            return Is("", func);
        }

        public Validator Is(string name, Func<bool> func)
        {
            return Is(name, func, MessageContainer.IsMessage);
        }

        public Validator Is(string name, Func<bool> func, string message)
        {
            // do the check
            if (func())
            {
                return NoError();
            }
            else
            {
                return AddError(name, message);
            }
        }

        #endregion

        #region " IsNot "

        public Validator IsNot(Func<bool> func)
        {
            return IsNot("", func);
        }

        public Validator IsNot(string name, Func<bool> func)
        {
            return IsNot(name, func, MessageContainer.IsNotMessage);
        }

        public Validator IsNot(string name, Func<bool> func, string message)
        {
            // do the check
            if (func())
            {
                return AddError(name, message);
            }
            else
            {
                return NoError();
            }
        }

        #endregion








    }

    public class ValidationException : Exception
    {
        public Validator Validator { get; }
        public string Message { get; }

        private ValidationException()
        {
        }

        public ValidationException(string message, params object[] args) : base()
        {
            Message = string.Format(message, args);
        }

        public ValidationException(Validator validator, string message, params object[] args) : base()
        {
            Validator = validator;
            Message = string.Format(message, args);
        }
    }


    public class ValidationError
    {
        public string Name { get; }
        public string Message { get; set; }

        public ValidationError(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }

    public class MessageContainer
    {
        public string IsNotMessage { get; set; } = "'{0}' cannot be null.";
        public string IsNotNullMessage { get; set; } = "'{0}' cannot be null.";
        public string IsLengthMessage { get; set; } = "'{0}' must be {1} characters long.";
        public string IsBetweenLengthMessage { get; set; } = "'{0}' must be between {1} and {2} characters long.";
        public string IsMessage { get; set; } = "'{0}' does not match the specified criteria.";
        public string IsEmailMessage { get; set; } = "'{0}' does not match the specified criteria.";
        public string IsNotNullOrEmptyMessage { get; set; } = "'{0}' does not match the specified criteria.";
        public string IsRegexMessage { get; set; } = "'{0}' does not match the provided regular expression.";
        public string IsNotNullOrWhiteSpaceMessage { get; set; } = "'{0}' does not match the specified criteria.";
        public string IsPasswordMessage { get; set; } = "'{0}' is not a valid password. Passwords must be 8 to 30 characters, at least on 1 uppercase letter, at least 1 lowercase letter and at least one number.";
        public string IsMatchMessage { get; set; } = "'{0}' did not match the specified criteria.";
        public string IsMinLengthMessage { get; set; } = "'{0}' must be a at least {1} characters.";
        public string IsMaxLengthMessage { get; set; } = "'{0}' must be {1} characters or less.";
        public string IsExactLengthMessage { get; set; } = "'{0}' must be exactly {1} characters.";

        // Additional messages can be added here as needed
    }
}
