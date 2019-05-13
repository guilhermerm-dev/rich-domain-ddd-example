using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {

        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments => _payments.ToArray();


        public void AddPayment(Payment payment)
        {

            AddNotifications(new Contract()
            .Requires()
            .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento deve ser futura."));

            if (Valid)
                _payments.Add(payment);
        }

        public void DeactivateSubscription()
        {
            Active = false;
            LastUpdateDate = DateTime.Now;
        }

        public void ActivateSubscription()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

    }
}