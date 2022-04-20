using Application.ShippingOrders.Commands;
using Application.ShippingOrders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShippingOrdersController : BaseController
    {
        private readonly IMediator mediator;

        public ShippingOrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ShippingOrderViewModel orderViewModel, CancellationToken cancellationToken)
        {
            var command = new CreateShippingOrderCommand(
                orderViewModel.Description,
                CurrentUserId,
                orderViewModel.Address);

            var result = await mediator.Send(command, cancellationToken);

            return result != null
                ? Ok(result)
                : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid orderId, CancellationToken cancellationToken)
        {
            var query = new GetShippingOrderByIdQuery(orderId);
            var result = await mediator.Send(query, cancellationToken);

            return result != null
                ? Ok(result)
                : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var query = new GetShippingOrdersQuery();
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid orderId, [FromBody] ShippingOrderViewModel orderViewModel, CancellationToken cancellationToken)
        {
            var command = new UpdateShippingOrderCommand(
                orderId,
                orderViewModel.Address,
                orderViewModel.Description,
                CurrentUserId);

            await mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}