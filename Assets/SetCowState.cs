using UnityEngine;

public class SetCowState : MonoBehaviour
{
    public GameObject cow1;
    //public Button btn1;
    public void setIdle()
    {

        cow1.GetComponent<Animator>().SetInteger("State", 0);
        //cow2.GetComponent<Animator>().SetInteger("State", 0);

    }
    public void setWalk()
    {

        cow1.GetComponent<Animator>().SetInteger("State", 1);
        //cow2.GetComponent<Animator>().SetInteger("State", 1);

    }

    public void setRun()
    {
        cow1.GetComponent<Animator>().SetInteger("State", 2);
        //cow2.GetComponent<Animator>().SetInteger("State", 2);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
