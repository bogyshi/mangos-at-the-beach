using UnityEngine;
using System.Collections;

public class GroundCreator : MonoBehaviour {

	public int length;
	public int width;

	public string seed;
	public bool useRandomSeed;
	
	Color[] colorArray = new Color[7];
    int[,] map;
	GameObject[,] board;

    void Start() {

        colorArray[0] = new Color(0.87f, 0.08f, 0.529f, 1.0f);
        colorArray[1] = Color.red;
        colorArray[2] = Color.blue;
        colorArray[3] = Color.green;
        colorArray[4] = new Color(0.298f, 0.039f, 0.451f);
        colorArray[5] = Color.yellow;
        colorArray[6] = new Color(1.0f, 0.455f, 0.0f, 1.0f);

        board = new GameObject[length + 4, width];
        map = new int[length, width];

        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random random = new System.Random(seed.GetHashCode());
        do
        {
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    map[x, y] = random.Next(0, 7);
                }
            }

        } while (isViable(0, 0, false));

        Color newColor;
        for (int x = 0; x < length + 4; x++) {
            for (int y = 0; y < width; y++) {
                board[x, y] = GameObject.CreatePrimitive(PrimitiveType.Plane);


                if (x < 4) {
                    newColor = Color.black;
                }
                else {
                    newColor = colorArray[map[x - 4, y]];
                }
                MeshRenderer gameObjectRenderer = board[x, y].GetComponent<MeshRenderer>();
                Material newMaterial = new Material(Shader.Find("Standard"));
                newMaterial.color = newColor;
                gameObjectRenderer.material = newMaterial;

                Vector3 temp = new Vector3(10.0f * x, 0, -10.0f * y);
                board[x, y].transform.position += temp;
            }
        }

        GameObject topWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject rightWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject bottomWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject leftWall = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Vector3 scaleVector = new Vector3(10.0f * (width + 4.2f), width * length, .5f);
        Vector3 posVector = new Vector3((width + 3) * 5, (width * length) / 2, 5f);
        topWall.transform.position += posVector;
        topWall.transform.localScale = scaleVector;

        scaleVector = new Vector3(.5f, width * length, 10.1f * length);
        posVector = new Vector3(10f * (width + 4) - 5, (width * length) / 2, -(length - 1) * 5f);
        rightWall.transform.position += posVector;
        rightWall.transform.localScale = scaleVector;

        scaleVector = new Vector3(10.0f * (width + 4.2f), width * length, .5f);
        posVector = new Vector3((width + 3) * 5, (width * length) / 2, -(length * 10) + 5f);
        bottomWall.transform.position += posVector;
        bottomWall.transform.localScale = scaleVector;

        scaleVector = new Vector3(.5f, width * length, 10.1f * length);
        posVector = new Vector3(-5f, (width * length) / 2, -(length - 1) * 5f);
        leftWall.transform.position += posVector;
        leftWall.transform.localScale = scaleVector;


    }
    
    private bool isViable(int x, int y, bool isOrange)
    {
        if (y == length) return true;
        if (!onMap(x, y)) return false;

        int color = map[x, y];
        switch (color)
        {
            case 6: isOrange = true;
                    return isViable(x + 1, y, isOrange) || isViable(x - 1, y, isOrange) || isViable(x, y + 1, isOrange);
            case 0: return isViable(x + 1, y, isOrange) || isViable(x - 1, y, isOrange) || isViable(x, y + 1, isOrange);
            case 1: return false;
            case 2: if (checkForYellow(x, y)) return false; return true;
            case 3:
                isOrange = false;
                return isViable(x + 1, y, isOrange) || isViable(x - 1, y, isOrange) || isViable(x, y + 1, isOrange);
            case 4: return false;
            case 5: return false;

        }

        return false;
    }
    private bool checkForYellow(int x, int y)
    {
        int color1 = 0;
        int color2 = 0;
        int color3 = 0;

        if (x > 0) {
            color1 = map[x - 1, y];
        }
        if (x < width - 1) {
            color2 = map[x + 1, y];
        }
        if (y < length - 1) {
            color3 = map[x, y + 1];
        }

        if (color1 == 5 || color2 == 5 || color3 == 5)
        {
            return true;
        }
        return false;
    }

    bool onMap(int x, int y)
    {
        if (x > width || y > length)
            return false;
        else return true;
    }
}



