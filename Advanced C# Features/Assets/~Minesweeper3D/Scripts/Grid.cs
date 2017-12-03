using UnityEngine;

namespace Minesweeper3D
{
    public class Grid : MonoBehaviour
    {
        public GameObject blockPrefab;

        // Grid Dimensions
        public int width = 10;
        public int height = 10;
        public int depth = 10;

        public float spacing = 1.2f; // How much spacing between each Block

        // Multi-Dimensional Array storing the blocks
        private Block[,,] blocks;

        void Start ()
        {
            GenerateBlocks();
        }

        void Update ()
        {
            // IF left mouse button is up
            if (Input.GetMouseButtonUp(0))
            {
                // IF raycast out from camera hits something
                RaycastHit hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    // Get hit object's block component
                    

                    // CALL SelectedBlock() and pass in the hit block
                    // SelectBlock();
                }
            }
        }

        // Spawns a Block at the position and returns the Block component
        Block SpawnBlock (Vector3 pos)
        {
            GameObject clone = Instantiate(blockPrefab);
            clone.transform.position = pos;

            Block currentBlock = clone.GetComponent<Block>();
            return currentBlock;
        }

        // Spawns the blocks in a grid-like fashion
        void GenerateBlocks ()
        {
            // Create a 3D array to store all the blocks
            blocks = new Block[width, height, depth];

            // Loop through the X, Y and Z axis of the 3D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        // Calculate the halfSize using the array dimensions
                        Vector3 halfSize = new Vector3(width / 2, height / 2, depth / 2);

                        // Offset by half so the elements are centered
                        halfSize -= new Vector3(0.5f, 0.5f, 0.5f);

                        // Create a position for the element to pivot around the Grid's zero point
                        Vector3 pos = new Vector3(x - halfSize.x, y - halfSize.y, z - halfSize.z);

                        // Apply spacing
                        pos *= spacing;

                        // Spawn the Block at that position
                        Block block = SpawnBlock(pos);

                        // Attach the Block to the Grid as a child
                        block.transform.SetParent(transform);

                        // Store the array coordinates inside the Block itself
                        block.x = x;
                        block.y = y;
                        block.z = z;

                        // Store the Block in the array at the specified coordinates
                        blocks[x, y, z] = block;
                    }
                }
            }
        }

        // Count the adjacentMines at the element
        public int GetAdjacentMineCountAt (Block b)
        {
            int count = 0;

            // Loop through all elements and have each axis go between -1 to 1
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        // Calculate the adjacent element's index
                        int desiredX = b.x + x;
                        int desiredY = b.y + y;
                        int desiredZ = b.z + z;

                        // Coordinates in range?
                        if (desiredX >= 0 && desiredY >= 0 && desiredZ >= 0 && desiredX < width && desiredY < height && desiredZ < depth)
                        {
                            // Check for mine
                            Block currentBlock = blocks[desiredX, desiredY, desiredZ];

                            if (currentBlock.isMine)
                            {
                                // Increment the count
                                count++;
                            }
                        }
                    }
                }
            }

            // Return the total recorded at the end of the algorithm
            return count;
        }

        // Flood Fill function to uncover all the empty elements
        public void FFuncover (int x, int y, int z, bool[,,] visited)
        {
            // Coordinates in range?
            if (x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth)
            {
                // Visited already?
                if (visited[x, y, z])
                    return;

                // Uncover element
                Block block = blocks[x, y, z];

                int adjacentMines = GetAdjacentMineCountAt(block);

                block.Reveal(adjacentMines);

                // Close to a mine?
                if (adjacentMines > 0)
                    return;

                // Set a visited flag
                visited[x, y, z] = true;

                // Perform recursion in each axis to detect adjacent elements
                FFuncover(x - 1, y, z - 1, visited);
                FFuncover(x + 1, y, z - 1, visited);
                FFuncover(x, y - 1, z - 1, visited);
                FFuncover(x, y + 1, z - 1, visited);
                FFuncover(x, y, z - 1, visited);

                FFuncover(x - 1, y, z, visited);
                FFuncover(x + 1, y, z, visited);
                FFuncover(x, y - 1, z, visited);
                FFuncover(x, y + 1, z, visited);

                FFuncover(x - 1, y, z + 1, visited);
                FFuncover(x + 1, y, z + 1, visited);
                FFuncover(x, y - 1, z + 1, visited);
                FFuncover(x, y + 1, z + 1, visited);
                FFuncover(x, y, z + 1, visited);
            }
        }

        // Uncovers all mines that are in the Grid
        public void UncoverMines ()
        {
            // Loop through all elements in the array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        // Get currentBlock at index
                        Block currentBlock = blocks[x, y, z];

                        // IF currentBlock is a mine
                        if (currentBlock.isMine)
                        {
                            // Reveal the mine

                        }
                    }
                }
            }
        }

        // Takes in a Block selected by the user in some way to reveal it
        public void SelectBlock (Block selectedBlock)
        {
            // Reveal the selectedBlock


            // IF the selectedBlock is a mine
            if (selectedBlock.isMine)
            {
                // Uncover all other mines
                UncoverMines();
            }
            // ELSE IF there are no adjacentMines
            // else if ()
            {
                // Perform the Flood Fill algorithm to reveal all empty blocks
                // FFuncover();
            }
        }
    }
}
