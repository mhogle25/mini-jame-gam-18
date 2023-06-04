using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : EntityController
{
    public abstract void SetupDestination(PlayerController player);
}
