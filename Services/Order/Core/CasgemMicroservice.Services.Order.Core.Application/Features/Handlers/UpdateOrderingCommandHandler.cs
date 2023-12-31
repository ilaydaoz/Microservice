﻿using CasgemMicroservice.Services.Order.Core.Application.Features.Commands;
using CasgemMicroservice.Services.Order.Core.Application.Interfaces;
using CasgemMicroservice.Services.Order.Core.Domain.Entities;
using MediatR;

namespace CasgemMicroservice.Services.Order.Core.Application.Features.Handlers
{
    public class UpdateOrderingCommandHandler : IRequestHandler<UpdateOrderingCommandRequest>
    {
        private readonly IRepository<Ordering> _repository;

        public UpdateOrderingCommandHandler(IRepository<Ordering> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderingCommandRequest request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.OrderingID);
            values.UserID = request.UserID;
            values.TotalPrice = request.TotalPrice;
            values.OrderDate = request.OrderDate;
            await _repository.UpdateAsync(values);
        }
    }
}
