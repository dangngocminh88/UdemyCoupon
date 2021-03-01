using Entities;

namespace UdemyCoupon.Repositories
{
    public class RepositoryBase
    {
        protected RepositoryContext RepositoryContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }
    }
}
