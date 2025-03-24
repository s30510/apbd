namespace APBD3;

public class GasContainer : Container, IHazardNotifier
{
    public static int GCounter;
    public double Pressure;

    public GasContainer(int height, int singleWeight, int deep, int maxWeight, double pressure) : base(height,
        singleWeight, deep, maxWeight)
    {
        Pressure = pressure;
        GenerateSerialNumber();
    }


    public void GenerateSerialNumber()
    {
        SerialNumber = $"KON-G-{GCounter++}";
        ;
    }

    public override void DropContainer()
    {
        Cargo.Weight *= 0.05;
    }

    public override void LoadContainer(Cargo loadedCargo)
    {
        if (loadedCargo.CargoType == Cargo.Type.Gas)
        {
            base.LoadContainer(loadedCargo);
            bool dangerous = false;

            if (dangerous)
            {
                NotifyHazard(GetSerialNumber(),"Dangerous operation detected");
            }
        }
        else
        {
            Console.WriteLine("Invalid Cargo type");
        }
    }


    public void NotifyHazard(string containerNumber, string message)
    {
        Console.WriteLine("{0}: {1}", containerNumber, message);
    }
}