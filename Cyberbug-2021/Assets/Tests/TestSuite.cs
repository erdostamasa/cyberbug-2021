using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite{

        [UnityTest]
		[Timeout(1000000)]
        public IEnumerator EnemyTakesDamage(){
            GameObject enemy = new GameObject();
            EnemyHealth health = enemy.AddComponent<EnemyHealth>();
            health.MaxHealth = 10;
            yield return new WaitForEndOfFrame();
            health.ReceiveProjectile();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(9, health.Health);
            
            Object.Destroy(enemy);
        }
    }
}
