using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

	private Rigidbody rb;
	private int count;
	public Text winText;
	public Text countText;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		countText.text = "Count: " + count;
		winText.text = "";
	}

	void FixedUpdate () {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement*2);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive(false);
			count++;
			countText.text = "Count: " + count;
		}
		if (count >=6)
		{
			winText.text="YOU WIN BITCH";
		}
	}
	
	boolean isViable(int x, int y)
	{
		if(y==maxY) return true;
		if(!onMap(x,y)) return false;
		
		int color = map[x,y];
		switch(color)
		{
			case 6: player.status=orange;
			case 0: return isViable(x+1,y) || isViable(x-1,y) || isViable[x,y+1];
			case 1: return false;
			case 2: if(checkForYellow(x,y)) return false; return true;
			case 3: player.status=clean;
					return isViable(x+1,y) || isViable(x-1,y) || isViable[x,y+1];
			case 4: return false;
			case 5: return false;
			
		}
	}
	boolean checkForYellow(int x, int y)
	{
		int color1 = map[x-1,y];
		int color2 = map[x+1,y];
		int color3 = map[x,y+1];
		
		if(color1 == 5 || color2 == 5 || color3 == 5)
		{
			return true;
		}
	}
	boolean onMap(x,y)
	{
		if(x>maxX || y> maxY)
			return false;
		else return true;
	}
}
