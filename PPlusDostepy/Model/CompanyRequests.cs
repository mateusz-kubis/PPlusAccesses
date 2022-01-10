using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PPlusDostepy.Model;

public static class CompanyRequests
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.MapGet("/company/getall", CompanyRequests.GetAll)
            .Produces<List<Company>>()
            .WithTags("Companies")
            .AllowAnonymous();
        //.RequireAuthorization();
        app.MapGet("/company/{id}/getall", CompanyRequests.GetById)
            .Produces<Company>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Companies");
            //.RequireAuthorization();

        return app;
    }
    //[Authorize]
    public static IResult GetAll(ICompanyService service)
    {
        var companies = service.GetAll();
        return Results.Ok(companies);
    }
    //[Authorize]
    public static IResult GetById(ICompanyService service, int id)
    {
        var company = service.GetById(id);

        if (company == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(company);
    }
    
}
