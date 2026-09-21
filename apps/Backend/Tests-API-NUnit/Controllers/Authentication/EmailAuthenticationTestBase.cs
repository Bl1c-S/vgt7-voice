using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using API_NUnit_Tests.Infrastructure;
using API.Models.Requests;
using NUnit.Framework.Constraints;

namespace API_NUnit_Tests.Controllers.Authentication;

public abstract class EmailAuthenticationTestBase : IntegrationTestBase
{
    private const string _RegisterByEmailEndpoint = "api/auth/email/register";
    private const string _LoginByEmailEndpoint = "api/auth/email/login";

    protected const string ValidEmail = "testuser@example.com";
    protected const string ValidPassword = "StrongPassword123!";

    protected const string AccessTokenName = "accessToken";
    protected const string RefreshTokenName = "refreshToken";

    protected async Task RegisterTest(RegisterByEmailRequest requestDto,
        HttpStatusCode expectedStatusCode)
    {
        var response = await Client.PostAsJsonAsync(_RegisterByEmailEndpoint, requestDto);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode),
            $"API returned {response.StatusCode}. Response Body/Error Details: {responseContent}");
    }

    /// <summary>
    /// Assert response status will be skipped.
    /// </summary>
    protected async Task RegisterTest(RegisterByEmailRequest requestDto)
    {
        var response = await Client.PostAsJsonAsync(_RegisterByEmailEndpoint, requestDto);
    }

    protected async Task<HttpResponseMessage> LoginTest(LoginByEmailRequest requestDto,
        HttpStatusCode expectedStatusCode)
    {
        var response = await Client.PostAsJsonAsync(_LoginByEmailEndpoint, requestDto);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode),
            $"API returned {response.StatusCode}. Response Body/Error Details: {responseContent}");

        return response;
    }

    protected void TokenTest(JsonElement tokens, string tokenName, IResolveConstraint expectedResolveConstraint)
    {
        var token = tokens.GetProperty(tokenName).GetProperty("value").GetString();
        Assert.That(token, expectedResolveConstraint);
    }

    protected async Task<JsonElement> GetTokens(HttpResponseMessage response)
    {
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var tokens = body.RootElement.GetProperty("tokens").Clone();
        return tokens;
    }
}