namespace PChat.Domain.Abstractions;

public abstract class BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set;}
}
