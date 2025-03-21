namespace APBD3;

public class Container
{
    public double Weight { get; set; }
    public double Height { get; set; }
    public double SingleWeight { get; set; }
    public double Deep { get; set; }
    public string SerialNumber { get; set; }
    public double MaxWeight { get; set; }

   
    
    
    public Container(double weigth, double height, double singleWeigth, double deep, string serialNumber, double maxWeigth)
    {
        Weight = weigth;
        Height = height;
        SingleWeight = singleWeigth;
        Deep = deep;
        SerialNumber = serialNumber;
        MaxWeight = maxWeigth;
        LoadContainer(weigth);
    }
  

    public virtual void DropContianer()
    {
        this.Weight = 0;
        
    }

    public virtual void LoadContainer(double weigth)
    {
        
        Weight += weigth;
        if (Weight > MaxWeight)
        {
            throw new OverflowException("Weigth is greater than maximum weigth");
        }
    }


    public override string ToString()
    {
        return SerialNumber;
    }
}