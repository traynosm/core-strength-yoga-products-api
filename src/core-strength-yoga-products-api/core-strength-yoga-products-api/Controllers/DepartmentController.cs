using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Model;
using core_strength_yoga_products_api.Model.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace core_strength_yoga_products_api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class DepartmentController : ControllerBase
{
    
    private readonly ILogger<ProductsController> _logger;
    private readonly CoreStrengthYogaProductsApiDbContext _context;

    public DepartmentController(ILogger<ProductsController> logger, CoreStrengthYogaProductsApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }
            
    [Microsoft.AspNetCore.Mvc.HttpGet("Departments")]
    public async  Task<ActionResult<IEnumerable<DepartmentDto>>> Departments()
    {
        var departments = _context.Departments;

        if (departments == null) return NotFound();

        var departmentDtos = new List<DepartmentDto>();

        foreach (var department in departments)
        {
            departmentDtos.Add(DepartmentDto.Resolve(department));
        }

        return departmentDtos.ToList();
    }
}