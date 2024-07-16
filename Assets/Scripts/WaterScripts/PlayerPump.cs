using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPump : MonoBehaviour
{
    [SerializeField] GameObject player;
    IInteracteble _interacteble;
    IWaterIneract waterIneract;
    public int PumpForce = 30;
    public float V = 0;
    private bool isPump = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_interacteble != null && Input.GetKey(KeyCode.F))
        {
            _interacteble.Interacte();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        WaterP(collision);

        WaterF(collision);

    }
    private void WaterF(Collider2D collision)
    {
        IInteracteble i = collision.GetComponent<IInteracteble>();
        if (i != null && _interacteble == null)
        {
            _interacteble = i;
        }
    }
    private void WaterP(Collider2D collision)
    {
        IWaterIneract i = collision.gameObject.GetComponent<IWaterIneract>();

        if (i != null && waterIneract == null)
        {
            waterIneract = i;
            if (isPump == false)
            {
                isPump = true;
                StartCoroutine(Pupm());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(Pupm());
        isPump = false;
        waterIneract = null;
        _interacteble = null;

    }

    IEnumerator Pupm()
    {
        yield return new WaitForSeconds(1);
        if(waterIneract != null)
        {
            float i = waterIneract.Pump(PumpForce);
            if (i == 0f) { StopCoroutine(Pupm()); isPump = false; }
            V += i;
            if(player != null)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x +0.01f, player.transform.localScale.y + 0.01f, player.transform.localScale.z);
            }
        }
    }
}
