using Grohe.Ondus.Api.Client;
using Grohe.Ondus.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grohe.Ondus.Api.Actions
{
  public class LocationAction : AbstractionAction
  {
    #region Constants
    private const string LOCATIONS_URL = "iot/locations";
    //private const string LOCATION_URL_TEMPLATE = "iot/locations/%d";
    private const string LOCATION_URL_TEMPLATE = "iot/locations/{0}";
    #endregion

    #region getLocations()
    public List<Location> getLocations()
    {
      ApiResponse<Location[]> locationsResponse = getApiClient()
        .get<Location[]>(getApiClient().apiPath() + LOCATIONS_URL);

      if (locationsResponse.getStatusCode() != 200)
      {
        //return Collections.emptyList();
        return new List<Location>();
      }

      //return Arrays.asList(locationsResponse.getContent().orElseGet(()-> new Location[] { }));
      return (locationsResponse.getContent() ?? new Location[] { }).ToList();
    }
    #endregion
    #region getLocation(int id)
    public Location getLocation(int id)
    {
      ApiResponse<Location> locationApiResponse = getApiClient()
        .get<Location>(String.Format(getApiClient().apiPath() + LOCATION_URL_TEMPLATE, id));

      if (locationApiResponse.getStatusCode() != 200)
      {
        return null;
      }
      return locationApiResponse.getContent();
    }
    #endregion
  }
}
