using System.Data;

namespace Friends.Repository.Context
{
    public abstract class SQLServerContext
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public SQLServerContext(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
