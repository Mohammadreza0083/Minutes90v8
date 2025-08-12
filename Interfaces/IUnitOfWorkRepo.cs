namespace minutes90v8.Interfaces
{
    public interface IUnitOfWorkRepo
    {
        Task<bool> Complete();
    }
}
