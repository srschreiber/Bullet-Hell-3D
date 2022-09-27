using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BulletPool : MonoBehaviour
    {
        private Dictionary<AbstractBullet, List<AbstractBullet>> activeBullets;
        private Dictionary<AbstractBullet, List<AbstractBullet>> inactiveBullets;
        
        // Start is called before the first frame update
        void Start()
        {
            activeBullets = new Dictionary<AbstractBullet, List<AbstractBullet>>();
            inactiveBullets = new Dictionary<AbstractBullet, List<AbstractBullet>>();
        }

        // Update is called once per frame
        void Update()
        {   
            foreach (KeyValuePair<AbstractBullet, List<AbstractBullet>> kvp in activeBullets) {
                List<AbstractBullet> activeBullet = kvp.Value;
                int count = activeBullet.Count;
                int index = 0;
                for (int i = 0; i < count; i++) {
                    if (!activeBullet[index].isActive()) {
                        activeBullet[index].gameObject.SetActive(false);
                        inactiveBullets[kvp.Key].Add(activeBullet[index]);
                        activeBullet.Remove(activeBullet[index]);
                    } else {
                        ++index;
                    }
                }
            }
        }

        // bullet template is the key
        public void registerBullet(AbstractBullet bulletTemplate) {
            activeBullets.Add(bulletTemplate, new List<AbstractBullet>());
            inactiveBullets.Add(bulletTemplate, new List<AbstractBullet>());
        }

        public AbstractBullet getFreshBullet(AbstractBullet bulletTemplate, Vector3 position, Quaternion rotation) {
            // remove from inactive, put in active, return 
            // if not anymore inactive, create a new one (instantiate)

            if (inactiveBullets[bulletTemplate].Count > 0) {
                Debug.Log("Reuse");
                AbstractBullet bullet = inactiveBullets[bulletTemplate][0];
                inactiveBullets[bulletTemplate].Remove(bullet);
                bullet.gameObject.SetActive(true);
                bullet.firedFrom = position;
                bullet.gameObject.transform.position = position;
                bullet.gameObject.transform.rotation = rotation;
                activeBullets[bulletTemplate].Add(bullet);
                return bullet;
            }

            Debug.Log("Initialize fresh bullet");
            var projectile = Instantiate(bulletTemplate, position, rotation);
            activeBullets[bulletTemplate].Add(projectile);
            // initialize new one..
            return projectile;
        }
    }
}