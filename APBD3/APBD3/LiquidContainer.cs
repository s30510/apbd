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
    
    public override void LoadContainer(Cargo cargo)
    {
        base.LoadContainer(cargo);
        bool dangerous = false;

        if (dangerous)
        {
            if (cargo.weight > MaxWeight * 0.5)
            {
                Weight = MaxWeight*0.5;
                Warn(SerialNumber);
            }
        }
        else
        {
            if (cargo.weight > MaxWeight * 0.9)
            {
                Weight = MaxWeight*0.9;
                
            }
        }
        
    }

    public void Warn(string number)
    {
        Console.WriteLine($"Warning container : {number}");
    }


}