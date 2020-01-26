using Grohe.Ondus.Api.Actions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grohe.Ondus.Api.Client
{
  public class ApiClient
  {
    #region enum Version
    public enum Version
    {
      v2,
      v3
    }
    #endregion

    #region Constants
    private const string HEADER_AUTHORIZATION = "Authorization";
    private const string CONTENT_TYPE_JSON = "application/json";
    #endregion

    #region Fields
    private readonly HttpClient httpClient;
    private readonly string baseUrl;

    private string token;
    private Version version = Version.v3;
    #endregion

    #region ApiClient(string baseUrl)
    public ApiClient(string baseUrl)
    {
      this.httpClient = new HttpClient();
      this.baseUrl = baseUrl;
    }
    #endregion

    #region setToken(string token)
    public void setToken(string token)
    {
      this.token = token;
    }
    #endregion
    #region setVersion(Version version)
    public void setVersion(Version version)
    {
      this.version = version;
    }
    #endregion

    #region get<T>(string requestUrl)
    public ApiResponse<T> get<T>(string requestUrl)
      where T : class
    {
      HttpRequestMessage get = new HttpRequestMessage(HttpMethod.Get, baseUrl + requestUrl);
      get.Headers.Add(HEADER_AUTHORIZATION, authorization());

      Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(get, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
      HttpResponseMessage response = httpRequest.Result;

      return new ApiResponse<T>(response);
    }
    #endregion

    #region authorization()
    private string authorization()
    {
      if (token == null)
      {
        return null;
      }

      switch (this.version)
      {
        case Version.v2:
          return token;
        case Version.v3:
          return "Bearer " + token;
        default:
          throw new ApplicationException($"unknown Version {this.version}");
      }
    }
    #endregion

    #region post<T>(string requestUrl)
    public ApiResponse<T> post<T>(string requestUrl)
      where T : class
    {
      return post<T>(requestUrl, null);
    }
    #endregion

    #region post<T>(string requestUrl, object body)
    public ApiResponse<T> post<T>(string requestUrl, object body)
      where T : class
    {
      HttpRequestMessage post = new HttpRequestMessage(HttpMethod.Post, baseUrl + requestUrl);
      string serializedParameters = JsonConvert.SerializeObject(body);
      post.Content = new StringContent(serializedParameters, Encoding.UTF8, CONTENT_TYPE_JSON);

      if (authorization() != null)
      {
        post.Headers.Add(HEADER_AUTHORIZATION, authorization());
      }

      Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(post, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
      HttpResponseMessage response = httpRequest.Result;

      return new ApiResponse<T>(response);
    }
    #endregion

    #region getAction<T>()
    public T getAction<T>()
      where T : IAction, new()
    {
      T action;
      try
      {
        action = new T();
      }
      catch (Exception e)
      {
        throw new ApplicationException(e.ToString());
      }
      action.setApiClient(this);

      return action;
    }
    #endregion

    #region apiPath()
    public string apiPath()
    {
      switch (this.version)
      {
        case Version.v2:
          return "/v2/";
        case Version.v3:
          return "/v3/";
        default:
          throw new ApplicationException($"unknown Version {this.version}");
      }
    }
    #endregion
  }
}
