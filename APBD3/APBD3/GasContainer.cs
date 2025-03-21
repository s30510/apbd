namespace APBD3;

public class GasContainer : Container, IHazardNotifier

{
   private double Pressure;
    public GasContainer(int weigth, int height, int singleWeigth, int deep, string serialNumber, int maxWeigth) : base(weigth, height, singleWeigth, deep, serialNumber, maxWeigth)
    {
    }

    public override void DropContianer()
    {
       Weight *= 0.05;
    }

    public override void LoadContainer(double weigth)
    {
        base.LoadContainer(weigth);
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