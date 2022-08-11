using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class hitSound : MonoBehaviour
{

	SerialPort sp = new SerialPort("COM3", 9600); // set port of your arduino connected to computer
	public bool hit = false;
	private char[] txChars = { 'A', 'U' };
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
				if (hit == true)
				{
					sp.Write(txChars, 0, 1);
					
				}
			}
			catch (System.Exception)
			{
			}
		}
	}
}
