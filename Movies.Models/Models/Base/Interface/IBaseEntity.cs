using System;

namespace Movies.Models.Models.Base.Interface;

public interface IBaseEntity<T>
{
    T Id { get; set; }
}