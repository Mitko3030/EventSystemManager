using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class ParkingController
{
    private List<ParkingSpot> parkingSpots;

    public ParkingController()
    {
        parkingSpots = new List<ParkingSpot>();
    }

    public string CreateParkingSpot(List<string> args)
    {
        int id = int.Parse(args[0]);

        if (parkingSpots.Any(p => p.Id == id))
            return $"Parking spot {id} is already registered!";


        ParkingSpot spot;

        string type = args[2];
        bool occupied = bool.Parse(args[1]);
        double price = double.Parse(args[3]);

        try
        {
            if (type == "car")
                spot = new CarParkingSpot(id, occupied, price);
            else if (type == "bus")
                spot = new BusParkingSpot(id, occupied, price);
            else if (type == "subscription")
            {
                string registrationPlate = args[4];
                spot = new SubscriptionParkingSpot(id, occupied, price, registrationPlate);
            }
            else
                return "Unable to create parking spot!";

            parkingSpots.Add(spot);
            return $"Parking spot {spot.Id} was successfully registered in the system!";
        }
        catch
        {
            return "Unable to create parking spot!";
        }

    }

    public string ParkVehicle(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        string registrationPlate = args[1];
        int hoursParked = int.Parse(args[2]);
        string type = args[3];

        ParkingSpot spot = parkingSpots.FirstOrDefault(p => p.Id == parkingSpotId);
        if (spot == null)
            return $"Parking spot {parkingSpotId} not found!";


        bool parked = spot.ParkVehicle(registrationPlate, hoursParked, type);

        if (parked)
            return $"Vehicle {registrationPlate} parked at {parkingSpotId} for {hoursParked} hours.";
        else
            return $"Vehicle {registrationPlate} can't park at {parkingSpotId}.";

    }

    public string FreeParkingSpot(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        ParkingSpot spot = parkingSpots.FirstOrDefault(p => p.Id == parkingSpotId);

        if (spot == null)
            return $"Parking spot {parkingSpotId} not found!";

        if (!spot.Occupied)
            return $"Parking spot {parkingSpotId} is not occupied.";

        spot.Occupied = false;
        return $"Parking spot {parkingSpotId} is now free!";

    }

    public string GetParkingSpotById(List<string> args)
    {
        
        int parkingSpotId = int.Parse(args[0]);
        ParkingSpot spot = parkingSpots.FirstOrDefault(p => p.Id == parkingSpotId);

        if (spot == null)
            return $"Parking spot {parkingSpotId} not found!";

        return spot.ToString();
    

    }

    public string GetParkingIntervalsByParkingSpotIdAndRegistrationPlate(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        string registrationPlate = args[1];

        ParkingSpot spot = parkingSpots.FirstOrDefault(p => p.Id == parkingSpotId);
        if (spot == null)
            return $"Parking spot {parkingSpotId} not found!";

        List<ParkingInterval> intervals = spot.GetAllParkingIntervalsByRegistrationPlate(registrationPlate);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Parking intervals for parking spot {parkingSpotId}:");
        foreach (var interval in intervals)
        {
            sb.AppendLine(interval.ToString());
        }

        return sb.ToString().Trim();
    }


    public string CalculateTotal(List<string> args)
    {
        double total = parkingSpots.Sum(p => p.CalculateTotal());
        return $"Total revenue from the parking: {total:F2} BGN";
    }

}

