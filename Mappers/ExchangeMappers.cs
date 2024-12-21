using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models.Enums;

namespace SkillSwapAPI.Mappers
{
    public static class ExchangeMappers
    {
        // Mapper para convertir de Exchange a ExchangeDto
        public static ExchangeDto ToExchangeDto(this Exchange exchange)
        {
            return new ExchangeDto
            {
                Id = exchange.Id,
                OfferedUserId = exchange.OfferedUserId,
                RequestedUserId = exchange.RequestedUserId,
                OfferedSkillId = exchange.OfferedSkillId,
                RequestedSkillId = exchange.RequestedSkillId,
                Status = exchange.Status,
                OfferedUserName = exchange.OfferedUser?.Name ?? string.Empty,
                RequestedUserName = exchange.RequestedUser?.Name ?? string.Empty,
                OfferedSkillName = exchange.OfferedSkill?.Name ?? string.Empty,
                RequestedSkillName = exchange.RequestedSkill?.Name ?? string.Empty
            };
        }

        // Mapper para convertir de CreateExchangeDto a Exchange
        public static Exchange ToExchange(this CreateExchangeDto createExchangeDto)
        {
            return new Exchange
            {
                OfferedUserId = createExchangeDto.OfferedUserId,
                RequestedUserId = createExchangeDto.RequestedUserId,
                OfferedSkillId = createExchangeDto.OfferedSkillId,
                RequestedSkillId = createExchangeDto.RequestedSkillId,
                Status = ExchangeStatus.Pending
            };
        }

        // Mapper para convertir de UpdateExchangeDto a Exchange
        public static Exchange ToExchange(this UpdateExchangeDto updateExchangeDto, Exchange exchange)
        {
            exchange.Status = updateExchangeDto.Status;
            return exchange;
        }
    }
    
}