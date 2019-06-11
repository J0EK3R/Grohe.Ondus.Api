using Grohe.Ondus.Api.Client;

namespace Grohe.Ondus.Api.Actions
{
  public class AbstractionAction: 
    IAction
  {
    #region Fields
    private ApiClient apiClient;
    #endregion

    #region getApiClient()
    public ApiClient getApiClient()
    {
      return apiClient;
    }
    #endregion
    #region setApiClient(ApiClient apiClient)
    public void setApiClient(ApiClient apiClient)
    {
      this.apiClient = apiClient;
    }
    #endregion
  }
}
