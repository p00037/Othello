using System.Collections.Generic;

namespace EntityFrameworkBase
{
    //public class SingleServiceBase<TEntity, Tkey> : IService<TEntity, Tkey> where TEntity : class
    //{
    //    protected readonly IUnitOfWork unitOfWork;
    //    protected readonly IRepository<TEntity, Tkey> repository;

    //    public SingleServiceBase(IUnitOfWork unitOfWork, IRepository<TEntity, Tkey> repository)
    //    {
    //        this.repository = repository;
    //        this.unitOfWork = unitOfWork;
    //    }

    //    public virtual IEnumerable<TEntity> Get()
    //    {
    //        return repository.GetAll();
    //    }

    //    public virtual TEntity Get(Tkey id)
    //    {
    //        return repository.Get(id);
    //    }

    //    public virtual void Insert(TEntity entity)
    //    {
    //        unitOfWork.Save(() =>
    //        {
    //            repository.Add(entity);
    //        });
    //    }

    //    public virtual void Update(TEntity entity)
    //    {
    //        unitOfWork.Save(() =>
    //        {
    //            repository.Update(entity);
    //        });
    //    }

    //    public virtual void Delete(TEntity entity)
    //    {
    //        unitOfWork.Save(() =>
    //        {
    //            repository.Remove(entity);
    //        });
    //    }
    //}
}
