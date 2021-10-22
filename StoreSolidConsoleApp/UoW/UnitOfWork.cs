using StoreSolidConsoleApp.Data;
using StoreSolidConsoleApp.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSolidConsoleApp.UoW
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext storeContext;
        private CollectionProductRepository productRepository;
        private CollectionUserRepository userRepository;
        private CollectionOrderRepository orderRepository;

        public UnitOfWork()
        {
            storeContext = new StoreContext();
        }

        public CollectionProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new CollectionProductRepository(storeContext);
                return productRepository;
            }
        }

        public CollectionUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new CollectionUserRepository(storeContext);
                return userRepository;
            }
        }

        public CollectionOrderRepository OrderRepository
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new CollectionOrderRepository(storeContext);
                return orderRepository;
            }
        }
    }
}
