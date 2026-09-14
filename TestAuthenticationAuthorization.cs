using NUnit.Framework;

[TestFixture]
public class TestAuthenticationAuthorization
{
    [Test]
    public void ValidLoginShouldSucceed()
    {
        var auth = new AuthenticationService();

        auth.Register("alice", "Password123");

        Assert.That(
            auth.Authenticate("alice", "Password123"),
            Is.True
        );
    }

    [Test]
    public void InvalidPasswordShouldFail()
    {
        var auth = new AuthenticationService();

        auth.Register("alice", "Password123");

        Assert.That(
            auth.Authenticate("alice", "WrongPassword"),
            Is.False
        );
    }

    [Test]
    public void AdminShouldAccessDashboard()
    {
        var auth = new RoleAuthorization();

        auth.AssignRole("alice", "admin");

        Assert.That(
            auth.CanAccessAdminDashboard("alice"),
            Is.True
        );
    }

    [Test]
    public void NormalUserShouldNotAccessDashboard()
    {
        var auth = new RoleAuthorization();

        auth.AssignRole("bob", "user");

        Assert.That(
            auth.CanAccessAdminDashboard("bob"),
            Is.False
        );
    }

    [Test]
    public void UnknownUserShouldNotAccessDashboard()
    {
        var auth = new RoleAuthorization();

        Assert.That(
            auth.CanAccessAdminDashboard("unknown"),
            Is.False
        );
    }
}