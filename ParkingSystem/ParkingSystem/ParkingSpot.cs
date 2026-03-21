using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class ParkingSpot
{
    private int id;
    private bool occupied;
    private string type;
    private double price;
    protected List<ParkingInterval> parkingIntervals;


    protected ParkingSpot(int id, bool occupied, string type, double price)
    {
        this.Id = id;
        this.Occupied = occupied;
        this.Type = type;
        this.Price = price;
        parkingIntervals = new List<ParkingInterval>();
    }
    public int Id
    {
        get
        {
            return this.id;
        }

        set
        {
            this.id = value;
        }
    }

    public bool Occupied
    {
        get
        {
            return this.occupied;
        }

        set
        {
            this.occupied = value;
        }
    }

    public string Type
    {
        get
        {
            return this.type;
        }

        set
        {
            this.type = value;
        }

    }

    public double Price
    {
        get
        {
            return this.price;
        }

        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("greshka");
            }
            else
            {
                this.price = value;
            }
        }
    }



    public virtual bool ParkVehicle(string registrationPlate, int hoursParked, string type)
    {
        if (Occupied || type != Type)
            return false;

        ParkingInterval interval = new ParkingInterval(this, registrationPlate, hoursParked);
        parkingIntervals.Add(interval);
        Occupied = true;
        return true;
    }


    public List<ParkingInterval> GetAllParkingIntervalsByRegistrationPlate(string registrationPlate)
    {
        return parkingIntervals.Where(pinterval => pinterval.RegistrationPlate == registrationPlate).ToList();
    }

    public virtual double CalculateTotal()
    {
        return parkingIntervals.Sum(pi => pi.Revenue);
    }
    

    public override string ToString()
    {
        return $"> Parking Spot #{Id}\n> Occupied: {Occupied}\n> Type: {Type}\n> Price per hour: {Price:f2} BGN";
    }

}
