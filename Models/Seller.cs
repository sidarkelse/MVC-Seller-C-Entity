using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class Seller
    {
        public Seller(int id, string? name, string? email, DateTime birthDay, double baseSalary, Department? department)
        {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDay = birthDay;
            Department = department;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")] // vai pegar o nome do atributo {0}
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string? Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid Email")]
        public string? Email { get; set; }

      
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")] //Formatação F2 de duas casas decimais
        [Required(ErrorMessage = "{0} required")]
        public double BaseSalary { get; set; }

      
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} required")]
        public DateTime BirthDay { get; set; }

     
        public Department? Department { get; set; }

        public int DepartmentId { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        public Seller() { }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);

        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);


        }

        public double TotalSales(DateTime initial, DateTime final)
        {

            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);


        }


    }

}
