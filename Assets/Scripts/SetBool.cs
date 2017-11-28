using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBool : MonoBehaviour
{
	public bool isWalking = false;
    void Update()
    {
		GetComponent<Animator>().SetBool("IsWalking", isWalking);
    }
}
