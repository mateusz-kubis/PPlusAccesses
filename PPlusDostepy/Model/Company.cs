namespace PPlusDostepy.Model;

public class Company
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string CompanyDescription { get; set; }

    public List<Access> CompanyAccesses { get; set; }
}
