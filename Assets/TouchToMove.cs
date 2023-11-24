using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToMove : MonoBehaviour {

    Vector3 goalPos;
    float speed = 0.5f;
    float accuracy = 0.25f;
    float rotSpeed = 2f;
    Animator anim;
    public GameObject foodPrefab;
    GameObject food;

    // Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0) && food == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 10000.0f))
            {
                if (hit.collider.gameObject.tag == "ground")

                {
                    goalPos = hit.point;
                    food = Instantiate(foodPrefab, goalPos, Quaternion.identity);
                    Invoke("RemoveFood", 4.0f);
                }
            }
        }
	}

    void RemoveFood()
    {
        Destroy(food);
    }

    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goalPos.x,
                                        this.transform.position.y,
                                        goalPos.z);
        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                Quaternion.LookRotation(direction),
                                                Time.deltaTime * rotSpeed);
        if (Vector3.Distance(transform.position, lookAtGoal) > accuracy)
        {
            this.transform.Translate(0, 0, speed * Time.deltaTime);
            anim.SetBool("isRunning", true);
            anim.SetFloat("speed", 1.0f);
        }
        else
        {
            anim.SetBool("isRunning", false);
            if (food != null)
            {
                anim.SetBool("isEating", true);
            }
        }

        if(food == null)
        {
            anim.SetBool("isEating", false);
        }
    }
}
