﻿namespace Users.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; private set; }

    protected Entity() 
    { 
        Id = Guid.NewGuid();
    }
}
