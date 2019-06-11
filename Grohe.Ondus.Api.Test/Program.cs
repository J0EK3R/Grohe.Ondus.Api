using Grohe.Ondus.Api.Model;
using System;
using System.Collections.Generic;

namespace Grohe.Ondus.Api.Test
{
  class Program
  {
    static void Main(string[] args)
    {
      // The username of the GROHE account
      string userName = "";
      // The password of the GROHE account
      string password = "";

      OndusService ondusService = OndusService.login(userName, password);

      //string refresh = ondusService.refreshAuthorization();
      //DateTime expireDate = ondusService.authorizationExpiresAt();

      List<Location> locationList = ondusService.getLocations();

      foreach (Location currentLocation in locationList)
      {
        Console.WriteLine(currentLocation);

        List<Room> roomList = ondusService.getRooms(currentLocation);

        foreach (Room currentRoom in roomList)
        {
          Console.WriteLine(currentRoom);

          List<BaseAppliance> applianceList = ondusService.getAppliances(currentRoom);

          foreach (BaseAppliance currentAppliance in applianceList)
          {
            switch (currentAppliance.getType())
            {
              case SenseAppliance.TYPE:
                SenseAppliance senseAppliance = ondusService.getAppliance(currentRoom, currentAppliance.getApplianceId()) as SenseAppliance;

                Console.WriteLine(senseAppliance);

                break;
              case SenseGuardAppliance.TYPE:
                SenseGuardAppliance senseGuardAppliance = ondusService.getAppliance(currentRoom, currentAppliance.getApplianceId()) as SenseGuardAppliance;

                Console.WriteLine(senseGuardAppliance);

                ApplianceCommand applianceCommand = ondusService.getApplianceCommand(senseGuardAppliance);

                Console.WriteLine(applianceCommand);

                //ondusService.setValveOpen(senseGuardAppliance, false);
                //ondusService.setValveOpen(senseGuardAppliance, true);

                break;
            }

            ApplianceStatus applianceStatus = ondusService.getApplianceStatus(currentAppliance);
            Console.WriteLine(applianceStatus);

            BaseApplianceData baseApplianceData = ondusService.getApplianceData(currentAppliance, DateTime.Now-TimeSpan.FromDays(1), DateTime.Now);
            Console.WriteLine(baseApplianceData);
          }
        }
      }
    }
  }
}
