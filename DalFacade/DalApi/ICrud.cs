namespace DalApi;

/// <summary>
/// Interface for CRUD functions
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrud<T> 
{
    public int Add(T entity);
    public void Delete(int ID);
    public void Update(T o);
    public T Get(int ID);
    public IEnumerable<T?> GetAll(Func<T?, bool>? func = null);


}