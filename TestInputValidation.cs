using NUnit.Framework;

[TestFixture]
public class TestInputValidation
{
    [Test]
    public void TestForSQLInjection()
    {
        string maliciousInput = "' OR '1'='1";

        Assert.That(
            InputValidator.IsValidUsername(maliciousInput),
            Is.False
        );
    }

    [Test]
    public void TestForXSS()
    {
        string maliciousInput = "<script>alert('XSS')</script>";

        Assert.That(
            InputValidator.IsValidUsername(maliciousInput),
            Is.False
        );
    }
}