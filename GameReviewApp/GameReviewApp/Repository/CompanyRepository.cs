using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameReviewApp.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private DataContext _context;
        public CompanyRepository(DataContext context)
        {
            _context = context;
        }

        public bool CompanyExists(int id)
        {
            return _context.Companies.Any(c => c.Id == id);
        }

        public ICollection<Company> GetCompanies()
        {
            return _context.Companies.ToList();
        }

        public Company GetCompany(int id)
        {
            return _context.Companies.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Game> GetGamesByCompany(int companyId)
        {
            return _context.Games.Where(c => c.CompanyId == companyId).ToList();
        }
    }
}
