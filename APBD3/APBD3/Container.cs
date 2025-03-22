namespace APBD3;

abstract 
    public class Container
{
 
    public double Weight { get; set; }
    public double Height { get; set; }
    public double SingleWeight { get; set; }
    public double Deep { get; set; }
    public string SerialNumber { get; set; }
    public double MaxWeight { get; set; }

   
    
    
    public Container( double height, double singleWeigth, double deep, double maxWeigth)
    {
    
        Height = height;
        SingleWeight = singleWeigth;
        Deep = deep;
        MaxWeight = maxWeigth;
       
        
    }
    

    public virtual void DropContianer()
    {
        this.Weight = 0;
        
    }

    public virtual void LoadContainer(Cargo cargo)
    {
        Weight += cargo.weight;
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