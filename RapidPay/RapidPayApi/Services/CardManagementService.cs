using Microsoft.EntityFrameworkCore;
using RapidPayApi.Data;
using RapidPayApi.Data.Entities;
using RapidPayApi.Models;

namespace RapidPayApi.Services
{
    public class CardManagementService
    {
        RapidPayDbContext _rapidDbContext;
        FeeCalculationService _feeService;

        public CardManagementService(RapidPayDbContext rapidDbContext, FeeCalculationService UEFService)
        {
            _rapidDbContext = rapidDbContext;
            _feeService = UEFService;
        }

        public async Task<TransactionResponse> CreateCard(NewCard cardToCreate)
        {
            try
            {

                var entityCard = new Card
                {
                    CardNumber = cardToCreate.CardNumber,
                    CardHolderName = cardToCreate.CardHolderName
                };

                await _rapidDbContext.AddAsync(entityCard);
                await _rapidDbContext.SaveChangesAsync();

                return new TransactionResponse
                {
                    Success = true,
                    Message = "Your card has been created"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TransactionResponse> PayWithCard(PaymentInfo payment)
        {
            try
            {

                var entityCard = await _rapidDbContext.Cards
                    .Include(c => c.Payments).SingleOrDefaultAsync(c => c.CardNumber == payment.CardNumber);

                if (entityCard != null)
                {
                    var entityPayment = new Payment
                    {
                        CardNumber = payment.CardNumber,
                        Amount = payment.Amount,
                        PayDate = payment.PayDate,
                        PayTo = payment.PayTo,
                        FeeAmount = _feeService.GetFeeAmount
                    };

                    if(entityCard.Payments == null)
                        entityCard.Payments = new List<Payment>();

                    entityCard.Payments.Add(entityPayment);
                    await _rapidDbContext.SaveChangesAsync();

                    return new TransactionResponse
                    {
                        Success = true,
                        Message = $"Your card has been charged with {payment.Amount}"
                    };
                }

                return new TransactionResponse
                {
                    Success = false,
                    Message = $"Transaction has been rejected"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CardBalance> GetCardBalance(string cardNumber)
        {
            try
            {
                var entityCard = await _rapidDbContext.Cards
                    .Include(c => c.Payments).SingleOrDefaultAsync(c => c.CardNumber == cardNumber);

                var returns = new CardBalance();

                if (entityCard != null)
                {
                    returns.CardHolder = entityCard.CardHolderName;
                    returns.CardNumber = entityCard.CardNumber;

                    if(entityCard.Payments?.Count > 0)
                    {
                        returns.Transactions = entityCard.Payments.Count;
                        returns.TotalAmount = entityCard.Payments.Sum(p => p.Amount);
                    }
                }

                return returns;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
