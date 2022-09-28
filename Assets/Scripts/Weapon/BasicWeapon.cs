using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BasicWeapon : MonoBehaviour
    {
        public AbstractCharacter holder;
        public float shootCoolDownSeconds = .2F;
        //public float gravity = 20.0F;
        private Vector3 moveDirection = Vector3.zero;
        public AbstractBullet basicShot;
        private float lastShoot = -1;
        public BulletPool bulletPool;
        
        // Start is called before the first frame update
        void Start()
        {
            bulletPool.registerBullet(basicShot.bulletName);

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0) && (lastShoot == -1 || Time.time > lastShoot + shootCoolDownSeconds))
            {
                lastShoot = Time.time;
                var position = transform.position + transform.forward;
                var rotation = transform.rotation;
                var projectile = bulletPool.getFreshBullet(basicShot, position, rotation);
                projectile.shotBy = holder;
                projectile.Fire(10, transform.forward);
            }
        }
    }
}
