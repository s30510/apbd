namespace APBD3;

public class Cargo(
    string cargoName,
    Cargo.Type cargoType,
    double weight,
    bool dangerous,
    double requiredTemperature = 0)
{
    public enum Type
    {
        Liquid,
        Gas,
        Frozen,
        None
    }

    public string CargoName = cargoName;
    public Type CargoType = cargoType;
    public double Weight = weight;
    public bool Dangerous = dangerous;
    public double RequiredTemperature = requiredTemperature;
}