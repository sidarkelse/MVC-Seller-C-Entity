using Mvc.Models;

namespace Mvc.Data
{
    public class SeedingService
    {

        private MvcContext _context;

        public SeedingService(MvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if( _context.Department.Any() || _context.Seller.Any() || _context.SalesRecords.Any()) 
            {

                return; //Db já foi populado, DB has been seeded
            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Robert Diniro", "Roberto@gmail.com", new DateTime(1998, 4, 21), 12000.0, d1);
            Seller s2 = new Seller(2, "Davi Campos", "davi@gmail.com", new DateTime(1928, 9, 01), 3100.0, d2);
            Seller s3 = new Seller(3, "Carla Blink", "carla@gmail.com", new DateTime(1938, 3, 11), 15000.0, d1);
            Seller s4 = new Seller(4, "Zed Shadow", "Zed@gmail.com", new DateTime(1948, 1, 31), 12000.0, d4);
            Seller s5 = new Seller(5, "Robert Garcia", "Robert@gmail.com", new DateTime(1999, 7, 01), 2000.0, d3);
            Seller s6 = new Seller(6, "Alex Domingues ", "Alex@gmail.com", new DateTime(2001, 2, 22), 5000.0, d2);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 09, 25), 51000.0, Models.Enums.SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2018, 09, 12), 12000.0, Models.Enums.SaleStatus.Billed, s2);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2018, 09, 11), 15000.0, Models.Enums.SaleStatus.Billed, s2);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2018, 09, 05), 21000.0, Models.Enums.SaleStatus.Billed, s3);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2018, 09, 15), 41000.0, Models.Enums.SaleStatus.Billed, s5);


            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1,s2,s3,s4,s5);
            _context.SalesRecords.AddRange(r1, r2, r3, r4, r5);

            _context.SaveChanges();
        }
    }
}
