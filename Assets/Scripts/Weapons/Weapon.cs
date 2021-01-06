using Games.Global.Weapons;
using UnityEngine;

namespace Weapons
{
    public class Weapon
    {
        public string weaponName;
        public int weaponId;
        public int damages;
        public float rateOfFire;

        public int projSpeed;
        public int projLifeTime;

        public int modelId;
        public Effect effect;

        public void Shoot(GameObject player, float cameraEulerAngleX)
        {
            GameObject proj = ObjectPooler.SharedInstance.GetPooledObject(modelId);
            proj.transform.position = player.transform.position;

            Vector3 eulerAngle = proj.transform.localEulerAngles;
            eulerAngle.x = cameraEulerAngleX;
            eulerAngle.y = player.transform.localEulerAngles.y;
            eulerAngle.z = 0.0f;
            proj.transform.localEulerAngles = eulerAngle;

            ProjectileController projectileController = proj.GetComponent<ProjectileController>();
            projectileController.projectileSpeed = projSpeed;
            projectileController.projectileDamage = damages;
            projectileController.projectileDuration = projLifeTime;
            projectileController.effect = effect;

            proj.SetActive(true);
        }
    }
}
