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
    public class HistoricalEventService : GenericRepository<HistoricalEvents>, IHistoricalEventService
    {
        public HistoricalEventService(ApplicationDbContext context) : base(context)
        {
        }
        private readonly ApplicationDbContext _context;

       

        public async Task<HistoricalEvents> Create(HistoricalEvents historicalEvent)
        { 
            _context.HistoricalEvents.Add(historicalEvent);
            await _context.SaveChangesAsync();
            return historicalEvent;
        }

        public HistoricalEvents CreateAsync(HistoricalEvents obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<HistoricalEvents>> GetAllAsync()
        {
            return await _context.HistoricalEvents.ToListAsync();
        }

        public async Task<HistoricalEvents> GetByIdAsync(int id)
        {
            return await _context.HistoricalEvents.FindAsync(id);
        }

        Task<IList<HistoricalEvents>> IGenericRepository<HistoricalEvents>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
