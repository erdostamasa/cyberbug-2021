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
        }
    }
}