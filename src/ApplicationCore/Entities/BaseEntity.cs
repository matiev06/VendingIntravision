using System.ComponentModel.DataAnnotations;

namespace VendingIntravision.ApplicationCore.Entities;

public abstract class BaseEntity
{
    [Key]
    public virtual int Id { get; protected set; }
}
