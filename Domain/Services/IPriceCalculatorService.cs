using GhostWriter.Domain.Entities;
using System.Linq;

namespace GhostWriter.Domain.Services
{
    public interface IPriceCalculatorService
    {
        public bool ValidatePrice(decimal PricePerPage);
        public bool ValidatePrice(decimal totalPrice, int pagesNo);
        public IQueryable<ServiceCharge> GetServiceCharges();
        public decimal CalculateServiceCharges(decimal totalPrice, int pagesNo);
    }
}
