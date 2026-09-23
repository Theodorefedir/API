using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGames.DAL.Entities;

namespace VideoGames.DAL.Repositories
{
    public class DeveloperRepository
    {
        private readonly AppDbContext _context;

        public DeveloperRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Developer> GetAll()
        {
            return _context.Developers.AsNoTracking();
        }

        public async Task<Developer?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Developers.FirstOrDefaultAsync(d => d.Id == id, ct);
        }

        public async Task CreateAsync(Developer entity, CancellationToken ct = default)
        {
            await _context.Developers.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateRangeAsync(IEnumerable<Developer> entities, CancellationToken ct = default)
        {
            await _context.Developers.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Developer entity, CancellationToken ct = default)
        {
            _context.Developers.Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Developer entity, CancellationToken ct = default)
        {
            _context.Developers.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await GetByIdAsync(id, ct);
            if (entity != null)
            {
                await DeleteAsync(entity, ct);
            }
        }
    }
}
