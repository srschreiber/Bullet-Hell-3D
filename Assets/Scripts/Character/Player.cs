using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class Player : AbstractCharacter
    {
        public float speed = 6.0F;
        public float jumpSpeed = 8.0F;
        //public float gravity = 20.0F;
        private Vector3 moveDirection = Vector3.zero;
        public BulletPool bulletPool;
        public AbstractBullet basicShot;
        
        // Start is called before the first frame update
        void Start()
        {
            bulletPool.registerBullet(basicShot);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var position = transform.position + transform.forward;
                var rotation = transform.rotation;
                var projectile = bulletPool.getFreshBullet(basicShot, position, rotation);
                projectile.Fire(10, transform.forward);
            }

            CharacterController controller = GetComponent<CharacterController>();
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            //moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;
    
            // intersect ray with plane
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);    
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
    }
}