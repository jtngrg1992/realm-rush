using UnityEngine;
using TMPro;
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultLabelColor = Color.white;
    [SerializeField] Color disabledLabelColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = true;
        gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        ColorCoordinateLabel();
        ProcessLabelToggle();
    }

    void ProcessLabelToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleLabel();
        }
    }

    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = $"{coordinates.x},{coordinates.y}";
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void ColorCoordinateLabel()
    {
        if (gridManager == null)
        {
            return;
        }

        Node node = gridManager.GetNode(coordinates);

        if (node == null)
        {
            return;
        }

        if (!node.isWalkable)
        {
            label.color = disabledLabelColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultLabelColor;
        }
    }

    void ToggleLabel()
    {
        label.enabled = !label.IsActive();
    }
}
