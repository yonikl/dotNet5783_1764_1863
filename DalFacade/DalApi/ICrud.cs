namespace DalApi;


public interface ICrud<T>
{
    public void Add(T entity);
    public void Delete(T entity);
    public void Update(T entity);
    public T GetById(int id);
    public IEnumerable<T> GetAll();


}