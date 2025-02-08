using Shared.Entities;

namespace Domain.Contexts.Foods.Entities
{
  public class Group : Entity
  {

    protected Group()
    {

    }

    public Group(string wrapperId, string name)
    {
      WrapperId = wrapperId;
      Name = name;
    }

    public string WrapperId { get; private set; }
    public string Name { get; private set; }
    public IList<Food> Foods { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
  }
}