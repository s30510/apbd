namespace APBD3;

public class Container
{
    public Container(double weigth, double height, double singleWeigth, double deep, string serialNumber, double maxWeigth)
    {
        Weigth = weigth;
        Height = height;
        SingleWeigth = singleWeigth;
        Deep = deep;
        SerialNumber = serialNumber;
        MaxWeigth = maxWeigth;
        LoadContainer(weigth);
    }

    protected double Weigth { get; set; } 
    protected double Height { get; set; } 
    protected double SingleWeigth { get; set; }
    protected double Deep { get; set; }
    protected string SerialNumber { get; set; }
    protected double MaxWeigth { get; set; }

    public virtual void DropContianer()
    {
        this.Weigth = 0;
    }

    public virtual void LoadContainer(double weigth)
    {
        Weigth += weigth;
        if (Weigth > MaxWeigth)
        {
            throw new OverflowException("Weigth is greater than maximum weigth");
        }
    }
    
    
    
    



}