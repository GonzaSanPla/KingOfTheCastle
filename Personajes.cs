using System.Drawing;
using System.Text.Json;


namespace EspacioPersonajes;


public class Personajes
{

    private string nombre;

    private float vidaActual;
    private float vidaMaxima;
    private int ataque;
    private int evasion;
    private int defensa;
    private int pociones;
    private int raza; // 1- Hada 2-Centauro 3- Ogro
    private bool concentrado1;
    // private int movimiento;//Esta es una varible unicamente para que el enemigo elija movimiento 
    // private int oleada;

    public Personajes()
    {

    }
    public Personajes(string NombreIngresado, int numRaza)
    {
        Nombre= NombreIngresado; 
        Pociones=3;
        Concentrado=false;
        // Movimiento=1;
        // Oleada=1;
        switch(numRaza)
        {
            case 1:                    
                Raza= numRaza;
                VidaMaxima=75;
                VidaActual=75;
                Ataque=25;
                Evasion=50;
                Defensa=25;
                break;
            case 2:
                Raza=numRaza;
                VidaMaxima=100;
                VidaActual=100;
                Ataque=25;
                Evasion=25;
                Defensa=30;
                break;
            case 3:
                Raza=numRaza;
                VidaMaxima=125;
                VidaActual=125;
                Ataque=25;
                Evasion=0;
                Defensa=35;
                break;
        }

    }


    public void MostrarEstadisticas()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nEstadisticas:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("   Vida actual:"+VidaActual);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("   Ataque:"+Ataque);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("   Evasion:"+Evasion);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("   Defensa:"+Defensa);
        switch(Raza)
        {
            case 1:
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("   Raza: Hada");
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("   Raza: Centauro");
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("   Raza: Ogro");
                break;
        }
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("   Pociones restantes:"+Pociones);
    }
   
    public bool EstaVivo()// Si esta vivo retorna true
    {
        if(VidaActual!=0)
        {
            return true;
        }else
        {
            return false;
        }
    }

    public void PerderVida(int danioRecibido)
    {
        if(danioRecibido>VidaActual)
        {
            VidaActual=0;
        }else
        {
            VidaActual=VidaActual-danioRecibido;
        }
    }
    public void MostrarVida()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nVida de "+Nombre+": "+VidaActual+"/"+VidaMaxima);

    }
   
    public void Concentar()
    {
        Concentrado=true;
        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine(Nombre+" se ha concentrado");
    }
    public void Desoncentar()
    {
        Concentrado=false;
        Console.ForegroundColor=ConsoleColor.Blue;
        Console.WriteLine(Nombre+" se ha desconcentrado");
    }
    public bool EstaContrado()
    {
        if(Concentrado)
        {
            return true;
        }else
        {
            return false;
        }
    }
    public bool LoEsquiva(int numAtaque)
    {
        if(numAtaque>Evasion)
        {
            return false;
        }else
        {
            Console.ForegroundColor=ConsoleColor.DarkBlue;
            Console.WriteLine(Nombre+" ha evitado el ataque");
            return true;
        }
    }

      public void UtilizarPocion()
    {
        if(Pociones!=0)
        {
            if((VidaActual+(VidaMaxima/2))>VidaMaxima)
            {
                VidaActual=VidaMaxima;
            }else
            {
                VidaActual=VidaActual+(VidaMaxima/2);
            }
            
            Pociones--;
            Console.ForegroundColor=ConsoleColor.DarkRed;
            Console.WriteLine("\n"+Nombre+" ha utilizado una pocion, quedan "+Pociones+" pociones");
        }else
        {            
            Console.ForegroundColor=ConsoleColor.DarkRed;
            Console.WriteLine("A "+Nombre+" no le quedan mas pociones");
        }

    }
    

    public bool Concentrado { get => Concentrado1; set => Concentrado1 = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public float VidaActual { get => vidaActual; set => vidaActual = value; }
    public float VidaMaxima { get => vidaMaxima; set => vidaMaxima = value; }
    public int Ataque { get => ataque; set => ataque = value; }
    public int Evasion { get => evasion; set => evasion = value; }
    public int Defensa { get => defensa; set => defensa = value; }
    public int Pociones { get => pociones; set => pociones = value; }
    public int Raza { get => raza; set => raza = value; }
    public bool Concentrado1 { get => concentrado1; set => concentrado1 = value; }
    // protected int Movimiento { get => movimiento; set => movimiento = value; }
    // protected int Oleada { get => oleada; set => oleada = value; }
} 

