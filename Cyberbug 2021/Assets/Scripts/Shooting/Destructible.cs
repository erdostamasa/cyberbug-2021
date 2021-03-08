using UnityEngine;

public class Destructible : MonoBehaviour, IShootable{
    public void ReceiveProjectile(){
        Destroy(this.gameObject);
    }
}