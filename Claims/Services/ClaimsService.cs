using Claims.Controllers;
using Microsoft.EntityFrameworkCore;
using Claims;

namespace Claims.Services
{
    public class ClaimsService
    {
        private readonly ClaimsContext _context;

        public ClaimsService(ClaimsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            return await _context.Claims.ToListAsync();
        }

        public async Task<Claim?> GetClaimByIdAsync(string id)
        {
            return await _context.Claims.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Claim> CreateClaimAsync(Claim claim)
        {
            var cover = await _context.Covers.FirstOrDefaultAsync(c => c.Id == claim.CoverId);
            if (cover == null)
            {
                throw new InvalidOperationException("Related Cover does not exist.");
            }

            if (claim.Created < cover.StartDate || claim.Created > cover.EndDate)
            {
                throw new InvalidOperationException("Claim Created date must be within the Cover StartDate and EndDate.");
            }

            claim.Id = Guid.NewGuid().ToString();
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return claim;
        }

        public async Task DeleteClaimAsync(string id)
        {
            var claim = await GetClaimByIdAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
            }
        }
    }
}
