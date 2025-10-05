namespace MvcProgram.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Orders
{
[Key]
public Guid OrderID { get; set; }

    public enum OrderStatus
    {
        Pending,
        Approved,
        Rejected,
        Completed
    }
public OrderStatus Status {get ; set ; } = OrderStatus.Pending ;

[ForeignKey(nameof(Requester))]
public string? ReqestID {get ; set ; }

[ForeignKey(nameof(Responseder))]
public string? ResponseID {get ; set ; }

public applicationUser? Requester { get; set; }
public applicationUser? Responseder { get; set; }

}
public class orderItem
{

    [Key]
    public Guid orderItemID { get; set; }

    [ForeignKey(nameof(exchange))]
    public Guid exchangeID { get; set; }

    [ForeignKey(nameof(order))]
    public Guid orderID { get; set; }


    public exchange exchange { get; set; }
    public Orders order {  get; set; }

}

public class exchange
{
    [Key]
    public Guid exchangeID { get; set; }


    [ForeignKey(nameof(requestProduct))]
    public int? ReqestID {get ; set ; }

    [ForeignKey(nameof(responseProduct))]   
    public int? ResponseID {get ; set ; }

    public Product requestProduct { get; set; }
    public Product responseProduct { get; set; } 


}

