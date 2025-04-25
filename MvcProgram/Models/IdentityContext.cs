
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MvcProgram.Models;


public class applicationUser : IdentityUser
{

[Required(ErrorMessage ="地址不可空白")]
public string? Address  {get ; set ; }

[Required(ErrorMessage ="出生日期不可空")]
[DataType(DataType.Date)]
public  DateTime BirthDay  {get ; set ; }

[Required(ErrorMessage ="年齡不可為空")]
[Range(0,120,ErrorMessage ="需要在1~120範圍內")]
public int? age{get; set;}
}


public class IdentityContext  : IdentityDbContext<applicationUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

}
