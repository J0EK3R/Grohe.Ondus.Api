using Grohe.Ondus.Api.Client;
using Grohe.Ondus.Api.Model;
using Newtonsoft.Json;
using System;

namespace Grohe.Ondus.Api.Actions
{
  public class RefreshTokenAction : AbstractionAction
  {
    #region class RefreshTokenRequest
    public class RefreshTokenRequest
    {
      #region Fields
      [JsonProperty("refresh_token")]
      private String refreshToken;
      #endregion

      #region RefreshTokenRequest(string refreshToken)
      public RefreshTokenRequest(string refreshToken)
      {
        this.refreshToken = refreshToken;
      }
      #endregion
    }
    #endregion

    #region Constants
    private const String REFRESH_URL = "/v3/iot/oidc/refresh";
    #endregion


    #region refresh(string refreshToken)
    public RefreshTokenResponse refresh(string refreshToken)
    {
      ApiResponse<RefreshTokenResponse> refreshTokenResponse = getApiClient()
        .post<RefreshTokenResponse>(REFRESH_URL, new RefreshTokenRequest(refreshToken));

      if (refreshTokenResponse.getStatusCode() == 401)
      {
        throw new ApplicationException("401 - Unauthorized");
      }

      return refreshTokenResponse.getContent();
      //.orElseThrow(()-> new IllegalArgumentException(
      //        String.format("Unknown response with code %d", refreshTokenResponse.getStatusCode())));
    }
    #endregion
  }
}
