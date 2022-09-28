using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BasicBullet : AbstractBullet
    {
        void Start() {
            this.physicalDamage = 1;
            this.range = 20;
        }
    }
}