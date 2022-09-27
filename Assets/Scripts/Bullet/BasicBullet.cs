using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BasicBullet : AbstractBullet
    {
        void Start() {
            this.name = "Basic Shot";
            this.physicalDamage = 1;
        }
    }
}