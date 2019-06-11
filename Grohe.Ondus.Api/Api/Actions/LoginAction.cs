using Grohe.Ondus.Api.Client;
using Grohe.Ondus.Api.Model;
using Newtonsoft.Json;
using System;

namespace Grohe.Ondus.Api.Actions
{
  public class LoginAction : AbstractionAction
  {
    #region Constants
    private const string LOGIN_URL = "/v2/iot/auth/users/login";
    #endregion

    #region class LoginRequest
    public class LoginRequest
    {
      [JsonProperty]
      private string username;
      [JsonProperty]
      private string password;

      public LoginRequest(string username, string password)
      {
        this.username = username;
        this.password = password;
      }
    }
    #endregion

    #region getToken(String username, String password)
    public String getToken(String username, String password)
    {
      ApiResponse<Authentication> authResponse = getApiClient()
        .post<Authentication>(LOGIN_URL, new LoginRequest(username, password));

      if (authResponse.getStatusCode() == 441)
      {
        throw new ApplicationException("441 - Unauthorized");
      }

      //return authResponse.getContent()
      //              .orElseThrow(()-> new IllegalArgumentException(
      //                      String.format("Unknown response with code %d", authResponse.getStatusCode())))
      //              .getToken();
      return authResponse.getContent().token;
    }
    #endregion
  }
}
