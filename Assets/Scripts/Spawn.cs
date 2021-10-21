using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject patientPrefab;
    public int numPatients;
    public bool hasSpawning = false;
    private void Start()
    {
        for (int i = 0; i < numPatients; i++)
        {
            Instantiate(patientPrefab, transform.position, Quaternion.identity);
        }
        if(hasSpawning)
            InvokeRepeating("SpawnPatient", 5, Random.Range(10, 50));
    }
    private void SpawnPatient() 
    {
        Instantiate(patientPrefab, transform.position, Quaternion.identity);
    }
}
