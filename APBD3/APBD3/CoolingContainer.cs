namespace APBD3;

public class CoolingContainer : Container
{
    public static int CCounter;
    public string ProductType ="";
    public double Temperature;
    
    public CoolingContainer( int height, int singleWeight, int deep,  int maxWeight, double temperature) : base( height, singleWeight, deep, maxWeight)
    {
        Temperature = temperature;
        GenerateSerialNumber();
    }
    
    public void GenerateSerialNumber()
    {
        SerialNumber = $"KON-C-{CCounter++}"; ;
    }

    public override void LoadContainer(Cargo loadedCargo)
    {
        if (loadedCargo.CargoType == Cargo.Type.Frozen)
        {
            if (Temperature < loadedCargo.RequiredTemperature)
            {
                Console.WriteLine("The temperature of the container cannot be lower than the temperature required");
                return;
            }
            
            base.LoadContainer(loadedCargo);
            ProductType = loadedCargo.CargoName;
        }
        else
        {
            Console.WriteLine("Invalid cargo type");
        }

    }
}