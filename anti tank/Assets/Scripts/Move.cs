using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Move : MonoBehaviour
{

	SerialPort sp = new SerialPort("COM3", 9600); // set port of your arduino connected to computer
	public int walkSpeed = 10;
	void Start()
	{
		sp.Open();
		sp.ReadTimeout = 1;
	}

	void Update()
	{

		if (sp.IsOpen)
		{
			try
			{
				if (sp.ReadByte() == 1)
				{
					transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
				}
				if (sp.ReadByte() == 2)
				{
					transform.Translate(Vector3.left * Time.deltaTime * walkSpeed);
				}
				if (sp.ReadByte() == 3)
				{
					transform.Translate(Vector3.back * Time.deltaTime * walkSpeed);
				}
				if (sp.ReadByte() == 4)
				{
					transform.Translate(Vector3.right* Time.deltaTime * walkSpeed);
				}
			}
			catch (System.Exception)
			{
			}
		}
	}
}
