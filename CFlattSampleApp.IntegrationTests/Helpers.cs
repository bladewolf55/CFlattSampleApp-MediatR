using System.Runtime.CompilerServices;

namespace CFlattSampleApp.IntegrationTests;

public static class Helpers
{

    /// <summary>
    /// Checks if HttpStatusCode.OK, model not null and expected type, then returns model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static T ValidateHttpResponseModel<T>(this HttpResponseMessage result) where T : class
    {
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var content = result.Content.ReadAsStringAsync().Result;
        var model = System.Text.Json.JsonSerializer.Deserialize<T>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        model.Should().NotBeNull();
        model.Should().BeOfType<T>();
        if (model == null)
            throw new NullReferenceException();
        else
            return model;
    }
}
