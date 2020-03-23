using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPGenerate : MonoBehaviour
{
    public GameObject wall;
    public GameObject floor;

    public int width, height;
    public int rasio;

    int minWidth, minHeight;
    int[,] arr = new int[1000,1000];

	public void bsp(int w, int h, int corX, int corY)
	{
		//1 = hor, 0 = ver
		int sliceOrientation = Random.Range(0,1);
		if (sliceOrientation == 1)
		{
			//potong hor
			if (h >= minHeight * 2)
			{
				//potong hor
				int slicedSize = Random.Range(0, h - (minHeight * 2)) + minHeight;
				int toSlice = slicedSize + corY;
				for (int a = corX; a < corX + w + 1; a++)
				{
					arr[toSlice,a] = 1;
				}

				int door = Random.Range(0, minWidth-1) + corX + 1;
				arr[toSlice,door] = 2;

				bsp(w, toSlice - 1 - corY, corX, corY);
				bsp(w, h - slicedSize, corX, toSlice);
			}
			else if (w >= minWidth * 2)
			{
				//potong ver
				int slicedSize = Random.Range(0, w - (minWidth * 2)) + minWidth;
				int toSlice = slicedSize + corX;
				for (int a = corY + 1; a < corY + h + 1; a++)
				{
					arr[a,toSlice] = 1;
				}

				int door = corY + h - Random.Range(0,minWidth-1) - 1;
				arr[door,toSlice] = 2;

				bsp(toSlice - 1 - corX, h, corX, corY);
				bsp(w - slicedSize, h, toSlice, corY);
			}
			return;
		}
		else
		{
			//potong hor

			if (w >= minWidth * 2)
			{
				//potong ver
				int slicedSize = Random.Range(0, w - (minWidth * 2)) + minWidth;
				int toSlice = slicedSize + corX;
				for (int a = corY + 1; a < corY + h + 1; a++)
				{
					arr[a,toSlice] = 1;
				}

				int door = Random.Range(0,minHeight-1) + corY + 1;
				arr[door,toSlice] = 2;

				bsp(toSlice - 1 - corX, h, corX, corY);
				bsp(w - slicedSize, h, toSlice, corY);

			}
			else if (h >= minHeight * 2)
			{
				//potong hor
				int slicedSize = Random.Range(0, h - (minHeight * 2)) + minHeight;
				int toSlice = slicedSize + corY;
				for (int a = corX; a < corX + w + 1; a++)
				{
					arr[toSlice,a] = 1;
				}

				int door = corX + w - Random.Range(0, minWidth-1) - 1;
				arr[toSlice,door] = 2;

				bsp(w, toSlice - 1 - corY, corX, corY);
				bsp(w, h - slicedSize, corX, toSlice);
			}
			return;
		}
	}

	public void initMap()
	{
		for (int a = 0; a < height; a++)
		{
			for (int b = 0; b < width; b++)
			{
				if (a == 0 || b == 0 || a == height - 1 || b == width - 1)
				{
					arr[a,b] = 1;
				}
				else
				{
					arr[a,b] = 0;
				}
			}

		}
	}

	public void generate()
    {
		for(int a = 0; a < height; a++)
        {
			for(int b = 0; b < width; b++)
            {
				if (arr[a, b] == 1)
				{
					GameObject temp = Instantiate(wall);
					temp.transform.position = new Vector3(a * 6, 0, b * 6);
				}
				else
				{
					GameObject temp = Instantiate(floor);
					temp.transform.position = new Vector3(a * 6, 0, b * 6);
				}
			}
        }
    }

	// Start is called before the first frame update
	void Start()
    {
		width += 2;
		height += 2;
		minHeight = height / rasio;
		minWidth = width / rasio;
		initMap();
		bsp(width,height,0,0);
		generate();
    }

    // Update is called once per frame
}
