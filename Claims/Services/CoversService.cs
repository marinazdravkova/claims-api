using Microsoft.EntityFrameworkCore;

namespace Claims.Services;

public class CoversService
{
    private readonly ClaimsContext _context;
    private readonly PremiumComputeService _premiumComputeService;

    public CoversService(ClaimsContext context, PremiumComputeService premiumComputeService)
    {
        _context = context;
        _premiumComputeService = premiumComputeService;
    }

    public async Task<IEnumerable<Cover>> GetCoversAsync()
    {
        return await _context.Covers.ToListAsync();
    }

    public async Task<Cover?> GetCoverByIdAsync(string id)
    {
        return await _context.Covers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Cover> CreateCoverAsync(Cover cover)
    {
        cover.Id = Guid.NewGuid().ToString();
        cover.Premium = _premiumComputeService.ComputePremium(cover.StartDate, cover.EndDate, cover.Type);

        _context.Covers.Add(cover);
        await _context.SaveChangesAsync();

        return cover;
    }

    public async Task DeleteCoverAsync(string id)
    {
        var cover = await GetCoverByIdAsync(id);
        if (cover != null)
        {
            _context.Covers.Remove(cover);
            await _context.SaveChangesAsync();
        }
    }
}