using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {


	public float recordTime = 5f;

	bool isRewinding = false;
	List<PointInTime> pointsInTime;

	Rigidbody2D rb;
    RigidbodyType2D default_bodytype;

	// Use this for initialization
	void Start () {
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody2D>();
        default_bodytype = rb.bodyType;
        
	}
	
	// // Update is called once per frame
	// void Update () {
	// 	if (Input.GetKeyDown(KeyCode.Return))
	// 	{
	// 		StartRewind();
	// 	}
	// 	if (Input.GetKeyUp(KeyCode.Return))
	// 	{
	// 		StopRewind();
	// 	}
	// }

	// void FixedUpdate ()
	// {
	// 	if (isRewinding)
	// 		Rewind();
	// 	else
	// 		Record();
	// }

	void Rewind ()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		} else
		{
			StopRewind();
		}
		
	}

	void Record ()
	{
		if (pointsInTime.Count > 2 * Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	public void StartRewind ()
	{
		isRewinding = true;
		Time.timeScale = 2;
		try{gameObject.GetComponent<CharacterController2D>().enabled = false;}
		catch{}
		rb.isKinematic = true;
	}

	public void StopRewind ()
	{
		isRewinding = false;
		Time.timeScale = 1;
		rb.isKinematic = false;
		try{gameObject.GetComponent<CharacterController2D>().enabled = true;}
		catch{}
        rb.bodyType = default_bodytype;
	}
}