using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace redux.Models {
 
  public class Category
  {

      [Key]
      public int Id { get; set; }

     
      public int? ParentCategoryId { get; set; }

      // Tiều đề Category
     
      public string Title { get; set; }

      // Nội dung, thông tin chi tiết về Category
     
      public string Content { set; get; }

      //chuỗi Url
      
      public string Slug { set; get; }

      // Các Category con
      public List<Category> CategoryChildren { get; set; }

      [ForeignKey("ParentCategoryId")]
      [Display(Name = "Danh mục cha")]


      public Category ParentCategory { set; get; }

  }
}