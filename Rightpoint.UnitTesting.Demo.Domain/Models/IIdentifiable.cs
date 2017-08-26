namespace Rightpoint.UnitTesting.Demo.Domain.Models
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}
