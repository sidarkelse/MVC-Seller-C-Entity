using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mvc.Models;

namespace Mvc.Data
{
    public class MvcContext : DbContext
    {
        public MvcContext (DbContextOptions<MvcContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord>  SalesRecords { get; set; }


    }
}
