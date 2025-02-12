using System;

namespace Movies.Models.Models.Base.Interface;

public interface IBaseEntity<T>
{
    T Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    DateTime? DeletedAt { get; set; }
    bool IsDeleted { get; set; }
}