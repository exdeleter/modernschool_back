using modernschool_back.Models;

namespace modernschool_back.Interfaces
{
    public interface IClass
    {
        IEnumerable<Class> Get();
        Class Get(int id);
        void Create(Class entity);
        void Update(Class entity);
        Class Delete(int id);
    }
}
