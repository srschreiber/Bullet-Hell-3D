using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class Player : AbstractCharacter
    {
        public float speed = 6.0F;
        public float jumpSpeed = 8.0F;
        public float dodgeCoolDownSeconds = 2F;
        public float dodgeDurationSeconds = 1F;
        public float dodgeSpeedMultiplier = 3F;
        //public float gravity = 20.0F;
        private Vector3 moveDirection = Vector3.zero;
        private float lastDodge = -1;
        
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            float effectiveSpeed = speed;

            if (Input.GetKeyDown(KeyCode.Space) && (lastDodge == -1 || Time.time > lastDodge + dodgeCoolDownSeconds)) {
                lastDodge = Time.time;
            }

            if (lastDodge != -1 && Time.time < lastDodge + dodgeDurationSeconds) {
                effectiveSpeed *= dodgeSpeedMultiplier;
            }

            CharacterController controller = GetComponent<CharacterController>();
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= effectiveSpeed;
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