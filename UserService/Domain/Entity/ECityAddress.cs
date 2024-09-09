using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entity;

public class ECityAddress : BaseEntity
{
    public string CityName { get; set; }
    public string? CityCode { get; set; }
    [ForeignKey("Country")]
    public Guid CountryId { get; set; }
}
