using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.SQL;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class OrderManagerController : Controller
    {
        IOrderService orderService;
        // GET: OrderManager
        public OrderManagerController()
        {
            DataContext DContext = new DataContext();
            IRepository<Order> orderContext = new SQLRepository<Order>(DContext);
            orderService = new OrderService(orderContext);

        }
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList();
            return View(orders);
        }
        public ActionResult UpdateOrder(string Id)
        {
            Order order = orderService.GetOrder(Id);
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            return View(order);
        }
        [HttpPost]
        public ActionResult UpdateOrder(Order UpdatedOrder,string Id)
        {
            Order order = orderService.GetOrder(Id);
            order.OrderStatus = UpdatedOrder.OrderStatus;
            orderService.UpdateOrder(order);
            return RedirectToAction("Index");
        }
    }
}