using NUnit.Framework;

[TestFixture]
public class SecurityRegressionTests
{
    [Test]
    public void UsernameValidationRejectsSqlInjectionAttempt()
    {
        string maliciousInput = "' OR '1'='1";

        Assert.That(
            InputValidator.IsValidUsername(maliciousInput),
            Is.False
        );
    }

    [Test]
    public void UsernameValidationRejectsXssPayload()
    {
        string maliciousInput = "<script>alert('XSS')</script>";

        Assert.That(
            InputValidator.IsValidUsername(maliciousInput),
            Is.False
        );
    }

    [Test]
    public void EmailValidationRejectsInvalidEmail()
    {
        string invalidEmail = "not-an-email";

        Assert.That(
            InputValidator.IsValidEmail(invalidEmail),
            Is.False
        );
    }

    [Test]
    public void RegistrationRejectsInvalidUsernames()
    {
        var auth = new AuthenticationService();

        Assert.Throws<ArgumentException>(() =>
            auth.Register("bad user", "Password123")
        );
    }

    [Test]
    public void RegistrationRejectsPasswordsShorterThanEightCharacters()
    {
        var auth = new AuthenticationService();

        Assert.Throws<ArgumentException>(() =>
            auth.Register("alice", "short")
        );
    }

    [Test]
    public void RoleAssignmentRejectsInvalidUsernames()
    {
        var authorization = new RoleAuthorization();

        Assert.Throws<ArgumentException>(() =>
            authorization.AssignRole("bad user", "admin")
        );
    }

    [Test]
    public void NormalUserShouldNotAccessAdminDashboard()
    {
        var authorization = new RoleAuthorization();

        authorization.AssignRole("bob", "user");

        Assert.That(
            authorization.CanAccessAdminDashboard("bob"),
            Is.False
        );
    }

    [Test]
    public void AdminUserShouldAccessAdminDashboard()
    {
        var authorization = new RoleAuthorization();

        authorization.AssignRole("alice", "admin");

        Assert.That(
            authorization.CanAccessAdminDashboard("alice"),
            Is.True
        );
    }
}
