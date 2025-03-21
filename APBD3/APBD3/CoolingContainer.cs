namespace APBD3;

public class CoolingContainer : Container, IHazardNotifier
{
    
    public CoolingContainer(int weigth, int height, int singleWeigth, int deep, string serialNumber, int maxWeigth) : base(weigth, height, singleWeigth, deep, serialNumber, maxWeigth)
    {
    }

    public void Warn(string number)
    {
        throw new NotImplementedException();
    }
}