using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OGS_Api.Data;
using OGS_Api.DTO;

namespace OGS_Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly OgsContext _context;
        public readonly IMapper _mapper;
        public OrderRepository(OgsContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<int> AddOrderAsync(Orders order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.id;
        }

        public void DeleteOrderById()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Orders>> GetAllOrderAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public Task<Orders> GetOrderByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Orders>> GetOrdersByInvoiceAsync(string invoiceId)
        {
            var orders = await _context.Orders.Where(c => c.invoiceId == invoiceId).ToListAsync();
            return orders;
        }

        public async Task<List<Orders>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.Orders.Where(c => c.userId == userId).ToListAsync();
            return orders;
        }

        public async Task<Orders> updateOrderStatus(int id, OrderStatusModel order){

            var updatedOrder = _mapper.Map<Orders>(order);

            var order2 = await _context.Orders.FindAsync(id);

            if(order2 != null){

                order2.orderstatus = updatedOrder.orderstatus;

                await _context.SaveChangesAsync();
            }

            return order2;
        }
    }
}