using System.Collections;
using System.Collections.Generic;
using UdemyProject1.Pools;
using UnityEngine;

namespace UdemyProject1.Controllers
{
    public class RedDragonController : EnemyController
    {
        public override void PutInPoolEnemy()
        {
            RedDragonPool.Instance.PutInPool(this);
        }
    }
}

