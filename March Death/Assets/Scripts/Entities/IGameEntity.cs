﻿using Storage;

public enum EntityStatus { IDLE, ATTACKING, DEAD };

public interface IGameEntity
{
    EntityInfo info { get; }
    EntityStatus status { get; }

    float damagePercentage { get; }
    float healthPercentage { get; }

    Unit toUnit();
    Building toBuilding();
}