using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenager : MonoBehaviour {

    // nowa zmiana

    private int[] talia = new int [56];
    private int[] deck1 = new int[56];
    private int[] deck2 = new int[56];

    private int[] ddeck1 = new int[56];
    private int[] ddeck2 = new int[56];

    private int[] bitwa1 = new int[16];
    private int[] bitwa2 = new int[16];

    public int ID_CL1, ID_CL2;
    private int ID1, ID2;
    private int pozycjaDeck1=0,pozycjaDeck2=0, pozUzepelniajaca1, pozUzepelniajaca2;
    private float warstwy=0.0f;
    public bool koniec = false;
    private bool boolKarta1= true, boolKarta2 = true, LEWAwyg=false, PRAWAwyg=false, zdolnosc=false;

    public GameObject card;
    public GameObject card2;
    public Transform spawn1 , spawn2;
    private Vector3 Vspawn1, Vspawn2;

    public TextMesh tab1, ttab1, tab2, ttab2, tali, bitwaNR1,bitwaNR2;

  

    void Start ()
    {
        TworzenieKart();
        Rozdawanie();
        PokaTablice();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            deck1[0] = 2;
            deck2[0] = 3;

            //deck1[2] = 2;
            //deck2[2] = 16;
            PokaTablice();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            PokaTablice();
        }

        // Wystaw karty
        if (Input.GetKeyDown(KeyCode.T) && koniec == false)
        {
            warstwy = 0.0f;
            DodajKarte();
            StartCoroutine(ZdolnosciKart());
        }
        //Szybka Gra
        if (Input.GetKey(KeyCode.G) && koniec == false)
        {
            warstwy = 0.0f;
            DodajKarte();
            StartCoroutine(ZdolnosciKart());
        }
    }
     
    void TworzenieKart ()
    {
        for(int i = 0; i <56; i++)
        {
            talia[i] = i+1;
        }

    }
    
    void Rozdawanie ()
    {
        int ran,a=0,b=56;
        int i = 1, z = 0, x = 0;
        bool zmiana = true;
        while(i<56)
        {
            ran = Random.Range(a,b);
            if (ran == a) a++;
            if (ran == b) b--;
            
            if (talia[ran] != 0)
            {
                if(zmiana)
                {
                    deck1[z] = talia[ran];
                    zmiana = false;
                    z++;
                }
                else
                {
                    deck2[x] = talia[ran];
                    zmiana = true;
                    x++;
                }

                talia[ran] = 0;
                
            }
            else continue;
            i=z+x;
        }
    }

    void DodajKarte ()
    {
        int i=0, j=0;

        if(boolKarta1) //dokładanie z deck1
        {
            ID_CL1 = deck1[pozycjaDeck1];

            Vspawn1 = new Vector3(spawn1.position.x + warstwy * 1.3f, spawn1.position.y, spawn1.position.z - warstwy);
            Instantiate(card, Vspawn1, Quaternion.Euler(0, 0, 0));

            while(bitwa1[i] != 0)
            i++;

            bitwa1[i] = deck1[pozycjaDeck1];
            deck1[pozycjaDeck1] = 0;

            pozycjaDeck1++;
            boolKarta1 = false;
        }

        if(boolKarta2) // dokladanie z deck2
        {
            ID_CL2 = deck2[pozycjaDeck2];

            Vspawn2 = new Vector3(spawn2.position.x + warstwy * 1.3f, spawn2.position.y, spawn2.position.z - warstwy);
            Instantiate(card2, Vspawn2, Quaternion.Euler(0, 0, 0));

            while(bitwa2[j] != 0)
            j++;

            bitwa2[j] = deck2[pozycjaDeck2];
            deck2[pozycjaDeck2] = 0;

            pozycjaDeck2++;
            boolKarta2 = false;
        }
    }

    void DodajDoLupu()
    {
        int i = 0,j=0;

        // karta z deck1 do lupu
        ID_CL1 = deck1[pozycjaDeck1];

        Vspawn1 = new Vector3(spawn1.position.x + warstwy * 1.3f, spawn1.position.y - 1.3f, spawn1.position.z - warstwy);
        Instantiate(card, Vspawn1, Quaternion.Euler(0, 0, -90));

         while (talia[i] != 0)
                i++;

        talia[i] = deck1[pozycjaDeck1];
        deck1[pozycjaDeck1] = 0;

        pozycjaDeck1++;


        i++;
        // karty z Bitwa1 do lupu
        while(bitwa1[j] != 0)
        {
            talia[i] = bitwa1[j];
            bitwa1[j] = 0;
            i++;
            j++;
        }


        // karta z deck2 do 
        ID_CL2 = deck2[pozycjaDeck2];

        Vspawn2 = new Vector3(spawn2.position.x + warstwy * 1.3f, spawn2.position.y - 1.3f, spawn2.position.z - warstwy);
        Instantiate(card2, Vspawn2, Quaternion.Euler(0, 0, -90));

        talia[i] = deck2[pozycjaDeck2];
        deck2[pozycjaDeck2] = 0;

        pozycjaDeck2++;

        
        i++;
        while (bitwa2[j] != 0)
        {
            talia[i] = bitwa2[j];
            bitwa2[j] = 0;
            i++;
            j++;
        }

    }

    // Sprawdza czy kary sie skończyly
    void Sprawdzanie()
    {
        if (deck1[pozycjaDeck1] == 0)
        {
            if (ddeck1[0] == 0)
            {
                Debug.Log("KONIEC deck2(prawa) wygral");
                koniec = true;
            }
            //zamiana d1 <== dd1
            for(int i = 0; i < pozUzepelniajaca1; i++)
            {
                deck1[i] = ddeck1[i];
                ddeck1[i] = 0;
            }
            pozycjaDeck1 = 0;
            pozUzepelniajaca1 = 0;
        }

        if (deck2[pozycjaDeck2] == 0)
        {
            if (ddeck2[0] == 0)
            {
                Debug.Log("KONIEC deck1(lewa) wygral");
                koniec = true;
            }
            //zamiana d2 <== dd2
            for (int i = 0; i < pozUzepelniajaca2; i++)
            {
                deck2[i] = ddeck2[i];
                ddeck2[i] = 0;
            }
            pozycjaDeck2 = 0;
            pozUzepelniajaca2 = 0;
        }

    }

    IEnumerator ZdolnosciKart()
    {
        yield return new WaitForFixedUpdate();

        int i=0;
        bool bez1=false, bez2=false;


        warstwy += 1;

        while (bitwa1[i] != 0)
        {
            // dla 2
            if (bitwa1[i] == 2 || bitwa1[i] == 16 || bitwa1[i] == 30 || bitwa1[i] == 44)
            {
                bez1 = false;
                boolKarta1 = true;
                DodajKarte();
            }

            // dla Jopka
            if (bitwa1[i] == 11 || bitwa1[i] == 25 || bitwa1[i] == 39 || bitwa1[i] == 53)
            {
                bez1 = false;
                boolKarta2 = true;
                DodajKarte();
            }
            
            //dla Dama Wino
            if(bitwa1[i]== 54)
            {
                bez1 = true;
                PRAWAwyg = true;
                LEWAwyg = false;
            }

            i++;
        }

        i = 0;
        yield return new WaitForFixedUpdate();
        while (bitwa2[i] != 0)
        {


            // dla 2
            if (bitwa2[i] == 2 || bitwa2[i] == 16 || bitwa2[i] == 30 || bitwa2[i] == 44)
            {
                bez2 = false;
                boolKarta2 = true;
                DodajKarte();
            }

            // dla Jopka
            if (bitwa2[i] == 11 || bitwa2[i] == 25 || bitwa2[i] == 39 || bitwa2[i] == 53)
            {
                bez1 = false;
                boolKarta1 = true;
                DodajKarte();
            }

            //dla Dama Wino
            if (bitwa2[i] == 54)
            {
                bez2 = true;
                LEWAwyg = true;
                PRAWAwyg = false;
            }

            i++;
        }

        //Brak zdolnosci
        if (bez1 == false && bez2 == false)
        {
            zdolnosc = false;
        }
        else zdolnosc = true;

        Sprawdzanie();
        Walka();
        PokaTablice();

    }
    
    void Walka ()
    {
        int maxD1=0, maxD2=0; //maxymalne wartosci kart na polu bitwy 

        int pozBitwa1 = 0, pozBitwa2 = 0;

        while (bitwa1[pozBitwa1] != 0)
        {
            ID1 = bitwa1[pozBitwa1] % 14;
            if (ID1 == 0) ID1 = 14;

            if (ID1 > maxD1) maxD1 = ID1;
            pozBitwa1++;
        }

        while (bitwa2[pozBitwa2] != 0)
        {
            ID2 = bitwa2[pozBitwa2] % 14;
            if (ID2 == 0) ID2 = 14;

            if (ID2 > maxD2) maxD2 = ID2;
            pozBitwa2++;
        }

        pozBitwa1 = 0;
        pozBitwa2 = 0;

        if(!zdolnosc)
        {
            // dla 1
            if (maxD1 == 1 && maxD2 >= 10) maxD1 = 15;
            if (maxD2 == 1 && maxD2 >= 10) maxD2 = 15;

            if (maxD1 > maxD2) LEWAwyg = true;
            if (maxD1 < maxD2) PRAWAwyg = true;
        }



        // LEWA wygrywa
        if (LEWAwyg == true && PRAWAwyg == false)  
        {   
            Debug.Log("Lewa wygrala");

            // dodaj swoje
            while (bitwa1[pozBitwa1] != 0)
            {
                ddeck1[pozUzepelniajaca1] = bitwa1[pozBitwa1];
                pozUzepelniajaca1++;

                bitwa1[pozBitwa1] = 0; // zeruj swoje
                pozBitwa1++;
            }

            // dodaj przeciwnika
            while (bitwa2[pozBitwa2] != 0)
            {
                ddeck1[pozUzepelniajaca1] = bitwa2[pozBitwa2];
                pozUzepelniajaca1++;

                bitwa2[pozBitwa2] = 0; // zeruj przeciwnika
                pozBitwa2++;
            }

            // dodaj lupy
            int i=0;
            while(talia[i] != 0)
            {
                ddeck1[pozUzepelniajaca1] = talia[i];
                pozUzepelniajaca1++;
                talia[i] = 0;
                i++;
            }

        }
        // PRAWA wygrywa
        else if (LEWAwyg == false && PRAWAwyg == true)  
        {
            Debug.Log("Prawa wygrala");

            // dodaj swoje
            while(bitwa2[pozBitwa2] != 0)
            {
                ddeck2[pozUzepelniajaca2] = bitwa2[pozBitwa2];
                pozUzepelniajaca2++;

                bitwa2[pozBitwa2] = 0; // zeruj swoje
                pozBitwa2++;
            }

            // dodaj przeciwnika
            while (bitwa1[pozBitwa1] != 0)
            {
                ddeck2[pozUzepelniajaca2] = bitwa1[pozBitwa1];
                pozUzepelniajaca2++;

                bitwa1[pozBitwa1] = 0; // zeruj przeciwika
                pozBitwa1++;
            }

            // dodaj lupy
            int i = 0;
            while (talia[i] != 0)
            {
                ddeck2[pozUzepelniajaca2] = talia[i];
                pozUzepelniajaca2++;
                talia[i] = 0;
                i++;
            }


        }
        // WOJNA
        else
        {   
            Debug.Log("WOJNA!!!");
            StartCoroutine(Wojna());
        }

        boolKarta1 = true;
        boolKarta2 = true;
        LEWAwyg = false;
        PRAWAwyg = false;
    }

    IEnumerator Wojna()
    {
        Sprawdzanie();
        yield return new WaitForFixedUpdate();

        warstwy += 0.3f;
        DodajDoLupu();
        Sprawdzanie();

        yield return new WaitForFixedUpdate();
        warstwy += 0.3f;
        boolKarta1 = true;
        boolKarta2 = true;

        DodajKarte();

        Walka();

    }

    public void PokaTablice ()
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

            tali.text = tali.text + " " + talia[i];

            if(i < 16)
            {
                bitwaNR1.text = bitwaNR1.text + " " + bitwa1[i];
                bitwaNR2.text = bitwaNR2.text + " " + bitwa2[i];
            }

        }
    }
}
