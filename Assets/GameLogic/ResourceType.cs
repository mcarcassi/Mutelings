using System;
public class ResourceType
{
    public String Name { get; }
    public bool IsFood { get; }

    public ResourceType(string name, bool isFood)
    {
        Name = name;
        IsFood = isFood;
    }
}
