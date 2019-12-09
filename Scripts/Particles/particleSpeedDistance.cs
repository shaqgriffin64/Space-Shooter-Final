using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSpeedDistance : MonoBehaviour
    {
        private ParticleSystem p;
        private int flagValue;

        void Start()
            {

                p = GetComponent<ParticleSystem>();
                var v = p.main;
                v.startSpeed = new ParticleSystem.MinMaxCurve(0.1f, 0.2f);
            }

        void Update()
            {
                if (Input.GetKeyDown (KeyCode.Z))
                    {
                        var v = p.main;
                        v.startSpeed = new ParticleSystem.MinMaxCurve(5, 8);
                    }
            }

        private void speed()
            {
                var v = p.main;
                v.startSpeed = new ParticleSystem.MinMaxCurve(5, 8);
            }

        public void speedUp()
            {
                speed();
            }
    }
