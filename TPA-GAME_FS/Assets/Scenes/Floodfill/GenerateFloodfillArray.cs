using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFloodfillArray : MonoBehaviour
{
    public GameObject floor;
    public GameObject trap;
    public int height, width;
    public int ratio;
    int[,] arr;
    int trapCount;
    int randHeight, randWidth;
    int maxY = 0, maxX = 0;

    // Start is called before the first frame update
    void Start()
    {
        arr = new int[height + 2, width + 1];
        trapCount = ((height * width) / ratio) - width + 1;

        //baris terakhir
        for (int i = 0; i < width; i++)
        {
            arr[height, i] = 1;
        }
        //buat warna putih di terakhir
        //rand.nextInt((max - min) + 1 )+ min;

        //spawn character
        maxX = width / 2;

        initTrap();
        do
        {
            if (arr[maxY + 1, maxX] == 1)
            {
                arr[maxY + 1, maxX] = 0;
            }

            floodFill(maxY + 1, maxX);
        } while (maxY < height);
        initMap();
    }

    void floodFill(int y, int x)
    {
        if (y < 0 || y > height || x < 0 || x > width - 1)
        {
            return;
        }

        if (y == height + 1)
        {
            return;
        }
        if (arr[y, x] == 1 || arr[y, x] == 8)
        {
            return;
        }

        if (y > maxY)
        {
            maxY = y;
            maxX = x;
        }

        arr[y, x] = 8;
        //        initMap();

        floodFill(y + 1, x);
        floodFill(y - 1, x);
        floodFill(y, x + 1);
        floodFill(y, x - 1);
    }

    void initTrap()
    {
        for (int i = 0; i < trapCount; i++)
        {
            do
            {
                randHeight = Random.Range(maxY + 1, height);
                randWidth = Random.Range(0, width);
            } while (arr[randHeight, randWidth] == 1);
            arr[randHeight, randWidth] = 1;
        }
    }

    void initMap()
    {
        for (int i = 0; i < height + 2; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (arr[i, j] == 0 || arr[i, j] == 8)
                {
                    GameObject temp = Instantiate(floor);
                    temp.transform.position = new Vector3(i * 3, 0, j * 3);
                }
                else
                {
                    GameObject temp = Instantiate(trap);
                    temp.transform.position = new Vector3(i * 3, 0, j * 3);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}