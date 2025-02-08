using Shared.Entities;

namespace Domain.Contexts.Foods.Entities
{
  public class Component : Entity
  {

    protected Component()
    {

    }

    public Component(string name, string unit, string value, Guid foodId)
    {
      Name = name;
      Unit = unit;
      Value = value;
      FoodId = foodId;
    }

    public string Name { get; private set; }
    public string Unit { get; private set; }
    public string Value { get; private set; }
    public Guid FoodId { get; private set; }
    public Food Food { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
  }
}