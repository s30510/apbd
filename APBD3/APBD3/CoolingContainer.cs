namespace APBD3;

public class CoolingContainer : Container, IHazardNotifier
{
    public static int CCounter;
    public CoolingContainer( int height, int singleWeigth, int deep,  int maxWeigth) : base( height, singleWeigth, deep, maxWeigth)
    {
        GenerateSerialNumber();
    }
    
    public void GenerateSerialNumber()
    {
        SerialNumber = $"KON-C-{CCounter++}"; ;
    }

    public void Warn(string number)
    {
        throw new NotImplementedException();
    }
}