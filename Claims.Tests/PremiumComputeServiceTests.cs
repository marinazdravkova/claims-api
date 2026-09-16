using Claims.Controllers;
using Claims.Services;
using Xunit;

namespace Claims.Tests
{
    public class PremiumComputeServiceTests
    {
        private readonly PremiumComputeService _service = new();

        [Fact]
        public void ComputeYachtPremium_30Days()
        {
            var startDate = new DateTime(2026, 1, 1);
            var endDate = startDate.AddDays(30);

            var result = _service.ComputePremium(startDate, endDate, CoverType.Yacht);

            Assert.Equal(41250m, result);
        }

        [Fact]

        public void ComputeYachtPremium_40Days()
        {
            var startDate = new DateTime(2026, 1, 1);
            var endDate = startDate.AddDays(40);

            var result = _service.ComputePremium(startDate, endDate, CoverType.Yacht);

            Assert.Equal(54312.50m, result);
        }

        [Fact]

        public void ComputeYachtPremium_200Days()
        {
            var startDate = new DateTime(2026, 1, 1);
            var endDate = startDate.AddDays(200);

            var result = _service.ComputePremium(startDate, endDate, CoverType.Yacht);

            Assert.Equal(262487.50m, result);
        }
    }
}
