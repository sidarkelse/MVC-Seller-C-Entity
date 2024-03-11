using Microsoft.EntityFrameworkCore;
using Mvc.Data;
using Mvc.Models;

namespace Mvc.Services
{
    public class DepartmentService
    {
        private readonly MvcContext _context;
        public DepartmentService(MvcContext context)
        {
            _context = context;
        }

        public  async Task <List<Department>> FindAllAsync()

        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();   

        } 
    }
}
