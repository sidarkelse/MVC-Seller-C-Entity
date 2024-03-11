using Microsoft.EntityFrameworkCore;
using Mvc.Data;
using Mvc.Models;
using Mvc.Services.Exceptions;
using System.Data.Common;
using System.Linq;

namespace Mvc.Services
{
    public class SellerService
    {
        private readonly MvcContext _context;

        public SellerService(MvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync(); //acessa o banco de dados pelo entityframework
        }


        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);

        }
        public async Task<Seller> FindByIdAsync(int id)// retorna vendedor que possui id, se não existir retorna nulo
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);


        }
        public async Task Remove(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);//encontrar pelo id
                _context.Seller.Remove(obj);// remover o obj
                await _context.SaveChangesAsync();//entity confirmar a remoção no banco de dados }


            }
            catch(DbUpdateException)
            {
                throw new IntegrityException("Can't Delete Seller Because he/she has sales"); // não pode deletar o vendedor pois ele ainda tem vendas

            }
            }


        public async Task Update(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new NotFoundException("ID not Found");

            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message);

            }

        }


    }
}
