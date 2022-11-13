namespace DalApi;


public interface ICrud<T>
{
    public int Add(T entity);
    public void Delete(int ID);
    public void Update(T o);
    public T Get(int ID);
    public IEnumerable<T> GetAll();


}