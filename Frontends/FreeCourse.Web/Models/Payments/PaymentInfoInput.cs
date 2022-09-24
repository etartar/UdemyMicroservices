﻿namespace FreeCourse.Web.Models.Payments
{
    public class PaymentInfoInput
    {
        public PaymentInfoInput()
        {
        }

        public PaymentInfoInput(string cardName, string cardNumber, string expiration, string cvv, decimal totalPrice)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            TotalPrice = totalPrice;
        }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
