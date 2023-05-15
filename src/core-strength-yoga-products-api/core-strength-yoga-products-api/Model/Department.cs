using System.ComponentModel.DataAnnotations;

namespace core_strength_yoga_products_api.Model;

public class Department
{
    [Key]
    public int Id { get; set; }
    public string Departmentname { get; set; }

    public string Base64Image { get; set; }

    public Department()
    {
        
    }
    
    public Department(int id, String departmentName, String base64Image)
    {
        Id = id;
        Departmentname = departmentName;
        Base64Image = base64Image;
    }

}