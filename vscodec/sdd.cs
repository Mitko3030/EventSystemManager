int a = int.Parse(Console.ReadLine());
string name = Console.ReadLine();
Ant ant = new Ant(name, a);

Console.WriteLine(ant.name + " " + ant.age);