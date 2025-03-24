namespace APBD3;

public class LiquidContainer : Container, IHazardNotifier
{
    public static int LCounter; 
    public LiquidContainer( double height, double singleWeigth, double deep, double maxWeigth) : base( height, singleWeigth, deep, maxWeigth)
    {
        GenerateSerialNumber();
    }
    
    public void GenerateSerialNumber()
    {
        SerialNumber = $"KON-L-{LCounter++}"; ;
    }

    public override void LoadContainer(Cargo loadedCargo)
    {
        if (loadedCargo.CargoType == Cargo.Type.Liquid)
        {

            base.LoadContainer(loadedCargo);
            bool dangerous =loadedCargo.Dangerous;

            if (dangerous)
            {
                if (GetCargoWeight() > MaxWeight * 0.5)
                {
                    NotifyHazard(GetSerialNumber(),"Dangerous operation detected");
                }
            }
            else
            {
                if (GetCargoWeight() > MaxWeight * 0.9)
                {
                    NotifyHazard(GetSerialNumber(),"Dangerous operation detected");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid cargo type");
        }
    }
    
    public void NotifyHazard(string containerNumber, string message)
    {
        Console.WriteLine("{0}: {1}", containerNumber, message);
    }
}