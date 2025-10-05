using System;
using System.Collections.Generic;

namespace MvcProgram.Models;

public partial class User
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string BirthDay { get; set; } = null!;

    public int Age { get; set; }
}
