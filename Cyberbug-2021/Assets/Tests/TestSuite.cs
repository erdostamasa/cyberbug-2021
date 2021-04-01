using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests{
    public class TestSuite{
        GameObject testObject;
        GameObject testPlayer;
        
        [SetUp]
        public void Setup(){
            testObject = new GameObject();
            testPlayer = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/PlayerContainer"));
        }

        [TearDown]
        public void Teardown(){
            Object.Destroy(testObject);
            Object.Destroy(testPlayer);
        }

        [UnityTest]
        public IEnumerator EnemyTakesDamage(){
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
            WeaponSwitching weapons = testPlayer.GetComponentInChildren<WeaponSwitching>();
            weapons.SelectedWeapon = 0;
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(0, weapons.SelectedWeapon);
            yield return new WaitForSeconds(0.1f);
            weapons.SwitchWeapon(2);
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(2, weapons.SelectedWeapon);
        }

        [UnityTest]
        public IEnumerator PlayerTakesDamage(){
            PlayerHealth health = testPlayer.GetComponentInChildren<PlayerHealth>();
            health.ReceiveProjectile();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(99, health.Health);
            health.ReceiveProjectile();
            health.ReceiveProjectile();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(97, health.Health);
        }
    }
}