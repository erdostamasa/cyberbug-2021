using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests{
    public class TestSuite{
        GameObject testObject;

        [SetUp]
        public void Setup(){
            testObject = new GameObject();
        }

        [TearDown]
        public void Teardown(){
            Object.Destroy(testObject);
        }

        [UnityTest]
        public IEnumerator ShootableTakesDamage(){
            EnemyHealth health = testObject.AddComponent<EnemyHealth>();
            health.MaxHealth = 5;
            yield return new WaitForSeconds(0.1f);
            health.ReceiveProjectile();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(4, health.Health);
        }

        [UnityTest]
        public IEnumerator ShootingUsesAmmo(){
            AmmoManager ammoManager = testObject.AddComponent<AmmoManager>();
            ammoManager.AmmoCount = 60;
            yield return new WaitForSeconds(0.1f);
            ammoManager.Fire();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(59, ammoManager.AmmoCount);
        }

        
        [UnityTest]
        public IEnumerator SwitchingWeapons(){
            GameObject player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/PlayerContainer"));
            WeaponSwitching weapons = player.GetComponentInChildren<WeaponSwitching>();
            weapons.SelectedWeapon = 0;
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(0, weapons.SelectedWeapon);
            yield return new WaitForSeconds(0.1f);
            weapons.SwitchWeapon(2);
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(2, weapons.SelectedWeapon);
        }
    }
}