﻿using AutoMapper;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        IOrderService _orderService;
        IMapper _mapper;
        ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, IMapper mapper,ILogger<OrderController> logger)
        {
            _mapper = mapper;
            _orderService = orderService;
            _logger = logger;
        }
        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            GetOrderDTO orderDTO = await _orderService.GetOrderByIdAsync(id);

            if (orderDTO != null)
            {
                _logger.LogInformation("OrderController:get");
                return Ok(orderDTO);
            }
            else
                return BadRequest(); 
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<Order>> Post(PostOrderDTO orderDTO)
        {
            Order order = _mapper.Map<PostOrderDTO, Order>(orderDTO);
            Order o = await _orderService.AddOrderAsync(order);
            GetOrderDTO returnOrder = _mapper.Map<Order,GetOrderDTO>(o);

            if (returnOrder != null)
            {
                _logger.LogInformation($"OrderController:new order {o.OrderId} created  ");
                return CreatedAtAction(nameof(Get), new { id = returnOrder.OrderId }, returnOrder);
            }
            else
            {
                _logger.LogError("OrderController: post: error");
                return BadRequest();
            }
        }

    }
}
