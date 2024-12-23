﻿using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using AutoMapper;
namespace Discount.Application.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public GetDiscountQueryHandler(IDiscountRepository discountRepository,IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
           var coupon = await _discountRepository.GetDiscount(request.ProductName);

           if (coupon == null)
           {
                throw new RpcException(new Status(StatusCode.NotFound,$"Discount for the product name = {request.ProductName} not found"));
           }
            CouponModel couponModel = new CouponModel()
            {
                Id = coupon.Id,
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName,
            };

            var test = _mapper.Map<CouponModel>(coupon);   
            return couponModel;
        }
    }
}
