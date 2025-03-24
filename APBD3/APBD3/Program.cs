using APBD3;

Cargo cargo1 = new Cargo("Milk",Cargo.Type.Liquid,6000,false);
Cargo cargo2 = new Cargo("Fuel",Cargo.Type.Liquid,5500,true);
Cargo cargo3 = new Cargo("Ice cream",Cargo.Type.Frozen,400,false,-5);
Cargo cargo4 = new Cargo("Dry ice",Cargo.Type.Frozen,300,true,-20);
Cargo cargo5 = new Cargo("Oxygen", Cargo.Type.Gas,900, true);
Cargo cargo6 = new Cargo("Helium", Cargo.Type.Gas,60000, false);

LiquidContainer liquidContainer1 = new LiquidContainer(100, 50, 100, 10000);
LiquidContainer liquidContainer2 = new LiquidContainer( 100, 50, 100, 10000);
LiquidContainer liquidContainer3 = new LiquidContainer( 100, 50, 100, 10000);

liquidContainer2.LoadContainer(cargo1);
liquidContainer2.LoadContainer(cargo1); //próba załadowania dwóch ładunków do jednego kontenera 

liquidContainer3.LoadContainer(cargo2); //wykoannie niebezpiecznej operacji


GasContainer gasContainer1 = new GasContainer( 100, 50, 100 ,10000,200);
GasContainer gasContainer2 = new GasContainer( 100, 50, 100, 10000,200);
GasContainer gasContainer3 = new GasContainer(100, 50, 100, 10000,200);


gasContainer2.LoadContainer(cargo1); // próba załadowania Mleka do kontenera na gaz

// gasContainer2.LoadContainer(cargo6); //error : Overfillexception

gasContainer1.LoadContainer(cargo5);
gasContainer2.LoadContainer(cargo5);


Console.WriteLine("Cargo weight before drop: "+cargo5.Weight);
Console.WriteLine("Container weight before drop: "+gasContainer1.GetTotalWeight());

gasContainer2.DropContainer();

Console.WriteLine("Cargo weight after drop: " + cargo5.Weight);  // pozostawiamy 5% ładunku wewnątrz kontenera
Console.WriteLine("Container weight after drop: "+gasContainer2.GetTotalWeight());


CoolingContainer coolingContainer1 = new CoolingContainer( 100, 50, 100, 10000,-4);
CoolingContainer coolingContainer2 = new CoolingContainer( 100, 50, 100, 10000, -6);
CoolingContainer coolingContainer3 = new CoolingContainer( 100, 50, 100, 10000, -20);
CoolingContainer coolingContainer4 = new CoolingContainer( 100, 50, 100, 10000, -10);

coolingContainer2.LoadContainer(cargo3); // próba załadowania produktu którego temperatura wymagana jest wyższa niż panująca kontenerze


ContainerShip containerShip1 = new ContainerShip("Devil", 26, 10, 23);
ContainerShip containerShip2 = new ContainerShip("Angel", 20, 10, 15);

containerShip1.Load(liquidContainer1);
containerShip1.Load(liquidContainer2);
containerShip1.Load(liquidContainer3);
containerShip1.Load(gasContainer1);
containerShip1.Load(gasContainer2);
containerShip1.Load(gasContainer3);
containerShip1.Load(coolingContainer1);
containerShip1.Load(coolingContainer2);
containerShip1.Load(coolingContainer3);

containerShip1.Move(containerShip2,liquidContainer2.GetSerialNumber());
containerShip1.Move(containerShip2, gasContainer1.GetSerialNumber());

containerShip1.Drop(liquidContainer1.GetSerialNumber());

containerShip1.Replace(gasContainer2,coolingContainer1);

Console.WriteLine();
Console.WriteLine(containerShip1);
Console.WriteLine();
Console.WriteLine(containerShip2);