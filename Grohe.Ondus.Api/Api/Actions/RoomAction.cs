using Grohe.Ondus.Api.Client;
using Grohe.Ondus.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grohe.Ondus.Api.Actions
{
  public class RoomAction : AbstractionAction
  {
    #region Constants
    //private const string LOCATIONS_URL_TEMPLATE = "iot/locations/%d/rooms";
    private const string LOCATIONS_URL_TEMPLATE = "iot/locations/{0}/rooms";
    //private const string LOCATION_URL_TEMPLATE = "iot/locations/%d/rooms/%d";
    private const string LOCATION_URL_TEMPLATE = "iot/locations/{0}/rooms/{1}";
    #endregion

    #region getRooms(Location forLocation)
    public List<Room> getRooms(Location forLocation)
    {
      ApiResponse<Room[]> roomsResponse = getApiClient()
        .get<Room[]>(String.Format(getApiClient().apiPath() + LOCATIONS_URL_TEMPLATE, forLocation.getId()));

      if (roomsResponse.getStatusCode() != 200)
      {
        return new List<Room>();
      }
      List<Room> rooms = (roomsResponse.getContent() ?? new Room[] { }).ToList();

      //return rooms.stream().peek(room->room.setLocation(forLocation)).collect(Collectors.toList());
      rooms.ForEach(room => room.setLocation(forLocation)); 
      return rooms;
    }
    #endregion
    #region getRoom(Location inLocation, int id)
    public Room getRoom(Location inLocation, int id)
    {
      ApiResponse<Room> roomResponse = getApiClient()
        .get<Room>(String.Format(getApiClient().apiPath() + LOCATION_URL_TEMPLATE, inLocation.getId(), id));

      if (roomResponse.getStatusCode() != 200)
      {
        return null;
      }

      Room roomOptional = roomResponse.getContent();

      //if (roomOptional.isPresent())
      if (roomOptional != null)
      {
        Room room = roomOptional; //.get();
        room.setLocation(inLocation);
        //roomOptional = Optional.of(room);
        roomOptional = room;
      }

      return roomOptional;
    }
    #endregion
  }
}
