namespace minutes90.Interfaces
{
    public interface IUnitOfWorkRepo
    {
        Task<bool> Complete();
    }
}
