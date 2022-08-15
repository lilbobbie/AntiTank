using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoMovement : MonoBehaviour
{

	SerialPort sp = new SerialPort("COM3", 9600); // set port of your arduino connected to computer
	public int walkSpeed = 10;
	public bool hit = false;
	private char[] txChars = { 'H', 'F','M','L','E' };
	void Start()
	{
		sp.Open();
		sp.ReadTimeout = 25;

	}

	void Update()
	{

		if (sp.IsOpen)
		{
			try
			{
				Movement();
			}
			catch (System.Exception)
			{
			}
		}
	}

	public void HitSound()
    {
		sp.Write(txChars, 0, 1);
    }

	public void AmmoLed(int ammo)
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
	}

	public void Movement()
    {
		if(sp.ReadByte() == 1)
				{
			transform.Translate(Vector3.forward + Camera.main.transform.forward * Time.deltaTime * walkSpeed);
			Debug.Log("Forward");
		}
		if (sp.ReadByte() == 2)
		{
			transform.Translate(Vector3.left - Camera.main.transform.right * Time.deltaTime * walkSpeed);
			Debug.Log("Left");
		}
		if (sp.ReadByte() == 3)
		{
			transform.Translate(Vector3.back - Camera.main.transform.forward* Time.deltaTime * walkSpeed);
			Debug.Log("Back");
		}
		if (sp.ReadByte() == 4)
		{
			transform.Translate(Vector3.right + Camera.main.transform.right  * Time.deltaTime * walkSpeed);
			Debug.Log("Right");
		}

	}
}
