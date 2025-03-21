
using APBD3;

Container container1 = new Container(5,10,1,50,"x",100);
Container container2 = new Container(5,10,1,50,"y",100);
Container container3 = new Container(5,10,1,50,"z",100);


ContainerShip containerShip = new ContainerShip("Sofia", 23, 100, 23);

containerShip.Load(container1);
containerShip.Load(container2);
containerShip.Load(container3);

Console.WriteLine(containerShip);



