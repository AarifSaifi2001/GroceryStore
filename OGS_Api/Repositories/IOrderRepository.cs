using System.Collections.Generic;
using System.Threading.Tasks;
using OGS_Api.Data;
using OGS_Api.DTO;

namespace OGS_Api.Repositories
{
    public interface IOrderRepository
    {
        public Task<List<Orders>> GetAllOrderAsync();

        public Task<Orders> GetOrderByIdAsync(int id);

        public Task<List<Orders>> GetOrdersByInvoiceAsync(string invoiceId);

        public Task<List<Orders>> GetOrdersByUserIdAsync(int userId);

        public Task<int> AddOrderAsync(Orders order);

        public Task<Orders> updateOrderStatus(int id, OrderStatusModel order);

        public void DeleteOrderById();
    }
}