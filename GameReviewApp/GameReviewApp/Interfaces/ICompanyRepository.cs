using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompany(int id);
        ICollection<Game> GetGamesByCompanyId(int companyId);
        bool CompanyExists(int id);
        bool CreateCompany(Company company);
        bool UpdateCompany(int companyId,Company company);
        bool DeleteCompany(Company company);

    }
}
