using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBooks.Models
{
    public class Category
    {
        //Data annotation that allows us to set this attribute as the primary key
        [Key]
        public int Id { get; set; }
        //DA that allows us to make sure this field is not omitted. Equivalent to not null in SQL.
        [Required]
        [DisplayName("Category's name")]
        public string name { get; set; }

        /// This property allows us to define the name of the attribute in front end
        [DisplayName("Order")] 
        //  This property allows us to set a valid range for our int attribute, as well as an error message.
        [Range(1,100, ErrorMessage = "Order value must be between 1 and 100")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
