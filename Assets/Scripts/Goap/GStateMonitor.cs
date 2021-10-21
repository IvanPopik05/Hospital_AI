using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GStateMonitor : MonoBehaviour
{
    public string state; // busting
    public float stateStrength; // Сила состояния. Сколько нужно терпеть
    public float stateDecayRate; // Сила распада состояния со временем
    public WorldStates beliefs; // Появилось ли это состояние или нет
    public GameObject resourcePrefab; // префаб лужи
    public string queueName; // Очередь, в которую мы хотим поместить префаб ресурс
    public string worldState; // FreePuddle
    public GAction action; // Действие Go To Toilet

    bool stateFound = false;
    float initialStrength;
    private void Awake()
    {
        initialStrength = stateStrength;
        beliefs = GetComponent<GAgent>().beliefs;
    }

    private void LateUpdate()
    {
        if (action.running) 
        {
            stateFound = false;
            stateStrength = initialStrength;
        }
        if (!stateFound && beliefs.HasState(state)) 
        {
            stateFound = true;
        }

        if (stateFound) 
        {
            stateStrength -= stateDecayRate * Time.deltaTime;
            if (stateStrength <= 0) 
            {
                Vector3 location = new Vector3(transform.position.x,resourcePrefab.transform.position.y,transform.position.z);
                GameObject puddle = Instantiate(resourcePrefab,location,resourcePrefab.transform.rotation);
                stateFound = false;
                stateStrength = initialStrength;
                beliefs.RemoveState(state);
                GWorld.Instance.GetQueue(queueName).AddResource(puddle);
                GWorld.Instance.GetWorld().ModifyState(worldState,1);
            }
        }
    }
}
