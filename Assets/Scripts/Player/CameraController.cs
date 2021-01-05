using System;
using System.Collections;
using Games.Global.Weapons;
using UnityEngine;
using Weapons;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        private float speed = 5f;
        
        private bool wantToGoForward;
        private bool wantToGoBackward;
        private bool wantToGoRight;
        private bool wantToGoLeft;

        private bool wantToShoot;
        private bool canShoot;
        
        private float projectileCooldown = 1f;

        private Weapon weapon;
        
        public void InitPlayer()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            canShoot = true;

            weapon = WeaponList.GetWeaponById(1);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                wantToGoForward = true;
            } 
            if (Input.GetKeyDown(KeyCode.Q))
            {
                wantToGoLeft = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                wantToGoBackward = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                wantToGoRight = true;
            }

            wantToShoot = Input.GetMouseButton(0);
            
            if (Input.GetKeyUp(KeyCode.Z))
            {
                wantToGoForward = false;
            }
            
            if (Input.GetKeyUp(KeyCode.Q))
            {
                wantToGoLeft = false;
            }
            
            if (Input.GetKeyUp(KeyCode.S))
            {
                wantToGoBackward = false;
            }
            
            if (Input.GetKeyUp(KeyCode.D))
            {
                wantToGoRight = false;
            }
        }

        private void FixedUpdate()
        {
            if (wantToGoForward)
            {
                player.transform.position += player.transform.forward * speed * Time.deltaTime;
            }

            if (wantToGoBackward)
            {
                player.transform.position -= player.transform.forward * speed * Time.deltaTime;
            }

            if (wantToGoLeft)
            {
                player.transform.position -= player.transform.right * speed * Time.deltaTime;
            }

            if (wantToGoRight)
            {
                player.transform.position += player.transform.right * speed * Time.deltaTime;
            }

            if (wantToShoot && canShoot && weapon != null)
            {
                StartCoroutine(ProjectileCooldown());
                
                weapon.Shoot(player, transform.localEulerAngles.x);
            }
        }

        private IEnumerator ProjectileCooldown()
        {
            canShoot = false;
            yield return new WaitForSeconds(weapon.rateOfFire);
            canShoot = true;
        }
    }
}
