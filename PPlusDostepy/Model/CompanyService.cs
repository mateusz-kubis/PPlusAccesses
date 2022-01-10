namespace PPlusDostepy.Model;

public interface ICompanyService
{
    List<Company> GetAll();
    Company GetById(int id);
}

public class CompanyService : ICompanyService
{
    private readonly List<Company> _companies;
    public CompanyService()
    {
        //var sampleCompany = new Company()
        //{
        //    Id = 1,
        //    CompanyName = "Test Company",
        //    CompanyDescription = "Test Description for Test Company",
        //    CompanyAccesses = new List<Access> { new Access() { Id = 1, Name = "TestAccess", Description = "Test description for test Access" } }
        //};
        //_company[sampleCompany.Id] = sampleCompany;

        _companies = Controller.DatabaseRequests.GetCompanies();
        foreach (var company in _companies)
        {
            company.CompanyAccesses = Controller.DatabaseRequests.GetAccesses(company.Id);
            _company.Add(company.Id, company);
        }
    }

    private readonly Dictionary<int, Company> _company = new();

    public Company GetById(int id)
    {
        return _company.GetValueOrDefault(id);
    }

    public List<Company> GetAll()
    {
        return _company.Values.ToList();
    }



}
