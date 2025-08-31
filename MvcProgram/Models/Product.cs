namespace MvcProgram.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Product
{
[Key]
public int? ProductID{get;set;}

[DisplayName("商品名稱")]
[Required(ErrorMessage ="商品不可為空")]
public string? Name {get ; set ; }

[DisplayName("商品價格")]  
[Required(ErrorMessage ="價格不可為空")]
[Range(0 , int.MaxValue , ErrorMessage ="價格必須為非負整數")]
public int? price {get ; set ; }

[DisplayName("商品描述")]
public string? Description { get; set; }

public applicationUser? User { get; set; }

[ForeignKey(nameof(User))]
public string? UID{get;set;}



}

public class User
{
[Key]
[DisplayName("使用者名稱")]
[Required(ErrorMessage ="帳號不可空")]
public string? UserName {get ; set ; }  


[Required(ErrorMessage ="密碼不可空")]
public string? Password  {get ; set ; }

[Required(ErrorMessage ="地址不可空")]
public string? Address  {get ; set ; }

[Required(ErrorMessage ="出生日期不可空")]
[DataType(DataType.Date)]
public  DateTime BirthDay  {get ; set ; }

[Required(ErrorMessage ="年齡不可為空")]
[Range(0,120,ErrorMessage ="需要在1~120範圍內")]

public int? age{get; set;}


}

