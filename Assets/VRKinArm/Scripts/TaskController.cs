using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [Header("Properties")]
    public int dotId;

    private Vector3 defaultRotation;

    private MeshRenderer meshRenderer;

    [SerializeField]
    private Material selectedMaterial;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Animator animator;

    public bool isSelected = false;

    void Awake()
    {
        defaultRotation = transform.localEulerAngles;

        if (!meshRenderer)
            meshRenderer = GetComponent<MeshRenderer>();

        if (!audioSource)
            audioSource = GetComponent<AudioSource>();

        if (!animator)
            animator = GetComponent<Animator>();
    }

    
    public bool SetSelectedDot()
    {
        bool flag = false;
        // Play animation
        StartCoroutine(PlayAnimation());

        if (!isSelected)
        {
            // Change material
            meshRenderer.material = selectedMaterial;

            //Play audio
            audioSource.Play();

            // Change state
            isSelected = true;
            flag = true;
        }

        return flag;
    }

    private IEnumerator PlayAnimation()
    {
        animator.enabled = true;

        yield return new WaitForSeconds(2.0f);

        animator.enabled = false;

        transform.localEulerAngles = defaultRotation;

    }


}
