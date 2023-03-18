using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Tests;

public class StringListControllerV1Tests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public StringListControllerV1Tests(WebApplicationFactory<Program> factory)
    {
        const string serviceUrl = "https://localhost:7114/";
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri(serviceUrl);
    }

    [Fact]
    public async Task GivenDefaultCall_WhenNoVersion_ThenReturnStringStartingWithB()
    {
        string json = await _httpClient.GetStringAsync("/api/StringList");
        JArray? strings = JArray.Parse(json);
        Assert.Equal(2, strings.Count);
        Assert.StartsWith("B", (string)strings[0]);
        Assert.StartsWith("B", (string)strings[1]);
    }

    [Fact]
    public async Task GivenQueryString_WhenCalledV1_ThenReturnStringStartingWithB()
    {
        string json = await _httpClient.GetStringAsync("/api/StringList?api-version=1.0");
        JArray? strings = JArray.Parse(json);
        Assert.Equal(2, strings.Count);
        Assert.StartsWith("B", (string)strings[0]);
        Assert.StartsWith("B", (string)strings[1]);
    }
}