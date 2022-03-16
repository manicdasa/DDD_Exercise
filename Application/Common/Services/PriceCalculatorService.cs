using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Defaults;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Services;
using System;
using System.Linq;

namespace GhostWriter.Application.Common.Services
{
    public class PriceCalculatorService : IPriceCalculatorService
    {
        private readonly IApplicationDbContext _context;

        public PriceCalculatorService(IApplicationDbContext context)
        {
            _context = context;
        }

        public bool ValidatePrice(decimal PricePerPage)
        {
            return PricePerPage >= BusinessDefaults.MinimalPricePerPage;
        }

        public bool ValidatePrice(decimal totalPrice, int pagesNo)
        {
            decimal pricePerPage = totalPrice / pagesNo;
            return pricePerPage >= BusinessDefaults.MinimalPricePerPage;
        }

        public IQueryable<ServiceCharge> GetServiceCharges()
        {
            var serviceCharges = _context.ServiceCharges.Where(x => x.StartDate >= DateTime.UtcNow && x.EndDate <= DateTime.UtcNow && !x.IsDefault);

            if (!serviceCharges.Any())
                serviceCharges = _context.ServiceCharges.Where(x => x.IsDefault);

            return serviceCharges;
        }

        public decimal CalculateServiceCharges(decimal totalPrice, int pagesNo)
        {
            var serviceCharges = GetServiceCharges();

            var tax = serviceCharges.Where(x => x.IsDefault && x.IsPercentage).FirstOrDefault().ChargeAmount * (decimal)0.01 * totalPrice;
            var fee = serviceCharges.Where(x => x.IsDefault && !x.IsPercentage).FirstOrDefault().ChargeAmount * pagesNo;

            var totalServiceCharge = tax + fee;

            return totalServiceCharge;
        }
    }
}
