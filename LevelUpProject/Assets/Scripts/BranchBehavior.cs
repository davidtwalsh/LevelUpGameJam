using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchBehavior : MonoBehaviour
{
    public bool isLit = false;
    public GameObject fireParticleGameobject;
    float timeAlive = 0;
    bool hasDetachedFire = false;

    public float shrinkFactor;
    public float minXScale;

    ParticleSystem fireParticleSystem;
    public GameObject fireLight;

    // Start is called before the first frame update
    void Start()
    {
        fireParticleSystem = fireParticleGameobject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (isLit == true && hasDetachedFire == false)
        {
            hasDetachedFire = true;
            //activate particle here
            fireParticleGameobject.SetActive(true);
            fireParticleGameobject.transform.parent = null;
            fireParticleGameobject.transform.localScale = new Vector3(1, 1, 1);
            fireLight.SetActive(true);
        }
        if (hasDetachedFire == true)
        {
            fireParticleGameobject.transform.position = transform.position;
            Quaternion rot = Quaternion.Euler(-90, transform.eulerAngles.y, transform.eulerAngles.z);
            fireParticleGameobject.transform.rotation = rot;
        }

        //shrine wood and fire x scale over time
        if (isLit == true)
        {
            transform.localScale = new Vector3(transform.localScale.x - shrinkFactor * Time.deltaTime, transform.localScale.y, transform.localScale.z);

            //fireParticleSystem.shape.scale.x = transform.localScale.x;
            //fireParticleSystem.shape.scale = new Vector3(1, 1, 1);
            ParticleSystem.ShapeModule editableShape = fireParticleSystem.shape;
            Vector3 newBox = new Vector3(transform.localScale.x, .3f, 1f);
            editableShape.scale = newBox;

            //destroy if its small enough
            if (transform.localScale.x < minXScale)
            {
                Destroy(fireParticleGameobject);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lighter" && timeAlive > .5f)
        {
            isLit = true;
        }
    }
}
