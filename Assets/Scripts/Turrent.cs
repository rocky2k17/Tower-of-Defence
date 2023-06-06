
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Turrent : MonoBehaviour
{

    
    private Transform target;

    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f ;



    [Header("Use Bullets(default)")]

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Use Laser")]

    public bool UseLaser = false;


    public int damageOverTime =30;
    public float slowPct = .5f;


    public LineRenderer lineRenderer;

    public ParticleSystem impactEffect;
    public Light impactLight;



    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";


    public Transform partTorotate;
    public float turnSpeed = 10f;


    
    public Transform firePoint;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance  = Mathf.Infinity ; 
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target =nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if(UseLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        //Target Lock On
        LockOnTarget();

        if(UseLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }

        

        
    }

    void LockOnTarget()
    {

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partTorotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partTorotate.rotation = Quaternion.Euler(0f, rotation.y, range);
    }

    void Laser()
    {

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);


        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized ;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.seek(target);
        
        }

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
