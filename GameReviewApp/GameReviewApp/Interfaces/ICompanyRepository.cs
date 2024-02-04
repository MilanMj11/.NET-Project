using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompany(int id);
        ICollection<Game> GetGamesByCompanyId(int companyId);
        bool CompanyExists(int id);

    }
}
