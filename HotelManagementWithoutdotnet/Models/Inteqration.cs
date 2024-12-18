using System;
using System.Collections.Generic;

namespace HotelManagementWithoutdotnet.Models;

public partial class Inteqration
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? IntegrationDetails { get; set; }
}
