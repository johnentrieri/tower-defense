using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform baseObject;
    [SerializeField] Transform headObject;
    [SerializeField] float viewDistance;
    [SerializeField] float fireRate;

    //private Enemy target;

    void Start() {
        ToggleShoot(false);
    }

    // Update is called once per frame
    void Update() {
        Enemy target = GetClosestTarget();
        LookAtTarget(target);
    }

    public Color GetColor() {
        MeshRenderer meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        return meshRenderer.sharedMaterial.color;
    }

    private void LookAtTarget(Enemy target) {
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
        projectileEmissions.rateOverTime = fireRate;
        projectileEmissions.enabled = isShoot;
    }

    private Enemy GetClosestTarget() {
        Enemy[] enemies = transform.parent.GetComponentsInChildren<Enemy>();      

        foreach(Enemy e in enemies) {
            float enemyDistance = Vector3.Distance(transform.position,e.transform.position);
            if ( (enemyDistance - viewDistance) <= Mathf.Epsilon) { return e; }
        }

        return null;
    }
}
