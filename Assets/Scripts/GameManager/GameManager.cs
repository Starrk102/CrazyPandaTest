using System;
using System.Collections;
using System.Collections.Generic;
using TestProjectCrazyPanda.Scripts.Gun;
using UnityEngine;

namespace TestProjectCrazyPanda.Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<GunFire> gun;
        [SerializeField] private Cannonball.Cannonball cannonball;

        private void FixedUpdate()
        {
            if (Time.fixedTime % 1 == 0)
            {
                gun[0].Fire(cannonball.gameObject);
                gun[1].Fire(cannonball.gameObject);
            }
        }
    }
}