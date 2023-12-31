﻿using CasgemMicroservice.Services.Order.Core.Application.Features.Commands;
using CasgemMicroservice.Services.Order.Core.Application.Features.Queries;
using CasgemMicroservice.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CasgemMicroservice.Services.Order.Presenation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderingController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> OrderingList()
        {
            var value = await _mediator.Send(new GetAllOrderingQueryRequest());
            return Ok(value);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> OrderingGetById(int id)
        {
            var values = await _mediator.Send(new GetByIdOrderingQueryRequest(id));
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> OrderingCreata(CreateOrderingCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok("Ordering eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> AddresUpdate(UpdateOrderingCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok("Ordering güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> AddresDelete(int id)
        {
            await _mediator.Send(new RemoveOrderingCommadRequest(id));
            return Ok("Ordering Silindi");
        }
        [HttpGet("orderinggetuser")]
        public async Task<IActionResult> OrderingGetUser()
        {
            var response = await _mediator.Send(new GetOrderUserByIdQueryRequst
            {
                Id = _sharedIdentityService.GetUserID
            });
            return Ok(response);    
        }
    }
}
