using System;
using System.Collections.Generic;
using System.Text;
using StoreSolidConsoleApp.Data;

namespace StoreSolidConsoleApp.UoW
{
    public partial interface IUnitOfWork
    {
        public CollectionProductRepository ProductRepository { get; }
        public CollectionUserRepository UserRepository { get; }
        public CollectionOrderRepository OrderRepository { get; }
    }
}
