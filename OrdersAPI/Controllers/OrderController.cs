﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersAPI.Filters;
using OrdersAPI.Models;
using OrdersAPI.Repository;

namespace OrdersAPI.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController()
        {
        }

        [HttpGet]
        [Route("[controller]/GetAll")]
        public IEnumerable<Order> GetAll()
        {
            return new OrdersRepository().GetAll();
        }

        [HttpGet]
        [Route("[controller]/GetById/{id}")]
        public Order GetById(Guid id)
        {
            return new OrdersRepository().GetById(id);
        }

        [HttpGet]
        [Route("[controller]/GetByUserId/{id}")]
        public IEnumerable<Order> GetByUserId(Guid userId)
        {
            return new OrdersRepository().GetByUserId(userId);
        }

        [HttpGet]
        [Route("[controller]/GetByProductId/{id}")]
        public IEnumerable<Order> GetByProductId(Guid userId)
        {
            return new OrdersRepository().GetByProductId(userId);
        }

        [ApiKeyAuth]
        [HttpPost]
        [Route("[controller]/Add")]
        public Order Add(Order order)
        {
            return new OrdersRepository().Add(order);
        }

        [ApiKeyAuth]
        [HttpPost]
        [Route("[controller]/RemoveById/{id}")]
        public Order RemoveById(Guid id)
        {
            return new OrdersRepository().RemoveById(id);
        }
    }
}
