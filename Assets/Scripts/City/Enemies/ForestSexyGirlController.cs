using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSexyGirlController : SexyGirlController
{
    public GameObject kissHeart2;
    private Rigidbody2D kissHeartBody2;
    private Transform kissHeartTransform2;

    public GameObject kissHeart3;
    private Rigidbody2D kissHeartBody3;
    private Transform kissHeartTransform3;

    public GameObject kissHeart4;
    private Rigidbody2D kissHeartBody4;
    private Transform kissHeartTransform4;

    private new void Start()
    {
        base.Start();

        kissHeartBody2 = kissHeart2.GetComponent<Rigidbody2D>();
        kissHeartBody3 = kissHeart3.GetComponent<Rigidbody2D>();
        kissHeartBody4 = kissHeart4.GetComponent<Rigidbody2D>();

        kissHeartTransform2 = kissHeart2.transform;
        kissHeartTransform3 = kissHeart3.transform;
        kissHeartTransform4 = kissHeart4.transform;
    }

    protected override void Kiss()
    {
        if (startKiss)
        {
            kissHeart.SetActive(true);
            kissHeart2.SetActive(true);
            kissHeart3.SetActive(true);
            kissHeart4.SetActive(true);

            kissHeartTransform.position = mouthPosition;
            kissHeartTransform2.position = mouthPosition;
            kissHeartTransform3.position = mouthPosition;
            kissHeartTransform4.position = mouthPosition;

            kissSound.Play();

            kissHeartBody.velocity = new Vector2(2.0f, 0.0f);
            kissHeartBody2.velocity = new Vector2(-2.0f, 0.0f);
            kissHeartBody3.velocity = new Vector2(1.4142f, 1.4142f);
            kissHeartBody4.velocity = new Vector2(-1.4142f, 1.4142f);
        }
        else
        {
            if (kissHeart.activeSelf)
            {
                kissHeart.SetActive(false);
            }
            if (kissHeart2.activeSelf)
            {
                kissHeart2.SetActive(false);
            }
            if (kissHeart3.activeSelf)
            {
                kissHeart3.SetActive(false);
            }
            if (kissHeart4.activeSelf)
            {
                kissHeart4.SetActive(false);
            }
        }
    }
}
