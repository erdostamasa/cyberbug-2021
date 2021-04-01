using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests{
    public class TestSuite{
        [UnityTest]
        public IEnumerator ShootableTakesDamage(){
            GameObject shootable = new GameObject();
            EnemyHealth health = shootable.AddComponent<EnemyHealth>();
            health.MaxHealth = 5;
            yield return new WaitForSeconds(0.1f);
            health.ReceiveProjectile();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(4, health.Health);

            Object.Destroy(shootable);
        }

        [UnityTest]
        public IEnumerator ShootingUsesAmmo(){
            GameObject gun = new GameObject();
            AmmoManager ammoManager = gun.AddComponent<AmmoManager>();
            ammoManager.AmmoCount = 60;
            yield return new WaitForSeconds(0.1f);
            ammoManager.Fire();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(59, ammoManager.AmmoCount);
            Object.Destroy(gun);
        }
    }
}