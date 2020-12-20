﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform baseObject;
    [SerializeField] Transform headObject;
    [SerializeField] Transform enemySpawnParent;

    private Enemy target;

    void Start() {
        ToggleShoot(false);
    }

    // Update is called once per frame
    void Update()
    {
        target = enemySpawnParent.GetComponentInChildren<Enemy>();
        LookAtTarget();
    }

    private void LookAtTarget() {
        if (target == null) { 
            ToggleShoot(false);
            return;
        }

        Transform targetObject = target.transform;

        Vector3 baseTargetPosition = new Vector3(
            targetObject.position.x,
            baseObject.position.y,
            targetObject.position.z
        );

        Vector3 headTargetPosition = new Vector3(
            targetObject.position.x,
            targetObject.position.y,
            targetObject.position.z
        );

        baseObject.LookAt(baseTargetPosition);
        headObject.LookAt(headTargetPosition);
        ToggleShoot(true);
    }

    private void ToggleShoot( bool isShoot) {
        ParticleSystem.EmissionModule projectileEmissions = GetComponentInChildren<ParticleSystem>().emission;
        projectileEmissions.enabled = isShoot;
    }
}
