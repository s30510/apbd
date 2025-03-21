namespace APBD3;

public class LiquidContainer : Container, IHazardNotifier
{
    public LiquidContainer(double weigth, double height, double singleWeigth, double deep, string serialNumber, double maxWeigth) : base(weigth, height, singleWeigth, deep, serialNumber, maxWeigth)
    {
        
    }
    
    public override void LoadContainer(double weigth)
    {
        base.LoadContainer(weigth);
        bool dangerous = false;

        if (dangerous)
        {
            if (weigth > MaxWeight * 0.5)
            {
                Weight = MaxWeight*0.5;
                Warn(SerialNumber);
            }
        }
        else
        {
            if (weigth > MaxWeight * 0.9)
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