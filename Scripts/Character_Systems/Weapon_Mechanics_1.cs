using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Mechanics_1 : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;
    public int ammo = 10;
    public int mag = 3;
    int ammo1;
    public int arm = 0;
    bool skill = true;

    private void Start()
    {
        ammo1 = ammo;
    }
    void Update()
    {
        if( Input.GetMouseButtonDown(arm) && ammo != 0)
        {
            Instantiate(bullet, shotPoint.position, transform.rotation);
            ammo--;
        }
        if( Input.GetKeyDown("r") && mag != 0)
        {
            ammo += ammo1;
            mag--;
        }

        
    }
   
    

}
