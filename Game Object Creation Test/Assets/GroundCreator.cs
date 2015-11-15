using UnityEngine;
using System.Collections;

public class GroundCreator : MonoBehaviour {

	public GameObject tile;

	public int length;
	public int width;

	public string seed;
	public bool useRandomSeed;
	
	Color[] colorArray = new Color[7];
	GameObject[,] board;
	
	void Start() {

		colorArray [0] = new Color (0.87f, 0.08f, 0.529f, 1.0f);
		colorArray [1] = Color.red;
		colorArray [2] = Color.blue;
		colorArray [3] = Color.green;
		colorArray [4] = new Color (0.298f, 0.039f, 0.451f);
		colorArray [5] = Color.yellow;
		colorArray [6] = new Color (1.0f, 0.455f, 0.0f, 1.0f);

		board = new GameObject[length, width];

		if(useRandomSeed) {
			seed = Time.time.ToString();
		}

		System.Random random = new System.Random(seed.GetHashCode());
		int randomNumber;
		
		for (int x = 0; x < length; x++) {
			for (int y = 0; y < width; y++) {

				board[x,y] = Instantiate(tile);

				randomNumber = random.Next (0,7);
				Color newColor = colorArray[randomNumber];
				MeshRenderer gameObjectRenderer = board[x,y].GetComponent<MeshRenderer>();
				Material newMaterial = new Material(Shader.Find("Standard"));
				newMaterial.color = newColor;
				gameObjectRenderer.material = newMaterial;

				Vector3 temp = new Vector3(10.0f*x,0,-10.0f*y);
				board[x,y].transform.position += temp;

			}
		}

	}
}
