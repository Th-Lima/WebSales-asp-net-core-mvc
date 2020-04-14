using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMVC.Data;
using WebSalesMVC.Models;
using WebSalesMVC.Models.Enums;

namespace WebSalesMVC.Services
{
    public class SalesRecordsService
    {
        private readonly WebSalesMVCContext _context;

        public SalesRecordsService(WebSalesMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate, SaleStatus status)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue && maxDate.HasValue && status.Equals(SaleStatus.All))
            {
                result = result.Where(x => x.Date >= minDate.Value);
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            if (status.Equals(SaleStatus.Billed))
            {
                result = result.Where(x => x.Date >= minDate.Value);
                result = result.Where(x => x.Date <= maxDate.Value);
                result = result.Where(x => x.Status == SaleStatus.Billed);
            }
            if (status.Equals(SaleStatus.Pending))
            {
                result = result.Where(x => x.Date >= minDate.Value);
                result = result.Where(x => x.Date <= maxDate.Value);
                result = result.Where(x => x.Status == SaleStatus.Pending);
            }
            if (status.Equals(SaleStatus.Canceled))
            {
                result = result.Where(x => x.Date >= minDate.Value);
                result = result.Where(x => x.Date <= maxDate.Value);
                result = result.Where(x => x.Status == SaleStatus.Canceled);
            }
           
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
    }
}
