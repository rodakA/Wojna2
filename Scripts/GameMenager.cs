using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenager : MonoBehaviour {

    private int[] MainDeck = new int [56];
    private int[] deck1 = new int[56];
    private int[] deck2 = new int[56];

    private int[] ddeck1 = new int[56];
    private int[] ddeck2 = new int[56];

    private int[] battle1 = new int[16];
    private int[] battle2 = new int[16];

    public int ID_CL1, ID_CL2;
    private int ID1, ID2;
    private int pozDeck1=0,pozDeck2=0, fillPoz1, fillPoz2;
    private float layerToDysplay=0.0f;
    public bool endGame = false;
    private bool boolCard1= true, boolCard2 = true, LEFT_win=false, RIGHT_win=false, ability=false;

    public GameObject card;
    public GameObject card2;
    public Transform spawn1 , spawn2;
    private Vector3 Vspawn1, Vspawn2;

    public TextMesh tab1, ttab1, tab2, ttab2, tali, bitwaNR1,bitwaNR2;

  

    void Start ()
    {
        CreateDeck();
        DealingCards();
        ShowTables();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            deck1[0] = 2;
            deck2[0] = 3;

            //deck1[2] = 2;
            //deck2[2] = 16;
            ShowTables();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowTables();
        }

        // Wystaw karty
        if (Input.GetKeyDown(KeyCode.T) && endGame == false)
        {
            layerToDysplay = 0.0f;
            AddCard();
            StartCoroutine(CardAbility());
        }
        //Szybka Gra
        if (Input.GetKey(KeyCode.G) && endGame == false)
        {
            layerToDysplay = 0.0f;
            AddCard();
            StartCoroutine(CardAbility());
        }
    }
     
    void CreateDeck ()
    {
        for(int i = 0; i <56; i++)
        {
            MainDeck[i] = i+1;
        }

    }
    
    void DealingCards ()
    {
        int ran,a=0,b=56;
        int i = 1, z = 0, x = 0;
        bool switcher = true;
        while(i<56)
        {
            ran = Random.Range(a,b);
            if (ran == a) a++;
            if (ran == b) b--;
            
            if (MainDeck[ran] != 0)
            {
                if(switcher)
                {
                    deck1[z] = MainDeck[ran];
                    switcher = false;
                    z++;
                }
                else
                {
                    deck2[x] = MainDeck[ran];
                    switcher = true;
                    x++;
                }

                MainDeck[ran] = 0;
                
            }
            else continue;
            i=z+x;
        }
    }

    void AddCard ()
    {
        int i=0, j=0;

        if(boolCard1) //dokładanie z deck1
        {
            ID_CL1 = deck1[pozDeck1];

            Vspawn1 = new Vector3(spawn1.position.x + layerToDysplay * 1.3f, spawn1.position.y, spawn1.position.z - layerToDysplay);
            Instantiate(card, Vspawn1, Quaternion.Euler(0, 0, 0));

            while(battle1[i] != 0)
            i++;

            battle1[i] = deck1[pozDeck1];
            deck1[pozDeck1] = 0;

            pozDeck1++;
            boolCard1 = false;
        }

        if(boolCard2) // dokladanie z deck2
        {
            ID_CL2 = deck2[pozDeck2];

            Vspawn2 = new Vector3(spawn2.position.x + layerToDysplay * 1.3f, spawn2.position.y, spawn2.position.z - layerToDysplay);
            Instantiate(card2, Vspawn2, Quaternion.Euler(0, 0, 0));

            while(battle2[j] != 0)
            j++;

            battle2[j] = deck2[pozDeck2];
            deck2[pozDeck2] = 0;

            pozDeck2++;
            boolCard2 = false;
        }
    }

    void AddToLoot()
    {
        int i = 0,j=0;

        // karta z deck1 do lupu
        ID_CL1 = deck1[pozDeck1];

        Vspawn1 = new Vector3(spawn1.position.x + layerToDysplay * 1.3f, spawn1.position.y - 1.3f, spawn1.position.z - layerToDysplay);
        Instantiate(card, Vspawn1, Quaternion.Euler(0, 0, -90));

         while (MainDeck[i] != 0)
                i++;

        MainDeck[i] = deck1[pozDeck1];
        deck1[pozDeck1] = 0;

        pozDeck1++;


        i++;
        // karty z Bitwa1 do lupu
        while(battle1[j] != 0)
        {
            MainDeck[i] = battle1[j];
            battle1[j] = 0;
            i++;
            j++;
        }


        // karta z deck2 do lupu
        ID_CL2 = deck2[pozDeck2];

        Vspawn2 = new Vector3(spawn2.position.x + layerToDysplay * 1.3f, spawn2.position.y - 1.3f, spawn2.position.z - layerToDysplay);
        Instantiate(card2, Vspawn2, Quaternion.Euler(0, 0, -90));

        MainDeck[i] = deck2[pozDeck2];
        deck2[pozDeck2] = 0;

        pozDeck2++;

        
        i++;
        while (battle2[j] != 0)
        {
            MainDeck[i] = battle2[j];
            battle2[j] = 0;
            i++;
            j++;
        }

    }

    // Sprawdza czy kary sie skończyly
    void Checking()
    {
        if (deck1[pozDeck1] == 0)
        {
            if (ddeck1[0] == 0)
            {
                Debug.Log("KONIEC deck2(prawa) wygral");
                endGame = true;
            }
            //zamiana d1 <== dd1
            for(int i = 0; i < fillPoz1; i++)
            {
                deck1[i] = ddeck1[i];
                ddeck1[i] = 0;
            }
            pozDeck1 = 0;
            fillPoz1 = 0;
        }

        if (deck2[pozDeck2] == 0)
        {
            if (ddeck2[0] == 0)
            {
                Debug.Log("KONIEC deck1(lewa) wygral");
                endGame = true;
            }
            //zamiana d2 <== dd2
            for (int i = 0; i < fillPoz2; i++)
            {
                deck2[i] = ddeck2[i];
                ddeck2[i] = 0;
            }
            pozDeck2 = 0;
            fillPoz2 = 0;
        }

    }

    IEnumerator CardAbility()
    {
        yield return new WaitForFixedUpdate();

        int i=0;
        bool beige1=false, beige2=false;


        layerToDysplay += 1;

        while (battle1[i] != 0)
        {
            // dla 2
            if (battle1[i] == 2 || battle1[i] == 16 || battle1[i] == 30 || battle1[i] == 44)
            {
                beige1 = false;
                boolCard1 = true;
                AddCard();
            }

            // dla Jopka
            if (battle1[i] == 11 || battle1[i] == 25 || battle1[i] == 39 || battle1[i] == 53)
            {
                beige1 = false;
                boolCard2 = true;
                AddCard();
            }
            
            //dla Dama Wino
            if(battle1[i]== 54)
            {
                beige1 = true;
                RIGHT_win = true;
                LEFT_win = false;
            }

            i++;
        }

        i = 0;
        yield return new WaitForFixedUpdate();
        while (battle2[i] != 0)
        {
            // dla 2
            if (battle2[i] == 2 || battle2[i] == 16 || battle2[i] == 30 || battle2[i] == 44)
            {
                beige2 = false;
                boolCard2 = true;
                AddCard();
            }

            // dla Jopka
            if (battle2[i] == 11 || battle2[i] == 25 || battle2[i] == 39 || battle2[i] == 53)
            {
                beige1 = false;
                boolCard1 = true;
                AddCard();
            }

            //dla Dama Wino
            if (battle2[i] == 54)
            {
                beige2 = true;
                LEFT_win = true;
                RIGHT_win = false;
            }

            i++;
        }

        //Brak zdolnosci
        if (beige1 == false && beige2 == false)
        {
            ability = false;
        }
        else ability = true;

        Checking();
        Combat();
        ShowTables();

    }
    
    void Combat ()
    {
        int maxD1=0, maxD2=0; //maxymalne wartosci kart na polu bitwy 

        int pozBattle1 = 0, pozBattle2 = 0;

        while (battle1[pozBattle1] != 0)
        {
            ID1 = battle1[pozBattle1] % 14;
            if (ID1 == 0) ID1 = 14;

            if (ID1 > maxD1) maxD1 = ID1;
            pozBattle1++;
        }

        while (battle2[pozBattle2] != 0)
        {
            ID2 = battle2[pozBattle2] % 14;
            if (ID2 == 0) ID2 = 14;

            if (ID2 > maxD2) maxD2 = ID2;
            pozBattle2++;
        }

        pozBattle1 = 0;
        pozBattle2 = 0;

        if(!ability)
        {
            // dla 1
            if (maxD1 == 1 && maxD2 >= 10) maxD1 = 15;
            if (maxD2 == 1 && maxD2 >= 10) maxD2 = 15;

            if (maxD1 > maxD2) LEFT_win = true;
            if (maxD1 < maxD2) RIGHT_win = true;
        }



        // LEWA wygrywa
        if (LEFT_win == true && RIGHT_win == false)  
        {   
            Debug.Log("Lewa wygrala");

            // dodaj swoje
            while (battle1[pozBattle1] != 0)
            {
                ddeck1[fillPoz1] = battle1[pozBattle1];
                fillPoz1++;

                battle1[pozBattle1] = 0; // zeruj swoje
                pozBattle1++;
            }

            // dodaj przeciwnika
            while (battle2[pozBattle2] != 0)
            {
                ddeck1[fillPoz1] = battle2[pozBattle2];
                fillPoz1++;

                battle2[pozBattle2] = 0; // zeruj przeciwnika
                pozBattle2++;
            }

            // dodaj lupy
            int i=0;
            while(MainDeck[i] != 0)
            {
                ddeck1[fillPoz1] = MainDeck[i];
                fillPoz1++;
                MainDeck[i] = 0;
                i++;
            }

        }
        // PRAWA wygrywa
        else if (LEFT_win == false && RIGHT_win == true)  
        {
            Debug.Log("Prawa wygrala");

            // dodaj swoje
            while(battle2[pozBattle2] != 0)
            {
                ddeck2[fillPoz2] = battle2[pozBattle2];
                fillPoz2++;

                battle2[pozBattle2] = 0; // zeruj swoje
                pozBattle2++;
            }

            // dodaj przeciwnika
            while (battle1[pozBattle1] != 0)
            {
                ddeck2[fillPoz2] = battle1[pozBattle1];
                fillPoz2++;

                battle1[pozBattle1] = 0; // zeruj przeciwika
                pozBattle1++;
            }

            // dodaj lupy
            int i = 0;
            while (MainDeck[i] != 0)
            {
                ddeck2[fillPoz2] = MainDeck[i];
                fillPoz2++;
                MainDeck[i] = 0;
                i++;
            }


        }
        // WOJNA
        else
        {   
            Debug.Log("WOJNA!!!");
            StartCoroutine(War());
        }

        boolCard1 = true;
        boolCard2 = true;
        LEFT_win = false;
        RIGHT_win = false;
    }

    IEnumerator War()
    {
        Checking();
        yield return new WaitForFixedUpdate();

        layerToDysplay += 0.3f;
        AddToLoot();
        Checking();

        yield return new WaitForFixedUpdate();
        layerToDysplay += 0.3f;
        boolCard1 = true;
        boolCard2 = true;

        AddCard();

        Combat();

    }

    //tego nie będzie w finalnej wersji
    //jest to tylko ułatwienie wyświetlające co jest w tablicach
    public void ShowTables ()
    {
        tab1.text = null;
        tab2.text = null;
        ttab1.text = null;
        ttab2.text = null;
        tali.text = null;
        bitwaNR1.text = null;
        bitwaNR2.text = null;

        for (int i =0; i<56; i++)
        {
            tab1.text = tab1.text + " " + deck1[i];
            ttab1.text = ttab1.text + " " + ddeck1[i];

            tab2.text = tab2.text + " " + deck2[i];
            ttab2.text = ttab2.text + " " + ddeck2[i];

            tali.text = tali.text + " " + MainDeck[i];

            if(i < 16)
            {
                bitwaNR1.text = bitwaNR1.text + " " + battle1[i];
                bitwaNR2.text = bitwaNR2.text + " " + battle2[i];
            }

        }
    }
}
