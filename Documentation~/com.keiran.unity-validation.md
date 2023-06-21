# Validation Tool
The Validation Tool is a utility that allows you to perform various validations on data and collect error messages. It provides a fluent API for defining validation rules and capturing validation errors. This tool is designed to help you easily validate input data and handle validation errors in your applications.


## Usage
To use the Validation Tool, follow these steps:

Import the Validation namespace in your code:

```csharp
using Validation;
```

## Create an instance of the Validator class:

```csharp
Validator validator = new Validator();
```

Define your validation rules using the fluent API provided by the Validator class. For example, to check if a string is not null or empty:

```csharp
validator.IsNotNullOrEmpty("name", value);
```

You can chain multiple validation rules together to perform complex validations.

Check if the data is valid by checking the `IsValid` property:

```csharp
if (validator.IsValid)
{
    // Data is valid, continue with your logic
}
else
{
    // Data is invalid, handle validation errors
    List<ValidationError> errors = validator.Errors;
    // Handle the validation errors as needed
}
```

If there are any validation errors, you can retrieve the list of errors using the Errors property of the Validator class.

Customize the error messages by modifying the MessageContainer properties of the Validator instance. You can provide your own error messages or use the default messages provided by the MessageContainer class.

## Validation Rules
The Validation Tool provides various validation rules that you can use to validate different types of data. Here are some examples:

- `IsNotNull`: Checks if a value is not null.
- `IsNotNullOrEmpty`: Checks if a string is not null or empty.
- `IsNotNullOrWhiteSpace`: Checks if a string is not null, empty, or contains only whitespace.
- `IsEmail`: Checks if a string is a valid email address.
- `IsRegex`: Checks if a string matches a specified regular expression.
- `IsPassword`: Checks if a string is a valid password according to specified criteria.
- `IsMinLength`: Checks if a string has a minimum length.
- `IsMaxLength`: Checks if a string has a maximum length.
- `IsBetweenLength`: Checks if a string has a length within a specified range.
- `IsExactLength`: Checks if a string has an exact length.
- `IsMatch`: Checks if two strings are equal.

You can chain multiple validation rules together to perform complex validations.

## Customizing Error Messages

The Validation Tool allows you to customize the error messages to provide more meaningful feedback to the user. You can modify the default error messages by setting the properties of the MessageContainer class. For example:

```csharp
validator.MessageContainer.IsNotNullOrEmptyMessage = "'{0}' cannot be empty.";
```
You can modify the error messages before performing the validations to ensure that the error messages reflect your application's requirements.

## Handling Validation Errors
When there are validation errors, you can retrieve the list of errors using the Errors property of the Validator class. Each error contains the name of the field and the corresponding error message. You can then handle the validation errors as needed, such as displaying them to the user or logging them for further analysis.


```csharp
// Check the validity of the data
if (validator.IsValid)
{
    // Data is valid, proceed with further processing
}
else
{
    // Data is invalid, handle validation errors
    List<ValidationError> errors = validator.Errors;

    foreach (ValidationError error in errors)
    {
        string fieldName = error.FieldName;
        string errorMessage = error.ErrorMessage;

        // Display or handle the validation error
        Console.WriteLine($"Validation error for field '{fieldName}': {errorMessage}");
    }
}
```

### Exception Handling
If you prefer to handle validation errors using exceptions, you can use the ValidationException class. It provides the Validator object and the error message as properties. You can catch the exception and access the validation errors for further processing.

Example:

```csharp
try
{
    // Perform validation checks
    // If data is invalid, throw a ValidationException
    throw new ValidationException(validator, "Data validation failed.");
}
catch (ValidationException ex)
{
    // Handle the exception and access the validation errors
    Validator validator = ex.Validator;
    List<ValidationError> errors = validator.Errors;
    // Display or handle the validation errors as needed
}
```

## Complete Examples

```csharp
// validate the name
validator
    .NotNull(name).WithMessage("Name cannot be an empty string")
    .IsLength(name, 5).WithMessage("Name must be at least 5 characters long")
    .IsLength(name, 5, 20).WithMessage("Name must between 5 and 20 characters")
    .Must(() =>
        {
            return name.Contains("abc");
        }).WithMessage("Name cannot contain abc")
    .MustNot(() =>
        {
            return name.Contains("bob");
        }).WithMessage("Name cannot contain bob");
```
