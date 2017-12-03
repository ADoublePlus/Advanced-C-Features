using UnityEngine;

namespace Minesweeper3D
{
    [RequireComponent(typeof(Renderer))]

    public class Block : MonoBehaviour
    {
        // Scores x, y and z coordinate in array
        public int x = 0;
        public int y = 0;
        public int z = 0;

        public bool isMine = false; // Is the current Block a mine?

        private bool isRevealed = false; // Has the Block already been revealed?

        [Header("References")]
        public Color[] textColors;
        public TextMesh textElement;
        public Transform mine;

        private Renderer rend;

        void Awake ()
        {
            rend = GetComponent<Renderer>();
        }

        // Use this for initialization
        void Start ()
        {
            // Detatch textElement from Block
            textElement.transform.SetParent(null);

            // Randomly decide if it's a mine or not
            isMine = Random.value < .05f;
        }

        void UpdateText (int adjacentMines)
        {
            // Are there adjacentMines?
            if (adjacentMines > 0)
            {
                // Set text to the amount of mines
                textElement.text = adjacentMines.ToString();

                // Check if adjacentMines are within textColor's array
                if (adjacentMines >= 0 && adjacentMines < textColors.Length)
                {
                    // Set textColor to whatever was preset
                    textElement.color = textColors[adjacentMines];
                }
            }
        }

        public void Reveal (int adjacentMines)
        {
            // Flags the Block as being revealed
            isRevealed = true;

            // Checks if Block is a mine
            if (isMine)
            {
                // Activate the reference mine
                mine.gameObject.SetActive(true);

                // Detatch mine from children
                mine.SetParent(null);
            }
            else
            {
                // Updates the text to display adjacentMines
                UpdateText(adjacentMines);
            }

            // Deactivates the Block
            gameObject.SetActive(false);
        }
    }
}
