namespace APBD3;

public class ContainerShip(string name, double maxSpeed, int maxContainersCount, double maxTotalWeight)
{
    private string Name { get; set; } = name;
    private List<Container> Containers { get; set; } = [];
    private double MaxSpeed { get; set; } = maxSpeed;
    private int MaxContainersCount { get; set; } = maxContainersCount;
    private double MaxTotalWeight { get; set; } = maxTotalWeight;


    public void Load(Container container)
    {
        if (Containers.Count >= MaxContainersCount)
        {
            throw new OverfillException("Containers count is greater than maxContainersCount.");
        }

        if (GetTotalWeight() > MaxTotalWeight)
        {
            throw new OverfillException("Containers weight is greater than maxTotalWeight.");
        }

        Containers.Add(container);
    }
    
    
    public void Drop(string containerId)
    {
        for (var index = 0; index < Containers.Count; index++)
        {
            if (Containers[index].GetSerialNumber() == containerId)
            {
                Containers.Remove(Containers[index]);
            }
        }
    }

    public void Replace(Container container1, Container container2)
    {
        bool exist = false;
        int tempIndex=0; 
        
        for (var index = 0; index < Containers.Count; index++)
        {
            if (Containers[index].GetSerialNumber() == container2.GetSerialNumber())
            {
             Console.WriteLine("Kontener istenieje");
             exist = true;
             tempIndex = index;
            }
        }
        
        
        for (var index = 0; index < Containers.Count; index++)
        {
            if (Containers[index].GetSerialNumber() == container1.GetSerialNumber())
            {
                Containers[index] = container2;
            }
            
        }

        if (exist)
        {
            Containers[tempIndex] = container1;
        }
    }

    public void Move(ContainerShip containerShip, string containerId)
    {
        for (var index = 0; index < Containers.Count; index++)
        {
            if (Containers[index].GetSerialNumber()== containerId)
            {
                containerShip.Load(Containers[index]);
                Containers.Remove(Containers[index]);
            }
        }
    }

    public double GetTotalWeight()
    {
        double totalWeight = 0;
        for (int i = 0; i < Containers.Count(); i++)
        {
            totalWeight += Containers[i].GetTotalWeight();
        }

        return totalWeight / 1000;
    }

    public override string ToString()
    {
        string containersString = "";

        for (var index = 0; index < Containers.Count; index++)
        {
            containersString += Containers[index] + " ";
        }

        return "|" + Name + "|\nMax speed: " + MaxSpeed + " knots \nContainers count: " + Containers.Count + "/" +
               MaxContainersCount
               + "\nTotal weight: " + GetTotalWeight() + "/" + MaxTotalWeight + " t\nContainers: " + containersString;
    }
}