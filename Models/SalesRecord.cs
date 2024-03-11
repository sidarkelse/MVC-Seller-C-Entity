using Mvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public   DateTime Date { get; set; }
		[DisplayFormat(DataFormatString = "{0:F2")]

		public Double Amount { get; set; }

        public SaleStatus MyProperty { get; set; }

        public Seller?  Seller { get; set; }

        public SalesRecord()
        {

        }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus myProperty, Seller? seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
			MyProperty = myProperty;
            Seller = seller;
        }
       
    }
}
