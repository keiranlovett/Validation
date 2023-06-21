using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Validation;

public class ValidatorTest
{
    [Test]
    public void TestEmailValidation()
    {
        Validator validator = new Validator();

        // value
        validator.IsEmail("test@email.com");

        // name, value
        validator.IsEmail("Email", "test@email.com");

        // name, value, message
        validator.IsEmail("Email", "test@ema", "Please enter a valid email address");

        if (validator.IsValid)
        {
            Debug.Log("Email validation passed");
        }
        else
        {
            Assert.Fail("Email validation failed: " + validator.Errors[0].Message);
        }
    }

    [Test]
    public void TestNameValidation()
    {
        Validator validator = new Validator();

        string name = "hello";
        // validate the name
        validator
            .IsNotNull(name).WithMessage("Name cannot be an empty string")
            .IsMinLength(name, 5).WithMessage("Name must be at least 5 characters long")
            .IsBetweenLength(name, 5, 20).WithMessage("Name must be between 5 and 20 characters")
            .IsNot(() => name.Contains("abc")).WithMessage("Name must not contain abc")
            .Is(() => name.Contains("bob")).WithMessage("Name cannot contain bob");

        if (validator.IsValid)
        {
            Debug.Log("Name validation passed");
        }
        else
        {
            Assert.Fail("Name validation failed: " + validator.Errors[0].Message);
        }
    }



    [Test]
    public void Test_Invalid_Email()
    {

        string email = "InvalidEmail";

        Validator validator = new Validator();

        validator.IsEmail(email);

        System.Diagnostics.Debug.WriteLine(validator.Errors.Count);

        Assert.IsTrue(validator.Errors.Count == 1);
    }

    [Test]
    public void Test_Invalid_Emails()
    {
        List<string> emails = new List<string>()
            {
                "valid@test.com", // valid
				"valid@t.com", // valid
				"v@v.co", // valid
				"@v.com", // invalid
				"v@.co", // invalid
				"v@v" // invalid
			};

        Validator validator = new Validator();

        foreach (var email in emails)
        {
            validator.IsEmail(email);
        }

        Assert.IsTrue(validator.Errors.Count == 3);
    }

    [Test]
    public void Test_Email_Message()
    {
        string email = "InvalidEmail";

        Validator validator = new Validator();

        validator.IsEmail(email).WithMessage("Test message");

        Assert.AreEqual(validator.Errors[0].Message, "Test message");
    }

    [Test]
    public void Test_Multiple_Email_Message()
    {
        List<string> emails = new List<string>()
            {
                "v@v.co", // valid
				"@v.com", // invalid
				"valid@t.com", // valid
				"v@.co", // invalid
				"valid@test.com", // valid
				"v@v" // invalid
			};

        Validator validator = new Validator();

        for (int i = 0; i < emails.Count; i++)
        {
            validator.IsEmail(emails[i]).WithMessage("Message " + i);
        }

        Assert.IsTrue(validator.Errors.Count == 3);
        Assert.AreEqual(validator.Errors[0].Message, "Message 1");
        Assert.AreEqual(validator.Errors[1].Message, "Message 3");
        Assert.AreEqual(validator.Errors[2].Message, "Message 5");
    }


    [Test]
    public void Test_Matches()
    {
        string input = "abc123";

        Validator validator = new Validator();

        validator.IsMatch(input, "");
        validator.IsMatch(input, "a");
        validator.IsMatch(input, "ab");
        validator.IsMatch(input, "abc");
        validator.IsMatch(input, "1");
        validator.IsMatch(input, "12");
        validator.IsMatch(input, "123");
        validator.IsMatch(input, "abc123");
        validator.IsMatch(input, "Abc123");
        validator.IsMatch(input, "aBc123");
        validator.IsMatch(input, "abC123");
        validator.IsMatch(input, "abc123 ");
        validator.IsMatch(input, " abc123");
        validator.IsMatch(input, null);
        validator.IsMatch(input, "****");

        Assert.IsTrue(validator.Errors.Count == 14);
    }

    [Test]
    public void Test_Inputs()
    {
        string match = "abc123";

        Validator validator = new Validator();

        validator.IsMatch("", match);
        validator.IsMatch("a", match);
        validator.IsMatch("ab", match);
        validator.IsMatch("abc", match);
        validator.IsMatch("1", match);
        validator.IsMatch("12", match);
        validator.IsMatch("123", match);
        validator.IsMatch("abc123", match);
        validator.IsMatch("Abc123", match);
        validator.IsMatch("aBc123", match);
        validator.IsMatch("abC123", match);
        validator.IsMatch("abc123 ", match);
        validator.IsMatch(" abc123", match);
        validator.IsMatch(null, match);
        validator.IsMatch("****", match);

        Assert.IsTrue(validator.Errors.Count == 14);
    }


    [Test]
    public void IsGreaterThanTest()
    {
        Validator validator = new Validator();

        validator.Is(() => 5.IsGreaterThan(1));

        Assert.IsTrue(validator.Errors.Count == 0);
    }

}
