using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    public class Magazine : MonoBehaviour
    {
        public Transform magazine;
        public Rifle rifle;
        public Vector3 magazineScale;

        public void Awake()
        {
            magazineScale = magazine.localScale;
        }
        public void OnEnable()
        {
            rifle.OnShoot += ReduceMag;
        }

        public void OnDisable()
        {
            rifle.OnShoot -= ReduceMag;
        }

        public void ReduceMag()
        {
            magazine.localScale -= new Vector3(0,magazineScale.y/30f,0);
            //magazine.position -= new Vector3(magazine.localPosition.x,magazine.localPosition.y / 2,magazine.localPosition.z);
        }
    }
}

