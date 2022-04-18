using System.Collections.Generic;

namespace com.minsoehanwin.sample.Core.Services
{
    public interface IServiceBase<T>
    {
        void Save(T obj);
        IList<T> GetList();
        void Delete(T obj);
        void DeleteById(int id);
        T GetById(int id);
    }
}