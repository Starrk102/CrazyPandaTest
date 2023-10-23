using System;
using Unity.Mathematics;
using UnityEngine;

namespace TestProjectCrazyPanda.Scripts.Gun
{
    public class GunFire : MonoBehaviour
    {
        [SerializeField] private GameObject gun;
        [SerializeField] private float force;
        [SerializeField] private Vector2 shotDirection;

        public void Fire(GameObject cannonball)
        {
            var go = Instantiate(cannonball, gun.transform.position, quaternion.identity);
            go.GetComponent<Rigidbody2D>().AddForce(shotDirection * force, ForceMode2D.Impulse);
        }
        
    }
}