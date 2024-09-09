using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entity;

public class EShippingAddress : BaseEntity
{
    [ForeignKey("City")]
    public Guid CityId { get; set; }
    public string StreetName { get; set; }
    public string? HouseId { get; set; }

}
