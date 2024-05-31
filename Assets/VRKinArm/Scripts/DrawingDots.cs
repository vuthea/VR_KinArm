using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingDots : MonoBehaviour
{

    public enum DotType { Line, Circle, Diamond}
    public DotType dotType;
    public float radius = 3.0f; 
    public int numDots = 40;
    public float length = 0.5f;
    public Vector3 localRotation = new Vector3(90, 0, 0);
    public Vector3 localScale = Vector3.one;
    public GameObject dot;

    private List<GameObject> lineDots;
    private List<GameObject> circleDots;
    private List<GameObject> diamondDots;

    //Layer mask for dots
    [SerializeField]
    private string layer_mask = "task_dot";

    // Start is called before the first frame update
    void Start()
    {
        if (dotType == DotType.Line)
            CreateLine();
        else if (dotType == DotType.Circle)
            CreateCircle();
        else if (dotType == DotType.Diamond)
            CreateDiamond();       

    }


    public void CreateLine()
    {
        lineDots = new List<GameObject>();

        float _x = 0, _y = 0;

        for (int i = 0; i < numDots; i++)
        {
            _x = i * length;            
            var _dot = GameObject.Instantiate(dot, transform);
            string _dotName = i.ToString();
            _dot.GetComponent<TaskController>().dotId = int.Parse(_dotName);
            _dot.name = _dotName;
            _dot.transform.localPosition = new Vector3(_x, _y, 0);
            _dot.transform.localEulerAngles = localRotation;
            _dot.transform.localScale = localScale;
            _dot.layer = LayerMask.NameToLayer(layer_mask);
            lineDots.Add(_dot);
        }

        //disable after creating dots
        gameObject.SetActive(false);

    }

    public void CreateCircle()
    {
        circleDots = new List<GameObject>();

        for (int i = 0; i < numDots; i++)
        {
            var _degree = i * (360f / numDots);

            float _x = Mathf.Cos(_degree * Mathf.PI / 180) * radius;
            float _y = Mathf.Sin(_degree * Mathf.PI / 180) * radius;
            //Debug.Log(i + " degree:" + _degree + " x: " + _x + " y: " + _y);

            var _dot = GameObject.Instantiate(dot, transform);
            string _dotName = i.ToString();
            _dot.GetComponent<TaskController>().dotId = int.Parse(_dotName);
            _dot.name = _dotName;
            _dot.transform.localPosition = new Vector3(_x, _y, 0);
            _dot.transform.localEulerAngles = localRotation;
            _dot.transform.localScale = localScale;
            _dot.layer = LayerMask.NameToLayer(layer_mask);
            circleDots.Add(_dot);
        }

        //disable after creating dots
        gameObject.SetActive(false);
    }


    public void CreateDiamond()
    {
        diamondDots = new List<GameObject>();

        var _step = numDots / 4f;
        float _x = 0, _xrow = 0;
        float _y = 0, _yrow = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < _step; j++)
            {
                if (i == 0)
                    _x = j * length;
                else if (i == 1)
                    _y = (j + 1) * length;
                else if (i == 2)
                    _x = _xrow - ((j + 1) * length);
                else if (i == 3)
                    _y = _yrow - ((j + 1) * length);

                //Debug.Log(i + " x: " + _x + " y: " + _y);

                var _dot = GameObject.Instantiate(dot, transform);
                string _dotName = i + "" + j;
                _dot.GetComponent<TaskController>().dotId = int.Parse(_dotName);
                _dot.name = _dotName;
                _dot.transform.localPosition = new Vector3(_x, _y, 0);
                _dot.transform.localEulerAngles = localRotation;
                _dot.transform.localScale = localScale;
                _dot.layer = LayerMask.NameToLayer(layer_mask);
                diamondDots.Add(_dot);
            }
            _xrow = _x;
            _yrow = _y;

        }

        //disable after creating dots
        gameObject.SetActive(false);
    }

    public List<GameObject> GetCircleDots()
    {
        return circleDots;
    }

    public List<GameObject> GetDiamondDots()
    {
        return diamondDots;
    }

    public List<GameObject> GetLineDots()
    {
        return lineDots;
    }
}
