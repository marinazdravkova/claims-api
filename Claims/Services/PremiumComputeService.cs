namespace Claims.Services
{
    public interface IPremiumComputeService
    {
        decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType);
    }

    public class PremiumComputeService : IPremiumComputeService
    {
        public decimal ComputePremium(DateTime startDate, DateTime endDate, CoverType coverType)
        {
            var multiplier = 1.3m;
            if (coverType == CoverType.Yacht)
            {
                multiplier = 1.1m;
            }

            if (coverType == CoverType.PassengerShip)
            {
                multiplier = 1.2m;
            }

            if (coverType == CoverType.Tanker)
            {
                multiplier = 1.5m;
            }

            var premiumPerDay = 1250m * multiplier;
            var insuranceLength = (int)(endDate - startDate).TotalDays;
            var totalPremium = 0m;

            for (var i = 0; i < insuranceLength; i++)
            {
                if (i < 30)
                {
                    totalPremium += premiumPerDay;
                }
                else if (i < 180)
                {
                    var discount = coverType == CoverType.Yacht ? 0.05m : 0.02m;
                    totalPremium += premiumPerDay - (premiumPerDay * discount);
                }
                else
                {
                    var discount = coverType == CoverType.Yacht ? 0.08m : 0.03m;
                    totalPremium += premiumPerDay - (premiumPerDay * discount);
                }
            }

            return totalPremium;
        }
    }
}
