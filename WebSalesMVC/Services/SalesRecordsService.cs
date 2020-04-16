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
          
            switch (status)
            {
                case SaleStatus.Billed:
                    result = result.Where(x => x.Date >= minDate.Value);
                    result = result.Where(x => x.Date <= maxDate.Value);
                    result = result.Where(x => x.Status == SaleStatus.Billed);
                    break;
                case SaleStatus.Pending:
                    result = result.Where(x => x.Date >= minDate.Value);
                    result = result.Where(x => x.Date <= maxDate.Value);
                    result = result.Where(x => x.Status == SaleStatus.Pending);
                    break;
                case SaleStatus.Canceled:
                    result = result.Where(x => x.Date >= minDate.Value);
                    result = result.Where(x => x.Date <= maxDate.Value);
                    result = result.Where(x => x.Status == SaleStatus.Canceled);
                    break;
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate, SaleStatus status)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue && maxDate.HasValue && status.Equals(SaleStatus.All))
            {
                result = result.Where(x => x.Date >= minDate.Value);
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            switch (status)
            {
                case SaleStatus.Billed:
                    result = result.Where(x => x.Date >= minDate.Value);
                    result = result.Where(x => x.Date <= maxDate.Value);
                    result = result.Where(x => x.Status == SaleStatus.Billed);
                    break;
                case SaleStatus.Pending:
                    result = result.Where(x => x.Date >= minDate.Value);
                    result = result.Where(x => x.Date <= maxDate.Value);
                    result = result.Where(x => x.Status == SaleStatus.Pending);
                    break;
                case SaleStatus.Canceled:
                    result = result.Where(x => x.Date >= minDate.Value);
                    result = result.Where(x => x.Date <= maxDate.Value);
                    result = result.Where(x => x.Status == SaleStatus.Canceled);
                    break;
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
