namespace APBD3;

abstract
    public class Container(double height, double singleWeight, double deep, double maxWeight)
{
    protected double Height = height;
    protected double SingleWeight = singleWeight;
    protected double Deep = deep;
    protected double MaxWeight = maxWeight;
    protected Cargo Cargo = new Cargo("None", Cargo.Type.None, 0, false);

    protected string SerialNumber = "";

    
    public string GetSerialNumber()
    {
        return SerialNumber;
    }


    public double GetTotalWeight()
    {
        return SingleWeight + Cargo.Weight;
    }

    public double GetCargoWeight()
    {
        return Cargo.Weight;
    }

    public void SetCargoWeight(double weight)
    {
        Cargo.Weight = weight;
    }


    public virtual void DropContainer()
    {
        SetCargoWeight(0);
    }

    public virtual void LoadContainer(Cargo loadedCargo)
    {
        if (GetCargoWeight() != 0)
        {
            Console.WriteLine("Container is already loaded");
            return;
        }

        SetCargoWeight(loadedCargo.Weight);

        if (GetCargoWeight() >= MaxWeight)
        {
            SetCargoWeight(0);
            throw new OverfillException("Weigth is greater than maximum weigth");
        }

        Cargo = loadedCargo;
    }


    public override string ToString()
    {
        return SerialNumber + "[" + Cargo.CargoName + "]";
    }
}