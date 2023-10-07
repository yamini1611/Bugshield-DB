using System;
using System.Collections.Generic;

namespace Bugshield.Models;

public partial class ComputerInfo
{
    public int ComputerId { get; set; }

    public string? ComputerName { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public string? SerialNumber { get; set; }

    public string? OperatingSystem { get; set; }

    public decimal? InstalledRamgb { get; set; }

    public string? Processor { get; set; }

    public string? Ipaddress { get; set; }

    public string? Macaddress { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public DateTime? WarrantyEndDate { get; set; }

    public string? Location { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
