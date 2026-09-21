using System.Net;

namespace API_NUnit_Tests.Controllers.Authentication;

public class EmailLoginTests : EmailAuthenticationTestBase
{

    [Test]
    public async Task Login_WithValidCredentials_ReturnsOk()
    {
        await RegisterTest(new(ValidEmail, ValidPassword));
        await LoginTest(new(ValidEmail, ValidPassword), HttpStatusCode.OK);
    }

    [Test]
    public async Task Login_WithValidCredentials_ReturnsAccessAndRefreshTokens()
    {
        await RegisterTest(new(ValidEmail, ValidPassword));
        var response = await LoginTest(new(ValidEmail, ValidPassword), HttpStatusCode.OK);
        var tokens = await GetTokens(response);
        TokenTest(tokens, AccessTokenName, Is.Not.Empty);
        TokenTest(tokens, RefreshTokenName, Is.Not.Empty);
    }

    [Test]
    public async Task LoginEmail_WithDifferentEmailCasing_ReturnsOk()
    {
        await RegisterTest(new(ValidEmail,
            ValidPassword), HttpStatusCode.OK);
        await LoginTest(new(ValidEmail.ToUpperInvariant(), ValidPassword), HttpStatusCode.OK);
    }

    [Test]
    public async Task LoginEmail_WithWrongPassword_ReturnsUnauthorized()
    {
        await RegisterTest(new(ValidEmail, ValidPassword));
        await LoginTest(new(ValidEmail, "WrongPassword123!"), HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task LoginEmail_WithUnknownEmail_ReturnsUnauthorized()
    {
        await RegisterTest(new(ValidEmail, ValidPassword));
        await LoginTest(new("unknown-user@example.com", ValidPassword), HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task LoginEmail_WithEmptyEmail_ReturnsBadRequest()
    {
        await LoginTest(new(string.Empty, ValidPassword), HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task LoginEmail_WithEmptyPassword_ReturnsBadRequest()
    {
        await LoginTest(new(ValidEmail, string.Empty), HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task LoginEmail_WithEmptyRequest_ReturnsBadRequest()
    {
        await LoginTest(new(string.Empty, string.Empty), HttpStatusCode.BadRequest);
    }
}
