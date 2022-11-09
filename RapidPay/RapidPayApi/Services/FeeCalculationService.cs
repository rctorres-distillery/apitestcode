using RapidPayApi.Data;
using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace RapidPayApi.Services
{
    public class FeeCalculationService
    {
        private decimal LastFeeAmount = 0.1M;
        private static Timer UEFService = new Timer(10);
        private decimal UEFMultiplier = 0;

        public FeeCalculationService()
        {
            UEFService.AutoReset = true;
            UEFService.Enabled = true;
            UEFService.Elapsed += SetUEFAmount;
            UEFService.Start();
            UEFService.Interval = 36000000;
        }

        private void SetUEFAmount(object? sender, ElapsedEventArgs e)
        {
            var UEF = new Random();
            var intRandom = (float)UEF.Next(0, 1);
            UEFMultiplier = (decimal)(intRandom + UEF.NextSingle());

            var amount = LastFeeAmount * UEFMultiplier;
            LastFeeAmount = amount;
        }

        public decimal GetFeeAmount
        {
            get => LastFeeAmount;
        }
    }
}
