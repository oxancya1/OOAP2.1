using System;

// Абстрактний будівельник для створення набору
abstract class GiftSetBuilder
{
    protected GiftSet giftSet;
    protected const double TotalWeight = 1.5; // Загальна вага набору в кг

    public void CreateNewGiftSet() => giftSet = new GiftSet();
    public GiftSet GetGiftSet() => giftSet;

    public abstract void SetName();
    public abstract void SetWeight();
    public abstract void SetCostPerKg(double candyCost, double chocolateCost, double waferCost, double drageeCost);
}

// Класи конкретних будівельників для різних типів наборів
class EconomicSetBuilder : GiftSetBuilder
{
    public override void SetName() => giftSet.Name = "Ласунка";
    public override void SetWeight()
    {
        // Пропорційно розподіляємо загальну вагу на компоненти
        giftSet.LollipopsWeight = TotalWeight * 0.3;
        giftSet.ChocolatesWeight = TotalWeight * 0.2;
        giftSet.WafersWeight = TotalWeight * 0.2;
        giftSet.DrageesWeight = TotalWeight * 0.3;
    }
    public override void SetCostPerKg(double candyCost, double chocolateCost, double waferCost, double drageeCost)
    {
        giftSet.TotalCost = giftSet.LollipopsWeight * candyCost +
                            giftSet.ChocolatesWeight * chocolateCost +
                            giftSet.WafersWeight * waferCost +
                            giftSet.DrageesWeight * drageeCost;
    }
}

class StandardSetBuilder : GiftSetBuilder
{
    public override void SetName() => giftSet.Name = "Наминайко";
    public override void SetWeight()
    {
        giftSet.LollipopsWeight = TotalWeight * 0.25;
        giftSet.ChocolatesWeight = TotalWeight * 0.25;
        giftSet.WafersWeight = TotalWeight * 0.2;
        giftSet.DrageesWeight = TotalWeight * 0.3;
    }
    public override void SetCostPerKg(double candyCost, double chocolateCost, double waferCost, double drageeCost)
    {
        giftSet.TotalCost = giftSet.LollipopsWeight * candyCost +
                            giftSet.ChocolatesWeight * chocolateCost +
                            giftSet.WafersWeight * waferCost +
                            giftSet.DrageesWeight * drageeCost;
    }
}

class ExtraSetBuilder : GiftSetBuilder
{
    public override void SetName() => giftSet.Name = "Пан Коцький";
    public override void SetWeight()
    {
        giftSet.LollipopsWeight = TotalWeight * 0.2;
        giftSet.ChocolatesWeight = TotalWeight * 0.4;
        giftSet.WafersWeight = TotalWeight * 0.15;
        giftSet.DrageesWeight = TotalWeight * 0.25;
    }
    public override void SetCostPerKg(double candyCost, double chocolateCost, double waferCost, double drageeCost)
    {
        giftSet.TotalCost = giftSet.LollipopsWeight * candyCost +
                            giftSet.ChocolatesWeight * chocolateCost +
                            giftSet.WafersWeight * waferCost +
                            giftSet.DrageesWeight * drageeCost;
    }
}

// Продукт - подарунковий набір
class GiftSet
{
    public string Name { get; set; }
    public double LollipopsWeight { get; set; }
    public double ChocolatesWeight { get; set; }
    public double WafersWeight { get; set; }
    public double DrageesWeight { get; set; }
    public double TotalCost { get; set; }

    public void DisplayInfo()
    {
        Console.WriteLine($"Назва набору: {Name}");
        Console.WriteLine($"Вага льодяників: {LollipopsWeight:F2} кг");
        Console.WriteLine($"Вага шоколадних цукерок: {ChocolatesWeight:F2} кг");
        Console.WriteLine($"Вага вафель: {WafersWeight:F2} кг");
        Console.WriteLine($"Вага драже: {DrageesWeight:F2} кг");
        Console.WriteLine($"Загальна ціна: {TotalCost:F2} грн");
    }
}

// Директор, який керує процесом створення подарункового набору
class PackagingSystem
{
    private GiftSetBuilder giftSetBuilder;

    public void SetBuilder(GiftSetBuilder builder) => giftSetBuilder = builder;

    public GiftSet BuildGiftSet(double candyCost, double chocolateCost, double waferCost, double drageeCost)
    {
        giftSetBuilder.CreateNewGiftSet();
        giftSetBuilder.SetName();
        giftSetBuilder.SetWeight();
        giftSetBuilder.SetCostPerKg(candyCost, chocolateCost, waferCost, drageeCost);
        return giftSetBuilder.GetGiftSet();
    }
}

// Тестування програми
class Program
{
    static void Main(string[] args)
    {
        PackagingSystem packagingSystem = new PackagingSystem();

        Console.Write("Введіть вартість льодяників за кг: ");
        double candyCost = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введіть вартість шоколадних цукерок за кг: ");
        double chocolateCost = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введіть вартість вафель за кг: ");
        double waferCost = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введіть вартість драже за кг: ");
        double drageeCost = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Виберіть тип набору: 1 - Ласунка, 2 - Наминайко, 3 - Пан Коцький");
        int choice = Convert.ToInt32(Console.ReadLine());

        GiftSetBuilder builder;

        switch (choice)
        {
            case 1:
                builder = new EconomicSetBuilder();
                break;
            case 2:
                builder = new StandardSetBuilder();
                break;
            case 3:
                builder = new ExtraSetBuilder();
                break;
            default:
                throw new ArgumentException("Невірний вибір набору");
        }

        packagingSystem.SetBuilder(builder);
        GiftSet giftSet = packagingSystem.BuildGiftSet(candyCost, chocolateCost, waferCost, drageeCost);
        giftSet.DisplayInfo();
    }
}
