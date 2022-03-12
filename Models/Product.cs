using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace redux.Models
{
    [Table("Product")]
public class Product
{
    [Key]
    public int ProductId {set; get;}
    public string Name {set; get;}
    public string Description {set; get;}
    public decimal Price {set; get;}
}
}