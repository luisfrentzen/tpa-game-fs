using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodLogic : MonoBehaviour
{

	// Start is called before the first frame update
	int[,] arr = new int[100, 100];
	public GameObject floor;
	public GameObject trap;

	public int x, y, rasio;

	int isFound = 0;
	int maxY = 0;

	void generate()
    {
		for(int a = 0; a < y+2; a++)
        {
			for(int b = 0; b < x; b++)
            {
				if(arr[a,b] == 0 || arr[a,b] == 2) 
                {
					GameObject safe = Instantiate(floor);
					safe.transform.position = new Vector3(a * 3, 0, b * 3);
                }
				else if(arr[a,b] == 1)
                {
					GameObject notSafe = Instantiate(trap);
					notSafe.transform.position = new Vector3(a * 3, 0, b * 3);
                }
            }
        }
    }

	void floodfill(int xa, int ya)
	{
		if (xa < 0 || xa > x - 1 || ya < 0 || ya > y + 2)
		{
			return;
		}
		if (arr[ya, xa] == 1 || arr[ya, xa] == 2)
		{
			return;
		}
		if (ya == y + 1)
		{
			isFound = 1;
			return;
		}

		if (ya > maxY)
		{
			maxY = ya;
		}

		arr[ya, xa] = 2;

		floodfill(xa + 1, ya);
		floodfill(xa - 1, ya);
		floodfill(xa, ya - 1);
		floodfill(xa, ya + 1);
	}

	int count = 0;

	int[] tempArr = new int[1000];

	public void initMap()
	{
		int trapCount;
		do
		{
			count++;
			//hitung jumlah trap
			int tileCount = x * y;
			trapCount = tileCount / rasio;

			trapCount -= x - 1;
			//nolin semua
			//0 = safe, 1 = trap
			for (int a = 0; a < y + 2; a++)
			{
				for (int b = 0; b < x; b++)
				{
					arr[a, b] = 0;
				}
			}

			//random lastrow
			int temp = Random.Range(0, x - 1);
			for (int a = 0; a < x; a++)
			{
				if (a != temp)
				{
					arr[y, a] = 1;
				}
			}

			//random sisanya
			int counter = 0;
			int randLength = x * y - 3;
			int prevPoint = 0;
			//random trap array ( random 1 trap setiap [ratio] tile )
			//random mulai dari checkpoint terakhir
			for (int a = (maxY > 0 ? maxY - 1 : maxY) * x; a < randLength; a++)
			{
				tempArr[a] = 0;
				if ((a + 1) % rasio == 0)
				{
					int tempTrap = (Random.Range(0, rasio - 1)) + prevPoint;
					tempArr[tempTrap] = 1;
					trapCount--;
					prevPoint = a + 1;
				}
			}

			//copy temparr > id
			// translate tempArr ke array asli ( 2d )
			int co = 0;
			for (int a = 1; a < y - 1; a++)
			{
				for (int b = 0; b < x; b++)
				{
					arr[a, b] = tempArr[((a - 1) * x) + b];
				}
			}

			//floodfill dan save checkpoint y terjauh
			floodfill(0, 0);

			//jika tidak memenuhi ulangi, dg catatan yang sudah benar / diatas checkpoint
			//tidak perlu diulang lagi
			//a.k.a random hanya dari checkpoint

			if (count % 1000 == 0)
			{
				maxY = 0;
			}
		}
		while (isFound == 0);
	}

	void Start()
	{
		x = 5;
		y = 15;
		rasio = 2;

		initMap();
		generate();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
