using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCidades.Interface
{
    //Interface para as classes DAO
    interface IDataStore<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
        public void Add(T obj);
        public void Delete(T obj);
        
    }
}
