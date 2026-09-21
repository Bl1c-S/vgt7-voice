using System.Net;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace API_NUnit_Tests.Controllers.Authentication;

public class EmailRegistrationTests : EmailAuthenticationTestBase
{
    [Test]
    public async Task RegisterEmail_WithValidData_ReturnsOkAndCreatesUser()
    {
        await RegisterTest(new(ValidEmail, ValidPassword), HttpStatusCode.OK);

        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var user = db.Users.FirstOrDefault(u => u.Email == ValidEmail);
        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public async Task RegisterEmail_WithInvalidEmail_ReturnsBadRequest()
    {
        await RegisterTest(new("invalid-email", ValidPassword), HttpStatusCode.BadRequest);
    }
    
    [Test]
    public async Task RegisterEmail_WithInvalidPassword_ReturnsBadRequest()
    {
        await RegisterTest(new(ValidEmail, "simple-password"), HttpStatusCode.BadRequest);
    }
    
    [Test]
    public async Task RegisterEmail_WithInvalidRequest_ReturnsBadRequest()
    {
        await RegisterTest(new("invalid-email", "simple-password"), HttpStatusCode.BadRequest);
    }
    
    [Test]
    public async Task RegisterEmail_WithDuplicateRequest_ReturnsOkAnyway()
    {
        await RegisterTest(new(ValidEmail, ValidPassword), HttpStatusCode.OK);
        await RegisterTest(new(ValidEmail, ValidPassword), HttpStatusCode.OK);
    }
}