using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject shellSpawnPos;
    public GameObject target;
    public GameObject parent;
    float speed = 15;
    float turnSpeed = 2;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void CanShootAgain()
    {
        canShoot = true;
    }
    void Fire()
    {
        if (canShoot)
    {
         GameObject shell = Instantiate(shellPrefab, shellSpawnPos.transform.transform.position, shellSpawnPos.transform.rotation);
         shell.GetComponent<Rigidbody>().velocity = speed * this.transform.forward;
         canShoot = false;
         Invoke("CanShootAgain", 0.2f);
    }
       
    }
    // Update is called once per frame
    void Update()
    {
       
     
        Vector3 direction = (target.transform.position - parent.transform.position).normalized;
        Quaternion lookRotaion = Quaternion.lookRotaion(new Vector3(direction.x, 0, direction.z));
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRotaion, Time.deltaTime * turnSpeed);

        float? angle = RotateTurret();

        if (angle != null && Vector3.Angle(direction, parent.transform.forward) <10)
          Fire();
    }
    float RotateTurret()
    {
        float? angle = CalculateAngle(false);
        if(angle != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, 0f,0f);
        }
        return angle;
    }
    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.9f;
        float sSqr = speed * speed;
        float underTheSqeRot = (sSqr * sSqr) - gravity * (gravity * x *x +2 * y * sSqr);
        if (underTheSqeRot >= 0f)
        { 
            float root = (Mathf.sSqr(underTheSqeRot));
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
            return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
            return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        }
        else 
        return null;
    }
}
