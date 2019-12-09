using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSpeed : MonoBehaviour
    {
        private ParticleSystem p;
        private int flagValue;

        void Start()
            {

                p = GetComponent<ParticleSystem>();
                var v = p.main;
                v.startSpeed = new ParticleSystem.MinMaxCurve(4, 5);
            }

        void Update()
            {
                if (Input.GetKeyDown (KeyCode.Z))
                    {
                       var v = p.main;
                       v.startSpeed = new ParticleSystem.MinMaxCurve(12, 15);
                    }
            }

        private void speed()
            {
                //p.Clear;
                var v = p.main;
                v.startSpeed = new ParticleSystem.MinMaxCurve(12, 15);
            }

        public void speedUp()
            {
                speed();
            }
     }
