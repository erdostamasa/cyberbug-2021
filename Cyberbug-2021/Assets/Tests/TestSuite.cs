using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests{
    public class TestSuite{
        GameObject testObject;
        GameObject testPlayer;
        EventManager eventManager;
        
        [SetUp]
        public void Setup(){
            testObject = new GameObject();
            testPlayer = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/PlayerContainer"));
            GameObject em = new GameObject();
            eventManager = em.AddComponent<EventManager>();
        }

        [TearDown]
        public void Teardown(){
            Object.Destroy(testObject);
            Object.Destroy(testPlayer);
            Object.Destroy(eventManager);
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
            ammoManager.MagazineSize = 5;
            ammoManager.AddAmmo(5);
            ammoManager.Reload();
            yield return new WaitForSeconds(0.1f);
            ammoManager.Fire();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(4, ammoManager.AmmoLoaded);
        }
        
        [UnityTest]
        public IEnumerator CantShootWithoutAmmo(){
            AmmoManager ammoManager = testObject.AddComponent<AmmoManager>();
            ammoManager.MagazineSize = 30;
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(0, ammoManager.AmmoLoaded);
            ammoManager.Fire();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(0, ammoManager.AmmoLoaded);
        }

        [UnityTest]
        public IEnumerator ReloadingWeaponSetsCorrectAmmo(){
            AmmoManager ammoManager = testObject.AddComponent<AmmoManager>();
            ammoManager.MagazineSize = 5;
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(0, ammoManager.AmmoLoaded);
            ammoManager.AddAmmo(8);
            yield return new WaitForSeconds(0.1f);
            
            ammoManager.Reload();
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(5, ammoManager.AmmoLoaded);
            Assert.AreEqual(3, ammoManager.AmmoInInventory);
            yield return new WaitForSeconds(0.1f);
            
            for (int i = 0; i < 5; i++){
                ammoManager.Fire();
            }
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(0, ammoManager.AmmoLoaded);
            ammoManager.Reload();
            yield return new WaitForSeconds(0.1f);
            
            Assert.AreEqual(3, ammoManager.AmmoLoaded);
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
        /*
         //Error test
        [UnityTest]
        public IEnumerator TestError()
        {
            Assert.AreEqual(0, 1);
            yield return null;
        }*/
    }
}