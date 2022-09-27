using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class AbstractBullet : MonoBehaviour
    {
        public int physicalDamage;

        public Vector3 firedFrom;

        public double range;

        private Rigidbody _rb;

        private void Awake() {
            _rb = GetComponent<Rigidbody>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public bool isActive() {
            // default definition
            return !(Vector3.Distance(firedFrom, transform.position) > range);
        }

        public void Fire(float speed, Vector3 direction) {
            _rb.velocity = direction * speed;
        }
    }
}