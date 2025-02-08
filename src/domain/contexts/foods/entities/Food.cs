using Shared.Entities;

namespace Domain.Contexts.Foods.Entities
{
  public class Food : Entity
  {

    protected Food()
    {

    }

    public Food(string code, string? name, string? scientificName, Guid? groupId, string? brand)
    {
      Code = code;
      Name = name;
      ScientificName = scientificName;
      GroupId = groupId;
      Brand = brand;
    }

    public string Code { get; private set; }
    public string? Name { get; private set; }
    public string? ScientificName { get; private set; }
    public string? Brand { get; private set; }
    public Guid? GroupId { get; private set; }
    public Group? Group { get; private set; }
    public List<Component> Components { get; private set; } = [];
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
  }
}