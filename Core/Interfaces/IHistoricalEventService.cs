﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IHistoricalEventService:IGenericRepository<HistoricalEvent>
    {
        Task<HistoricalEvent> CreateAsync(string dcZaman, string dckategori, string dcOlay);
 
    }
}
