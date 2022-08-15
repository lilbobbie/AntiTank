using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Move : MonoBehaviour
{

	SerialPort sp = new SerialPort("", 9600); // set port of your arduino connected to computer

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
					transform.Translate(Vector3.left * Time.deltaTime * 5);
				}
				if (sp.ReadByte() == 2)
				{
					transform.Translate(Vector3.right * Time.deltaTime * 5);
				}
			}
			catch (System.Exception)
			{
			}
		}
	}
}
