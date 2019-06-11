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
    private const string HEADER_CONTENT_TYPE = "Content-Type";
    private const string HEADER_AUTHORIZATION = "Authorization";
    private const string CONTENT_TYPE_JSON = "application/json";
    #endregion

    #region Fields
    private string baseUrl;
    private HttpClient httpClient;

    private string token;
    private Version version = Version.v2;
    #endregion

    #region ApiClient(string baseUrl)
    public ApiClient(string baseUrl)
      : this(baseUrl, new HttpClient())
    {
    }
    #endregion
    #region ApiClient(string baseUrl, HttpClient httpClient)
    private ApiClient(string baseUrl, HttpClient httpClient)
    {
      this.baseUrl = baseUrl;
      this.httpClient = httpClient;
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
      // HttpGet get = new HttpGet(baseUrl + requestUrl);
      // get.setHeader(HEADER_AUTHORIZATION, authorization());
      // HttpResponse response = httpClient.execute(get);

      HttpRequestMessage get = new HttpRequestMessage(HttpMethod.Get, baseUrl + requestUrl);
      get.Headers.Add(HEADER_AUTHORIZATION, authorization());

      Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(get, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
      HttpResponseMessage response = httpRequest.Result;
      HttpStatusCode statusCode = response.StatusCode;
      HttpContent responseContent = response.Content;

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

      if (version.Equals(Version.v3))
      {
        return "Bearer " + token;
      }

      return token;
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
      // HttpPost post = new HttpPost(baseUrl + requestUrl);
      HttpRequestMessage post = new HttpRequestMessage(HttpMethod.Post, baseUrl + requestUrl);

      // String serializedParameters = mapper.writeValueAsString(body);
      string serializedParameters = JsonConvert.SerializeObject(body);

      // post.setHeader(HEADER_CONTENT_TYPE, CONTENT_TYPE_JSON);
      post.Content = new StringContent(serializedParameters, Encoding.UTF8, CONTENT_TYPE_JSON);

      if (authorization() != null)
      {
        // post.setHeader(HEADER_AUTHORIZATION, authorization());
        post.Headers.Add(HEADER_AUTHORIZATION, authorization());
      }

      // HttpResponse response = httpClient.execute(post);
      Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(post, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
      HttpResponseMessage response = httpRequest.Result;
      HttpStatusCode statusCode = response.StatusCode;
      HttpContent responseContent = response.Content;
      
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
      if (version.Equals(Version.v2))
      {
        return "/v2/";
      }
      return "/v3/";
    }
    #endregion
  }
}
