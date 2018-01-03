using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSwitch : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    private Vector3 moveAwayPos;

	public Image imageLifebarBackground;
	public Sprite lifebarBackgroundBonnie;
	public Sprite lifebarBackgroundBazooka;
    Color player1AlphaChange;
    Color player2AlphaChange;

    bool player1Active = true;

    // Use this for initialization
    void Start () {
        player1AlphaChange = player1.GetComponent<SpriteRenderer>().color;
        player1AlphaChange.a = 255f;
        player2AlphaChange = player2.GetComponent<SpriteRenderer>().color;
        player2AlphaChange.a = 0f;
        player2.transform.position = moveAwayPos;
        foreach (Behaviour childCompnent in player2.GetComponentsInChildren<Behaviour>())
        {
            childCompnent.enabled = false;
        }
        player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    private void FixedUpdate()
    {
        if (player1Active)
            moveAwayPos = player1.transform.position;
        else
            moveAwayPos = player2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        player1.GetComponent<SpriteRenderer>().color = player1AlphaChange;
        player2.GetComponent<SpriteRenderer>().color = player2AlphaChange;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (player1Active == true)
            {
                player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                foreach (Behaviour childCompnent in player2.GetComponentsInChildren<Behaviour>())
                {
                    childCompnent.enabled = true;
                }

                player2.transform.position = new Vector3(player1.GetComponent<Rigidbody2D>().position.x,
                player1.GetComponent<Rigidbody2D>().position.y);

                foreach (Behaviour childCompnent in player1.GetComponentsInChildren<Behaviour>())
                {
                    childCompnent.enabled = false;
                }
                player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                player1.transform.position = moveAwayPos;
                player1Active = false;
                player1AlphaChange.a = 0f;
                player2AlphaChange.a = 255f;
                imageLifebarBackground.sprite = lifebarBackgroundBazooka;

            }
            else
            {
                player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                foreach (Behaviour childCompnent in player1.GetComponentsInChildren<Behaviour>())
                {
                    childCompnent.enabled = true;
                }

                player1.transform.position = new Vector3(player2.GetComponent<Rigidbody2D>().position.x,
                player2.GetComponent<Rigidbody2D>().position.y);

                foreach (Behaviour childCompnent in player2.GetComponentsInChildren<Behaviour>())
                {
                    childCompnent.enabled = false;
                }
                player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                player2.transform.position = moveAwayPos;
                player1Active = true;
                player2AlphaChange.a = 0f;
                player1AlphaChange.a = 255f;
                imageLifebarBackground.sprite = lifebarBackgroundBonnie;
            }
        }
    }
}
