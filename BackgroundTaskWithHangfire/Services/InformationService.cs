using BackgroundTaskWithHangfire.Data;
using BackgroundTaskWithHangfire.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundTaskWithHangfire.Services
{
    public class InformationService 
    {
        private readonly BackgroundTaskWithHangfireContext _context;

        public InformationService(BackgroundTaskWithHangfireContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Information>> GetTotalInformation()
        {
            return await _context.Information.ToListAsync();
        }
    }
}
