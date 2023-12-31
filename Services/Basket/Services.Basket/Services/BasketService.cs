﻿using Services.Basket.Dtos;
using Services.Basket.Services.Interfaces;
using Shared.Dtos;
using System.Text.Json;

namespace Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var basketExists = await _redisService.GetDb().StringGetAsync(userId);
            if (string.IsNullOrEmpty(basketExists))
                return Response<BasketDto>.Fail("Basket not found", 404);

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basketExists), 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not updated", 500);
        }
    }
}
