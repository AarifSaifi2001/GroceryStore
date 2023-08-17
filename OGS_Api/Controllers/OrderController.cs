namespace OGS_Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using PdfSharpCore;
    using PdfSharpCore.Pdf;
    using TheArtOfDev.HtmlRenderer.PdfSharp;
    using OGS_Api.Data;
    using OGS_Api.Repositories;
    using System.IO;
    using System.Collections.Generic;
    using System;
    using OGS_Api.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrderAsync();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody]Orders order){

            var id = await _orderRepository.AddOrderAsync(order);
            return Ok(id);
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetOrdersByInvoiceId([FromRoute]string invoiceId){

            var orders = await _orderRepository.GetOrdersByInvoiceAsync(invoiceId);
            return Ok(orders);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByInvoiceId([FromRoute]int userId){

            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPut("updateStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute]int orderId, [FromBody]OrderStatusModel orderStatus){

          var order = await _orderRepository.updateOrderStatus(orderId, orderStatus);

          return Ok(order);
        }

        [HttpGet("generatepdf/{invoiceId}")]
        public async Task<IActionResult> GeneratePDF([FromRoute]string invoiceId){

          int increment = 0;
          double total = 0;
          List<Orders> orders = await _orderRepository.GetOrdersByInvoiceAsync(invoiceId);
          var document = new PdfDocument();
          string HtmlContent = "<div style='width: 100%; text-align: center;'>";
          HtmlContent+= "<h1>ORDER INVOICE</h1>";
          HtmlContent+="<h4 style='text-align: right;'>Order From : AS Kirana Store</h4>";
          HtmlContent+="<h4 style='text-align: right;'>Order Date : "+DateTime.Now+"</h4>";
          HtmlContent+="<hr>";
          HtmlContent+= "<div style='margin-top : 10px;'>";
          HtmlContent+= "<table style='width: 100%;' border='1px'>";
          HtmlContent+= "<tr>";
          HtmlContent+= "<td style='font-weight: bold' scope='col'>SR.NO</td>";
          HtmlContent+= "<td style='font-weight: bold' scope='col'>ORDER NUMBER</td>";
          HtmlContent+= "<td style='font-weight: bold' scope='col'>PRODUCT NAME</td>";
          HtmlContent+= "<td style='font-weight: bold' scope='col'>EACH PRICE</td>";
          HtmlContent+= "<td style='font-weight: bold' scope='col'>PRODUCT QUANTITY</td>";
          HtmlContent+= "<td style='font-weight: bold' scope='col'>TOTAL PRICE</td>";
          HtmlContent+= "</tr>";

          if(orders != null && orders.Count > 0){

            orders.ForEach(item => {

              HtmlContent+= "<tr>";
              HtmlContent+= "<td>"+(++increment)+"</td>";
              HtmlContent+= "<td>"+item.orderNumber+"</td>";
              HtmlContent+= "<td>"+item.productName+"</td>";
              HtmlContent+= "<td>"+(item.productPrice / item.productQuantity)+"</td>";
              HtmlContent+= "<td>"+item.productQuantity+"</td>";
              HtmlContent+= "<td>"+item.productPrice+"</td>";
              HtmlContent+= "</tr>";

              total = total + item.productPrice;

            });

          }

          HtmlContent+= "<tr>";
          HtmlContent+= "<td></td>";
          HtmlContent+= "<td></td>";
          HtmlContent+= "<td></td>";
          HtmlContent+= "<td></td>";
          HtmlContent+= "<td></td>";
          HtmlContent+= "<td>"+total+"</td>";
          HtmlContent+= "</tr>";
          HtmlContent+= "</table>";
          HtmlContent+= "</div>";
          HtmlContent+= "</div>";


          PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);
          byte[]? response = null;
          using(MemoryStream ms = new MemoryStream()){
            document.Save(ms);
            response = ms.ToArray();
          }

          string Filename = "Invoice"+invoiceId+".pdf";

          return File(response,"application/pdf", Filename);
        }
    }
}