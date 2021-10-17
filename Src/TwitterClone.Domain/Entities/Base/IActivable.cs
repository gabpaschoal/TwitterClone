namespace TwitterClone.Domain.Entities.Base;

public interface IActivable
{
    bool IsActive { get; }
    public void Activate();
    public void Inactivate();
}
