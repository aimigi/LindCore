using LindCore.Domain.Entities;
using LindCore.Repositories.EF;

namespace LindCore.Manager
{
    public class ManagerEfRepository<T> : EFRepository<T> where T : class, IEntity
    {
        public ManagerEfRepository()
            : base(new ManagerContext())
        {

        }
    }
}
