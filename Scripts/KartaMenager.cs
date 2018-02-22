using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartaMenager : MonoBehaviour {



    public int ID;
    public int COLOR=1; //1-serce; 2-dzwonek 3-trefl; 4-wino;
    public bool PlayerBool=true;


    public Texture[] textureS;
    public Texture[] textureD;
    public Texture[] textureZ;
    public Texture[] textureW;
    private Renderer rend;

    private GameMenager GM;

   
    void Start ()
    {
        GM = GameObject.FindWithTag("GameController").GetComponent<GameMenager>();
        rend = GetComponent<Renderer>();

        if(PlayerBool)
        ZmnienWartosc(GM.ID_CL1);
        else ZmnienWartosc(GM.ID_CL2);


    }

    public void ZmnienWartosc(int id_cl)
    {
        ID = id_cl % 14;
        while (id_cl > 14)
        {
            COLOR += 1;
            id_cl -= 14;
        }
        



        

        switch (COLOR)
        {
            case 1:
                {
                    rend.material.mainTexture = textureS[ID];
                    rend.material.mainTextureScale = new Vector2(-1, 1);
                }
                break;
            case 2:
                {
                    rend.material.mainTexture = textureD[ID];
                    rend.material.mainTextureScale = new Vector2(-1, 1);
                }
                break;
            case 3:
                {
                    rend.material.mainTexture = textureZ[ID];
                    rend.material.mainTextureScale = new Vector2(-1, 1);
                }
                break;
            case 4:
                {
                    rend.material.mainTexture = textureW[ID];
                    rend.material.mainTextureScale = new Vector2(-1, 1);
                }
                break;

        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && GM.koniec == false)
        {
            Destroy(this.gameObject);
            GM.PokaTablice();
        }
        //Szybka Gra
        if (Input.GetKey(KeyCode.G))
        {
            Destroy(this.gameObject);
        }
    }


}
