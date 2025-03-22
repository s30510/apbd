namespace APBD3;

public class Cargo
{
    public enum Type
    {
        LIQUID,GAS,FROZEN
    }
    

    public string cargoName;
    public Type _type;
    public double weight;
    public bool dangerous;
    
    


    public Cargo(string cargoName, Type type,double weight, bool dangerous)
    {
        this.cargoName = cargoName;
        this._type = type;
        this.weight = weight;
        this.dangerous = dangerous;
    }
}