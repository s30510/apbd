namespace APBD3;

public class GasContainer : Container, IHazardNotifier

{
    public static int GCounter; 
   private double Pressure;
    public GasContainer( int height, int singleWeigth, int deep, int maxWeigth) : base( height, singleWeigth, deep,  maxWeigth)
    {
        GenerateSerialNumber();
    }

    
    public void GenerateSerialNumber()
    {
        SerialNumber = $"KON-G-{GCounter++}"; ;
    }
    public override void DropContianer()
    {
       Weight *= 0.05;
    }

    public override void LoadContainer(Cargo cargo)
    {
        base.LoadContainer(cargo);
        bool dangerous = false;

        if (dangerous)
        {
           Warn(SerialNumber);
        }
       
    }

    public void Warn(string number)
    {
        Console.WriteLine($"Warning container : {number}");
    }
}