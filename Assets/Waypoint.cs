using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public GameObject[] waypoints;
    int currentWP = 0;

    public float speed = 1.0f; //Esferas para detecção dos pontos por onde o player deve fazer patrol
    float accuracy = 10.0f; //Aproximação do projeto
    float rotSpeed = 0.4f; // Velociade da rotação

    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint"); //tag de identificação
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length == 0) return;
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z); //patrol pelas "waypoints"

        Vector3 direction = lookAtGoal - this.transform.position; //Mudar direção
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed); //Acertar rotação

        if (direction.magnitude < accuracy) // Proximidade entre o Player e o ponto de referência
        {
            currentWP++; // Soma feita para a locomoção até o próximo ponto
            if (currentWP >= waypoints.Length) //Valor das waypoints
            {
                currentWP = 0; //Começo do valor para ir até os próximos
            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime); //Virar para o lado
    }
}
