using System.Net;
using System.Net.Http.Json;
using API_NUnit_Tests.Infrastructure;
using API.Models.Requests;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace API_NUnit_Tests.Controllers.Authentication;

public class RegistrationTests : IntegrationTestBase
{
    const string _RegisterByEmailEndpoint = "api/auth/email/register";

    const string _ValidEmail = "testuser@example.com";
    const string _ValidPassword = "StrongPassword123!";

    [Test]
    public async Task RegisterEmail_WithValidData_ReturnsOkAndCreatesUser()
    {
        var requestDto = new RegisterByEmailRequest(
            _ValidEmail,
            _ValidPassword);

        var response = await Client.PostAsJsonAsync(_RegisterByEmailEndpoint, requestDto);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var user = db.Users.FirstOrDefault(u => u.Email == requestDto.Email);
        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public async Task RegisterEmail_WithInvalidEmail_ReturnsBadRequest()
    {
        var requestDto = new RegisterByEmailRequest(
            "invalid-email",
            _ValidPassword);

        var response = await Client.PostAsJsonAsync(_RegisterByEmailEndpoint, requestDto);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
    
    [Test]
    public async Task RegisterEmail_WithInvalidPassword_ReturnsBadRequest()
    {
        var requestDto = new RegisterByEmailRequest(
            _ValidEmail,
            "simple-password");

        var response = await Client.PostAsJsonAsync(_RegisterByEmailEndpoint, requestDto);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
    
    [Test]
    public async Task RegisterEmail_WithInvalidRequest_ReturnsBadRequest()
    {
        var requestDto = new RegisterByEmailRequest(
            "invalid-email",
            "simple-password");

        var response = await Client.PostAsJsonAsync(_RegisterByEmailEndpoint, requestDto);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
    
    [Test]
    public async Task RegisterEmail_WithDuplicateRequest_ReturnsOkAnyway()
    {
        var requestDto = new RegisterByEmailRequest(
            _ValidEmail,
            _ValidPassword);

        var response = await Client.PostAsJsonAsync(_RegisterByEmailEndpoint, requestDto);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        response = await Client.PostAsJsonAsync(_RegisterByEmailEndpoint, requestDto);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}