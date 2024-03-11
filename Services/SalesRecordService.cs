using Microsoft.EntityFrameworkCore;
using Mvc.Data;
using Mvc.Models;

namespace Mvc.Services
{
	public class SalesRecordService
	{

		private readonly MvcContext _context;

		public SalesRecordService(MvcContext context)
		{
			_context = context; 
		}
			
		public async Task <List<SalesRecord>> FindBydateAsync(DateTime? minDate, DateTime? maxDate) 
		{
			var result = from obj in _context.SalesRecords select obj;
			if (minDate.HasValue)
			{
				result = result.Where(x => x.Date >= minDate.Value);


			}
			if (maxDate.HasValue)
			{
				result = result.Where(x => x.Date <= maxDate.Value);


			}
			await _context.SaveChangesAsync();

			return await result
				.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderByDescending(x => x.Date)
				.ToListAsync();//join das tabelas

		}
		public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
		{
			var result = from obj in _context.SalesRecords select obj;
			if (minDate.HasValue)
			{
				result = result.Where(x => x.Date >= minDate.Value);


			}
			if (maxDate.HasValue)
			{
				result = result.Where(x => x.Date <= maxDate.Value);


			}

			return await result
				.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderByDescending(x => x.Date)
				.GroupBy(x => x.Seller.Department)
				.ToListAsync();//join das tabelas

		}
	}
}
