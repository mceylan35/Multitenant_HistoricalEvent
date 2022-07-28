using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class HistoricalEventService : GenericRepository<HistoricalEvent>, IHistoricalEventService
    {
        public HistoricalEventService(ApplicationDbContext context) : base(context)
        {
        }
        private readonly ApplicationDbContext _context;

       

        public async Task<HistoricalEvent> CreateAsync(string dcZaman, string dcKategori, string dcOlay)
        {
            var historicalEvent = new HistoricalEvent(dcZaman, dcKategori, dcOlay);
            _context.HistoricalEvents.Add(historicalEvent);
            await _context.SaveChangesAsync();
            return historicalEvent;
        }

        public Task<HistoricalEvent> CreateAsync(HistoricalEvent obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<HistoricalEvent>> GetAllAsync()
        {
            return await _context.HistoricalEvents.ToListAsync();
        }

        public async Task<HistoricalEvent> GetByIdAsync(int id)
        {
            return await _context.HistoricalEvents.FindAsync(id);
        }

        Task<IList<HistoricalEvent>> IGenericRepository<HistoricalEvent>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
