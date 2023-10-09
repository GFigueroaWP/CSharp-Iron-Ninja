Buffet buffet = new Buffet();
SweetTooth sweetTooth = new SweetTooth();
SpiceHound spiceHound = new SpiceHound();
while (sweetTooth.IsFull == false)
{
    sweetTooth.Consume(buffet.Serve());
}
while (spiceHound.IsFull == false)
{
    spiceHound.Consume(buffet.Serve());
}
int sweetToothItemCount = sweetTooth.ConsumptionHistory.Count;
int spiceHoundItemCount = spiceHound.ConsumptionHistory.Count;

if (sweetToothItemCount > spiceHoundItemCount)
{
    Console.WriteLine($"SweetTooth consumio mas items ({sweetToothItemCount}).");
}
else if (spiceHoundItemCount > sweetToothItemCount)
{
    Console.WriteLine($"SpiceHound consumio mas items ({spiceHoundItemCount}).");
}
else
{
    Console.WriteLine($"Ambos ninjas consumieron la misma cantidad de items ({sweetToothItemCount}).");
}

interface IConsumable
{
    string Name { get; set; }
    int Calories { get; set; }
    bool IsSpicy { get; set; }
    bool IsSweet { get; set; }
    string GetInfo();
}

class Food : IConsumable
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public bool IsSpicy { get; set; }
    public bool IsSweet { get; set; }
    public string GetInfo()
    {
        return $"{Name} (Food).  Calories: {Calories}.  Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
    }
    public Food(string name, int calories, bool spicy, bool sweet)
    {
        Name = name;
        Calories = calories;
        IsSpicy = spicy;
        IsSweet = sweet;
    }
}

class Drink : IConsumable
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public bool IsSpicy { get; set; }
    public bool IsSweet { get; set; }

    // Implement a GetInfo Method
    public string GetInfo()
    {
        return $"{Name} (Food).  Calories: {Calories}.  Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
    }
    // Add a constructor method
    public Drink(string name, int calories, bool spicy, bool sweet)
    {
        Name = name;
        Calories = calories;
        IsSpicy = spicy;
        IsSweet = sweet;
    }
}

abstract class Ninja
{
    protected int calorieIntake;
    public List<IConsumable> ConsumptionHistory;
    public Ninja()
    {
        calorieIntake = 0;
        ConsumptionHistory = new List<IConsumable>();
    }
    public abstract bool IsFull { get; }
    public abstract void Consume(IConsumable item);
}

class SweetTooth : Ninja
{
    // provide override for IsFull (Full at 1500 Calories)
    public override bool IsFull
    {
        get { return calorieIntake >= 1500; }
    }

    public override void Consume(IConsumable item)
    {
        if (!IsFull)
        {
            calorieIntake += item.Calories;
            if (item.IsSweet)
            {
                calorieIntake += 10;
            }
            ConsumptionHistory.Add(item);
            item.GetInfo();
        }
        else
        {
            Console.WriteLine("SweetTooth esta lleno y no puede comer mas");
        }

    }
}

class SpiceHound : Ninja
{
    public override bool IsFull
    {
        get { return calorieIntake >= 1200; }
    }

    public override void Consume(IConsumable item)
    {
        if (!IsFull)
        {
            calorieIntake += item.Calories;
            if (item.IsSpicy)
            {
                calorieIntake -= 5;
            }
            ConsumptionHistory.Add(item);
            item.GetInfo();
        }
        else
        {
            Console.WriteLine("SpiceHound esta lleno y no puede comer mas");
        }
    }
}

class Buffet
{
    public List<IConsumable> Menu;

    public Buffet()
    {
        Menu = new List<IConsumable>()
        {
            new Food("Pizza", 800, false, false),
            new Food("Salad", 200, false, false),
            new Food("Fruit", 100, false, true),
            new Food("Ice Cream", 500, false, true),
            new Food("Sushi", 600, true, false),
            new Drink("Water", 0, false, false),
            new Drink("Soda", 150, false, true),
            new Drink("Juice", 200, false, true),
            new Drink("Beer", 250, true, false)
        };
    }

    public IConsumable Serve()
    {
        Random rand = new Random();
        return Menu[rand.Next(Menu.Count)];
    }
}