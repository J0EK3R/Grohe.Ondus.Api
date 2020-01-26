using Grohe.Ondus.Api.Actions;
using Grohe.Ondus.Api.Client;
using Grohe.Ondus.Api.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Grohe.Ondus.Api
{
  public class WebFormLogin
  {
    #region Constants
    private const string LOGIN_URL = "/v3/iot/oidc/login";
    private static readonly Regex ACTION_PATTERN = new Regex("action=\"([^\"]*)\"");
    #endregion

    #region Fields
    private RefreshTokenResponse refreshTokenResponse;
    private readonly HttpClientHandler clientHandler;
    private readonly HttpClient httpClient;
    private readonly CookieContainer cookieContainer;
    private readonly string baseUrl;
    private readonly string username;
    private readonly string password;
    #endregion

    #region OndusService()
    /// <summary>
    /// Private Constructor
    /// </summary>
    public WebFormLogin(string baseUrl, String username, String password)
    {
      this.baseUrl = baseUrl;
      this.username = username;
      this.password = password;

      this.cookieContainer = new CookieContainer();
      this.clientHandler = new HttpClientHandler() { CookieContainer = this.cookieContainer };
      this.httpClient = new HttpClient(this.clientHandler);
    }
    #endregion

    public RefreshTokenResponse login()
    {
      HttpRequestMessage get = new HttpRequestMessage(HttpMethod.Get, baseUrl + LOGIN_URL);

      Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(get, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
      HttpResponseMessage httpResponse = httpRequest.Result;
      HttpStatusCode statusCode = httpResponse.StatusCode;
      HttpContent responseContent = httpResponse.Content;

      //ReadCookies(httpResponse);

      // this.statusCode = httpResponse.getStatusLine().getStatusCode();
      int statuscode = (int)httpResponse.StatusCode;

      //HttpEntity responseEntity = httpResponse.getEntity();

      string page;
      try
      {
        if (statuscode != 200)
        {
          page = null;
        }
        else
        {
          //extractContentFromResponse(httpResponse);
          Task<string> taskHttpRequest = httpResponse.Content.ReadAsStringAsync();
          page = taskHttpRequest.Result;
          string action = formTargetOf(page);
          return login(action);
        }
      }
      finally
      {
        //EntityUtils.consume(responseEntity);
      }
      return null;
    }

    private RefreshTokenResponse login(String actionUrl)
    {
      FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
{
          new KeyValuePair<string, string>("username", username),
          new KeyValuePair<string, string>("password", password),
      });

      HttpRequestMessage postConnection = new HttpRequestMessage(HttpMethod.Post, actionUrl);
      postConnection.Content = content;

      postConnection.Properties.Add("X-Requested-With", "XMLHttpRequest");
      postConnection.Properties.Add("referer", actionUrl);
      postConnection.Properties.Add("origin", baseUrl);

      Task<HttpResponseMessage> httpRequest = this.httpClient.SendAsync(postConnection, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
      HttpResponseMessage response = httpRequest.Result;
      HttpStatusCode statusCode = response.StatusCode;
      HttpContent responseContent = response.Content;

      switch (statusCode)
      {
        case HttpStatusCode.OK:
        case HttpStatusCode.Found:
          break;
        default:
          throw new ApplicationException($"Unknown response with code {statusCode}");
      }

      // change scheme from ondus:// to https://
      Uri location = new UriBuilder(response.Headers.Location) { Scheme = Uri.UriSchemeHttps }.Uri;

      HttpRequestMessage getConnection = new HttpRequestMessage(HttpMethod.Get, location);
      httpRequest = this.httpClient.SendAsync(getConnection, HttpCompletionOption.ResponseContentRead, CancellationToken.None);

      response = httpRequest.Result;
      statusCode = response.StatusCode;
      responseContent = response.Content;

      Task<string> httpRequestTask = response.Content.ReadAsStringAsync();
      string contentR = httpRequestTask.Result;

      return JsonConvert.DeserializeObject<RefreshTokenResponse>(contentR);
    }

    #region formTargetOf(String page)
    private String formTargetOf(String page)
    {
      MatchCollection matches = ACTION_PATTERN.Matches(page);

      if (matches.Count > 0)
      {
        //return StringEscapeUtils.unescapeHtml4(matches[1].Value);
        return WebUtility.HtmlDecode(matches[0].Groups[1].Value);
      }
      throw new ApplicationException("Unexpected result from Grohe API (login form target url not found)");
    }
    #endregion

  }
}
