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

        public ICollection<Game> GetGamesByCompanyId(int companyId)
        {
            return _context.Games.Where(c => c.CompanyId == companyId).ToList();
        }

        bool ICompanyRepository.CreateCompany(Company company)
        {
            if (CompanyExists(company.Id))
                return false;
            _context.Add(company);
            return _context.SaveChanges() > 0;
        }

        bool ICompanyRepository.DeleteCompany(Company company)
        {
            _context.Remove(company);
            return _context.SaveChanges() > 0;
        }

        bool ICompanyRepository.UpdateCompany(int companyId, Company company)
        {
            _context.Update(company);
            return _context.SaveChanges() > 0;
        }
    }
}
