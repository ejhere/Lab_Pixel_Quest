using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeoContoller : MonoBehaviour {
    public int variable1 = 2;

    // color change fields
    public SpriteRenderer targetRenderer;
    public Color keyColor = Color.red;
    public Color secondColor = Color.green;
    public Color thirdColor = Color.blue;
    public KeyCode changeKey = KeyCode.Alpha1;
   

    private Color originalColor;
    private int colorIndex = 0;
    public Color[] cycleColors;

    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();

        // Try to get the SpriteRenderer if one wasn't assigned in the Inspector
        if (targetRenderer == null) {
            targetRenderer = GetComponent<SpriteRenderer>();
        }

        if (targetRenderer != null) {
            originalColor = targetRenderer.color;
            // initialize cycle: 0 = original, 1 = keyColor, 2 = secondColor, 3 = thirdColor
            cycleColors = new Color[4] { originalColor, keyColor, secondColor, thirdColor };
            colorIndex = 0;
        }
    }

    // Update is called once per frame
    void Update() {
        float xInput = Input.GetAxis("Horizontal");
        xInput *= variable1;
        rb2d.velocity = new Vector2(xInput, rb2d.velocity.y);

        // Change/cycle color when key pressed
        if (Input.GetKeyDown(changeKey) && targetRenderer != null) {
            colorIndex = (colorIndex + 1) % 4; 
            targetRenderer.color = cycleColors[colorIndex];
        }
    }

    public string nextLevel = "Level2";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");

        switch (collision.tag) {
            case "Death": {
                string thisLevel = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(thisLevel);
                break;
            }
            case "Finish": {
                SceneManager.LoadScene(nextLevel);
                break;
            }
        }
    }
}
