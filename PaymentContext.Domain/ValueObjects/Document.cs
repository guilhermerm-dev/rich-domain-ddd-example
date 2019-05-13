using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
            .Requires()
            .IsTrue(Validate(), "Document.Number", "Documento Inválido"));
        }

        public string Number { get; set; }
        public EDocumentType Type { get; private set; }

        private bool Validate()
        {
            //Alterar
            if (Type == EDocumentType.CNPJ && Number.Length == 14)
           return true;

           if (Type == EDocumentType.CPF && Number.Length == 11)
           return true;

           return false;
        }
    }
}