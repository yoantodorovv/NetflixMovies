using System;
using Movies.Models.Models.Base.Interface;

namespace Movies.Models.Models.Base;

public class BaseEntity<T> : IBaseEntity<T>, IAuditEntity
{
    public T Id { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }
}