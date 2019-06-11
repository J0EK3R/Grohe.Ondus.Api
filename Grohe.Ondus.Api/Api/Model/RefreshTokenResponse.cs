using Newtonsoft.Json;
using System;

namespace Grohe.Ondus.Api.Model
{
  public class RefreshTokenResponse
  {
    [JsonProperty("access_token")]
    public string accessToken;
    [JsonProperty("refresh_token")]
    public string refreshToken;
    [JsonProperty("expires_in")]
    public int expiresIn;
    [JsonProperty]
    private readonly DateTime createdAt = DateTime.Now;

    #region expiresAt()
    public DateTime expiresAt()
    {
      return createdAt.AddSeconds(expiresIn);
    }
    #endregion
  }
}
