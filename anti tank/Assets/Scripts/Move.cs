﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Move : MonoBehaviour
{

	SerialPort sp = new SerialPort("COM3", 9600); // set port of your arduino connected to computer
	public int walkSpeed = 10;
	public bool hit = false;
	public int ammo = 3;
	private char[] txChars = { 'H', 'F','M','L','E' };
	void Start()
	{
		sp.Open();
		sp.ReadTimeout = 200;
	}

	void Update()
	{

		if (sp.IsOpen)
		{
			try
			{
                switch (ammo)
                {
					case 0:
						sp.Write(txChars, 4, 1);
						break;

					case 1:
						sp.Write(txChars, 3, 1);
						break;

					case 2:
						sp.Write(txChars, 2, 1);
						break;

					case 3:
						sp.Write(txChars, 1, 1);
						break;
					default:
                        break;
                }
                if (hit == true)
				{
					sp.Write(txChars, 0, 1);
				}
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
