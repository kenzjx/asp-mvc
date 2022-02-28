using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace redux.Models.Contacts
{
    public class Contact
    {
        [Key]
        public int Id { set; get; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Phải nhập họ tên {0}")]
        [Display(Name = "Họ và Tên")]
        public string FullName { set; get; }

        [Required(ErrorMessage = "Phải nhập Email {0}")]
        [StringLength(100)]
 [Display(Name ="Đia chỉ Email")]
        [EmailAddress(ErrorMessage = "Phải là địa chỉ email")]
        public string Email { set; get; }

        [Display(Name ="Ngày gửi")]
        public DateTime DateSend { set; get; }
        [Display(Name = "Nội dung")]
        public string Message { set; get; }

        [Phone(ErrorMessage = "Phải là số điện thoại")]
        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string Phone { set; get; }
    }
}