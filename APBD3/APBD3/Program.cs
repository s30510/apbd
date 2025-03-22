using APBD3;


Cargo cargo1 = new Cargo("Milk",Cargo.Type.LIQUID,600,false);
Cargo cargo2 = new Cargo("Fuel",Cargo.Type.LIQUID,550,true);
Cargo cargo3 = new Cargo("Ice cream",Cargo.Type.FROZEN,400,false);
Cargo cargo4 = new Cargo("Dry ice",Cargo.Type.FROZEN,300,true);
Cargo cargo5 = new Cargo("Oxygen", Cargo.Type.GAS,450, true);
Cargo cargo6 = new Cargo("Helium", Cargo.Type.GAS,600, false);


LiquidContainer liquidContainer1 = new LiquidContainer(100, 50, 100, 10000);
LiquidContainer liquidContainer2 = new LiquidContainer( 100, 50, 100, 10000);
LiquidContainer liquidContainer3 = new LiquidContainer( 100, 50, 100, 10000);

GasContainer gasContainer1 = new GasContainer( 100, 50, 100 ,10000);
GasContainer gasContainer2 = new GasContainer( 100, 50, 100, 10000);
GasContainer gasContainer3 = new GasContainer(100, 50, 100, 10000);

CoolingContainer coolingContainer1 = new CoolingContainer( 100, 50, 100, 10000);
CoolingContainer coolingContainer2 = new CoolingContainer( 100, 50, 100, 10000);
CoolingContainer coolingContainer3 = new CoolingContainer( 100, 50, 100, 10000);
CoolingContainer coolingContainer4 = new CoolingContainer( 100, 50, 100, 10000);


ContainerShip containerShip1 = new ContainerShip("Burgas", 23, 10, 23);
ContainerShip containerShip2 = new ContainerShip("Plovdiv", 23, 10, 23);

containerShip1.Load(liquidContainer1);
containerShip1.Load(liquidContainer2);
containerShip1.Load(liquidContainer3);
containerShip1.Load(gasContainer1);
containerShip1.Load(gasContainer2);
containerShip1.Load(gasContainer3);
containerShip1.Load(coolingContainer1);
containerShip1.Load(coolingContainer2);
containerShip1.Load(coolingContainer3);

containerShip1.Move(containerShip2,liquidContainer2.SerialNumber);
containerShip1.Move(containerShip2, gasContainer1.SerialNumber);

containerShip1.Drop(liquidContainer1.SerialNumber);



Console.WriteLine(containerShip1);
Console.WriteLine();
Console.WriteLine(containerShip2);