
namespace APBD3;

public class ContainerShip(string name, double maxSpeed, int maxContainersCount, double maxTotalWeight)
{
    private string Name { get; set; } = name; 
    private List<Container> Containers {get; set;} = []; 
    private double MaxSpeed{get; set; } = maxSpeed;
    private int MaxContainersCount {get; set; } = maxContainersCount;
    private double MaxTotalWeight{get; set; } = maxTotalWeight;
   
  
    
    public void Load(Container container)
    {
        if (Containers.Count >= MaxContainersCount)
        {
            throw new OverfillException("Containers count is greater than maxContainersCount.");
        }
    
        Containers.Add(container);
    }

    public void Drop(string containerId)
    {
        for (var index = 0; index < Containers.Count; index++)
        {
            if (Containers[index].SerialNumber == containerId)
            {
                Containers.Remove(Containers[index]);
                
            }
        }
    }

    public void Replace( Container container,string containerId)
    {
        for (var index = 0; index < Containers.Count; index++)
        {
            if (Containers.ToList()[index].SerialNumber == containerId)
            {
                Containers[index] = container;
            }
        }
    }

    public void Move(ContainerShip containerShip, string containerId)
    {
        for (var index = 0; index < Containers.Count; index++)
        {
            if (Containers[index].SerialNumber == containerId)
            {
                containerShip.Load(Containers[index]);
                Containers.Remove(Containers[index]);
               
            }
        }
        
    }

    public override string ToString()
    {
        string containersString = "";

        for (var index = 0; index < Containers.Count; index++)
        {
            containersString += Containers[index] + " ";
           
        }
        
        return name + "\nMax speed: " + MaxSpeed + "\nContainers count: " +Containers.Count+"/"+ MaxContainersCount
            + "\nMax total weight: " + MaxTotalWeight + "\nContainers: " + containersString;
    }
}