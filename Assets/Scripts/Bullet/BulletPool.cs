using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BulletPool : MonoBehaviour
    {
        private Dictionary<string, List<AbstractBullet>> activeBullets;
        private Dictionary<string, List<AbstractBullet>> inactiveBullets;
        
        // Start is called before the first frame update
        void Start()
        {
            activeBullets = new Dictionary<string, List<AbstractBullet>>();
            inactiveBullets = new Dictionary<string, List<AbstractBullet>>();
        } 


        public void MaybeMakeBulletInactive(AbstractBullet bullet) {
            List<AbstractBullet> bullets = activeBullets[bullet.name];
            if (!bullet.isActive()) {
                bullet.gameObject.SetActive(false);
                inactiveBullets[bullet.name].Add(bullet);
                bullets.Remove(bullet);
            }
        }

        // bullet template is the key
        public void registerBullet(string bulletTemplate) {
            activeBullets.Add(bulletTemplate, new List<AbstractBullet>());
            inactiveBullets.Add(bulletTemplate, new List<AbstractBullet>());
        }

        public AbstractBullet getFreshBullet(AbstractBullet bulletTemplate, Vector3 position, Quaternion rotation) {
            // remove from inactive, put in active, return 
            // if not anymore inactive, create a new one (instantiate)

            if (inactiveBullets[bulletTemplate.name].Count > 0) {
                AbstractBullet bullet = inactiveBullets[bulletTemplate.name][0];
                inactiveBullets[bulletTemplate.name].Remove(bullet);
                bullet.gameObject.SetActive(true);
                bullet.firedFrom = position;
                bullet.gameObject.transform.position = position;
                bullet.gameObject.transform.rotation = rotation;
                activeBullets[bulletTemplate.name].Add(bullet);
                return bullet;
            }

            var projectile = Instantiate(bulletTemplate, position, rotation);
            activeBullets[bulletTemplate.name].Add(projectile);
            // initialize new one..
            return projectile;
        }
    }
}