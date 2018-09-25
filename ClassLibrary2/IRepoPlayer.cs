using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary2
{
        public abstract class EntityBase
        {
            public Int64 Id { get; protected set; }
        }

        public interface IRepository<T> where T : EntityBase
        {
            T GetById(string id);
            List<T> ListAll();

            void Create(T entity);
            void Delete(T entity);
            void Update(T entity);
        }

        ////The generic Repository class implements the IRepository interface and implements the members of the interface.

        //public class Repository<T> : IRepository<T> where T : EntityBase

        //{

        //    public void Create(T entity)
        //    { }

        //    public List<T> ListKeys()
        //    { }

        //    public void Delete(T entity)
        //    { }

        //    public T GetById(string Id)
        //                {
        //        throw new NotImplementedException();
        //    }

        //    public void Update(T entity)
        //    { }

        //}

        //Creating repositories for specific classes
        // you were to create a ProductRepository, you should first create an entity class Product that extends the EntityBase class.


    }