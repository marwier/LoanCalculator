
using System;
using System.Collections.Generic;
using CommonModels;

namespace InterviewTask.Models.LoanModels
{
    public class MonthlyCapitalization : ILoanModel
    {
        public List<Payment> ReturnPayments(decimal interest, decimal totalAmount, ushort numberOfYears)
        {
            const int numberOfMonths = 12;

            if (interest < 0)
                throw new ArgumentOutOfRangeException(nameof(interest));
            if (totalAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(totalAmount));
            if (numberOfYears <= 0 || numberOfYears * numberOfMonths > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(numberOfYears));

            var capitalizationPeriod = numberOfYears * numberOfMonths;
            var interestPerMonth = interest / capitalizationPeriod;
            var capitalPerMonth = totalAmount / capitalizationPeriod;

            var paymentList = new List<Payment>();

            for (ushort i = 0; i < capitalizationPeriod; i++)
            {
                paymentList.Add(new Payment
                {
                    PaymentId = i,
                    Capital = capitalPerMonth,
                    Interest = totalAmount * interestPerMonth
                });

                totalAmount -= capitalPerMonth;
            }

            return paymentList;
        }
    }
}