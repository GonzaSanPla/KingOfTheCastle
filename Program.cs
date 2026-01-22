// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using EspacioJugador;
using EspacioEnemigo;
using EspacioPersonajes;
using EspacioRoot;

/*
Colores y su signficado:
    Texto       - White
    Vida        - Green
    Ataque      - Red
    Evasion     - DarkBlue
    Defensa     - Gray
    Hada        - Magenta
    Centauro    - DarkCyan
    Ogro        - DarkGreen
    Pociones    - DarkRed
    Concentrar  - Blue
    Título      - DarkYellow
    Castillo    - DarkGray
*/

Jugador jugador;
Enemigo enemigo;

Inicio();
do
{
    jugador = FabricaDeJugador();
    // jugador.MostrarEstadisticas();
    while (jugador.EstaVivo() && !jugador.Ganar())
    {
        MostrarOleada(jugador);
        enemigo = await FabricaDeEnemigo();
        enemigo.PresentarEnemigo();
        jugador = Pelear(jugador, enemigo);
        if (jugador.EstaVivo())
        {
            jugador.RecibirRecompensa(enemigo.Raza);
        }
    }
    Finalizar(jugador);
} while (SegirJugando());

static void Inicio()
{
    Mostrar mostrar= new Mostrar();
    mostrar.Titulo();
    Pausa();
    Console.WriteLine("     Erase una vez un bello castillo.EL cual su rey, ya seindo muy anciano, proclamo que su sucesor sera el luchador mas fuerte. Pordras tu quedarte con la corona y conevertirte en el Rey del catillo?");
    mostrar.Castillo();
    Pausa();
    mostrar.ComoJuagar();
}

static Jugador FabricaDeJugador()
{
    string nombreJ;
    int raza;
    Console.ForegroundColor = ConsoleColor.White;
    do
    {
        Console.WriteLine("\nIngresa tu nombre(Maximo 12 caracteres):");
        nombreJ = Console.ReadLine();
        if(nombreJ.StartsWith(' '))
        {
            Console.WriteLine("\nSu nombre no puede inciar por espacio");
        }
    } while (nombreJ == "" || nombreJ.StartsWith(' ')||nombreJ.Length>12);
    raza = ElegirRaza();

    Jugador pj = new Jugador(nombreJ, raza);
    return pj;
}
static void MostrarOleada(Jugador Jugador)
{
    Mostrar most = new Mostrar();
    most.ProximaOleada();
    Jugador.MostrarOleada();
}
static async Task<Enemigo> FabricaDeEnemigo()
{
    Random rand = new Random();
    string nombre = await ObtenerRoot();
    Enemigo pj = new Enemigo(nombre, rand.Next(1, 4));//Genera un numero al azar entre 1 y 3 
    return pj;
}

static async Task<string> ObtenerRoot()
{
    var url = "https://randomuser.me/api/";
    try
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Root infoBas = JsonSerializer.Deserialize<Root>(responseBody);
        return infoBas.Results[0].Name.First;
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("Problemas de acceso a la API");
        Console.WriteLine("Message :{0} ", e.Message);
        
        return null;
    }
}

static int ElegirRaza()
{
    bool condiciones;
    string strRaza;
    int raza;
    Mostrar most = new Mostrar();

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("\nHada: Fragil pero gracias su diminuta altura y alta velocidad puede evitar muchos golpes");
    most.Hada();
    Pausa();
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("\nCentauro: Logra aguantar unos cuantes golpes y con cierta agilidad se mueve evitando algunos ataques");
    most.Centauro();
    Pausa();
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("\nOgro: Debido a su gran tamanio se le complica movese velozmente lo cual queda extramadamente vulnerable a los golpes enemigos. Pero lo compensa con su gran capacidad de recibir golpes");
    most.Ogro();
    Pausa();
    do
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nElija que quiere ser:");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n  1-Hada");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("\n  2-Centauro");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("\n  3-Ogro");
        Console.ForegroundColor = ConsoleColor.White;
        strRaza = Console.ReadLine();
        if (int.TryParse(strRaza, out raza))        // Para controlar si es un numero y sies un numero valido
        {
            if (raza < 4 && raza > 0)
            {
                condiciones = true;
            }
            else
            {
                condiciones = false;
            }
        }
        else
        {
            condiciones = false;
        }

    } while (!condiciones);

    return raza;
}

static Jugador Pelear(Jugador jugador, Enemigo enemigo)
{
    Mostrar  mostrar = new Mostrar();
    int opcion;
    do
    {
        mostrar.Turno();
        jugador.MostrarVida();
        enemigo.MostrarVida();
        opcion = ElegirOpcion(jugador);          // Regresa un string con 1 para atacar, 2 para defender y 3 para usar pocion
        mostrar.Movimientos();
        if (enemigo.Raza >= jugador.Raza)          //Para elegir quien ataca primero me baso en su raza siendo Hada>Centauro>Ogro y si son de la misma raza va primero el jugador
        {
            Console.WriteLine("\n");
            //Este bloque es para realizar la accion del jugador
            switch (opcion)
            {
                case 1:
                    enemigo.PerderVida(Atacar(jugador, enemigo));
                    if (jugador.EstaContrado())
                    {
                        jugador.Desoncentar();
                    }
                    break;
                case 2:
                    jugador.Concentar();
                    break;
                case 3:
                    jugador.UtilizarPocion();
                    if (jugador.EstaContrado())
                    {
                        jugador.Desoncentar();
                    }
                    break;
            }
            if (enemigo.EstaVivo())
            {
                Console.WriteLine("\n");
                //Este bloque es para realizar la accion del enemigo 
                switch (enemigo.ElegirMovimiento())
                {
                case 1:
                    jugador.PerderVida(Atacar(enemigo, jugador));
                    if (enemigo.EstaContrado())
                    {
                        enemigo.Desoncentar();
                    }
                    break;
                case 2:
                    enemigo.Concentar();
                    break;    
                }
            }
        
        }else
        {
            Console.WriteLine("\n");
            //Este bloque es para realizar la accion del enemigo 
            switch (enemigo.ElegirMovimiento())
            {
                case 1:
                    jugador.PerderVida(Atacar(enemigo, jugador));
                    if (enemigo.EstaContrado())
                    {
                        enemigo.Desoncentar();
                    }
                    break;
                case 2:
                    enemigo.Concentar();
                    break;    
            }
            if (jugador.EstaVivo())
            {
                Console.WriteLine("\n");
                //Este bloque es para realizar la accion del jugador
                switch (opcion)
                {
                case 1:
                    enemigo.PerderVida(Atacar(jugador, enemigo));
                    if (jugador.EstaContrado())
                    {
                        jugador.Desoncentar();
                    }
                    break;
                case 2:
                    jugador.Concentar();
                    break;
                case 3:
                    jugador.UtilizarPocion();
                    if (jugador.EstaContrado())
                    {
                        jugador.Desoncentar();
                    }
                    break;
                }
            }
        }
    } while (jugador.EstaVivo() && enemigo.EstaVivo());
    if (jugador.EstaVivo())
    {
        Console.WriteLine("Has derrotado a " + enemigo.Nombre);
        jugador.AumentarOleada();
    }

    return jugador;
}

static int ElegirOpcion(Personajes jugador)        // Regresa un string con 1 para atacar, 2 para defender y 3 para usar pocion
{
    int opcion;
    string strOpcion;
    do
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nElija su siguiente movimiento:");
        Console.ForegroundColor = ConsoleColor.Green;   //Exepcion de color sino es demasiado rojo
        Console.WriteLine(" 1-Artacar");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(" 2-Concentrar");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(" 3-Tomar pocion(Pociones restantes:" + jugador.Pociones + ")");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(" 4-Mostrar estdísticas");
        strOpcion = Console.ReadLine();
        if(strOpcion == "4")
        {
            jugador.MostrarEstadisticas();
        }
    } while (strOpcion != "1" && strOpcion != "2" && strOpcion != "3");
    int.TryParse(strOpcion, out opcion);
    return opcion;
}

static int Atacar(Personajes Atacante, Personajes Defensor)
{
    Random rand = new Random();
    int danioRealizado=0, varConcentracion;
    if (!Defensor.LoEsquiva(rand.Next(1, 101)))                    //Para que sean numeros entre el 1 y el 100 
    {
        varConcentracion = VariableDeConcentracionParaCalcularDanio(Atacante, Defensor);
        danioRealizado = Atacante.Ataque * 15 * varConcentracion / Defensor.Defensa;     //15 es una variable de balanceo
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(Atacante.Nombre + " ha realizado " + danioRealizado + " puntos de daño a " + Defensor.Nombre);
    }
    return danioRealizado;
}

static int VariableDeConcentracionParaCalcularDanio(Personajes Atacante, Personajes Defensor)       //Retorna 2 si estan neutro,4 si solo el atacante y 1 si solo el defensor
{

    if (Atacante.EstaContrado() == Defensor.EstaContrado())    //Si los dos estan concentrados queda en un ataque normal porque se neutraliza
    {
        return 2;
    }
    else
    {
        if (Atacante.EstaContrado())                         //Si no esta concentrado el atacante entonces tiene que estar concentrado el defensor en este caso 
        {
            return 4;
        }
        else
        {
            return 1;
        }
    }

}

// static (Personajes,Personajes) RealizarMovimiento(Personajes Atacante,Personajes Defensor, int opcion)
// {

//     switch (opcion)
//     {
//         case 1:
//             Defensor = Atacar(Atacante, Defensor);
//             if (Atacante.EstaContrado())
//             {
//                 Atacante.Desoncentar();
//             }
//             break;
//         case 2:
//             Atacante.Concentar();
//             break;
//         case 3:
//             Atacante.UtilizarPocion();
//             if (Atacante.EstaContrado())
//             {
//                 Atacante.Desoncentar();
//             }
//             break;
//     }
//     return (Atacante,Defensor);
// }

static void Finalizar(Jugador Jugador)
{
    Mostrar most = new Mostrar();

    if (Jugador.Ganar())
    {
        most.Corona();
        most.Felicidades();
        Console.WriteLine("Losgraste tu objetivo y te convertiste en el/la Rey del castillo");
    }
    else
    {
    
        most.Fallaste();
        Jugador.MostrarOleadaFinal();
    }
    MostrarRanking(HistorialJugadores(Jugador));
}

static List<Jugador> HistorialJugadores(Jugador Jugador)
{
    string listaJugadoresSerializado;
    string NombreDelArchivo = "Historial del jugadores";
    var helper = new HelperDeJson();

    if(!File.Exists(NombreDelArchivo))
    {
        List<Jugador> listaJugadores= new List<Jugador>();
        listaJugadores.Add(Jugador);

        listaJugadoresSerializado=JsonSerializer.Serialize(listaJugadores);
        helper.GuardarArchivoTexto(NombreDelArchivo,listaJugadoresSerializado);

        return listaJugadores;
    }else
    {

        listaJugadoresSerializado=helper.AbrirArchivoTexto(NombreDelArchivo);
        var listaJugadores= JsonSerializer.Deserialize<List<Jugador>>(listaJugadoresSerializado);

        listaJugadores.Add(Jugador);
        listaJugadoresSerializado=JsonSerializer.Serialize(listaJugadores);
        helper.GuardarArchivoTexto(NombreDelArchivo,listaJugadoresSerializado);

        return listaJugadores;
    }

    
}

static void MostrarRanking(List<Jugador> listaJugadores)
{ 
    int oleada=10;
    int ranking=1;

    Console.ForegroundColor=ConsoleColor.White;
    Console.WriteLine("\nRanking   Oleada   Nombre");
    while(oleada>0)
    {
        foreach(Jugador jugador in listaJugadores)
        {
            if(oleada==jugador.Oleada)
            {
                if(ranking<10) //diferencio si es menor de 10 para que quede alineado 
                {
                    if(oleada==10)
                    {
                        Console.ForegroundColor=ConsoleColor.Yellow;
                        Console.WriteLine(ranking+"         "+oleada+"       "+jugador.Nombre);
                    }else
                    {
                        Console.ForegroundColor=ConsoleColor.White;
                        Console.WriteLine(ranking+"         "+oleada+"        "+jugador.Nombre);
                    }
                }
                else
                {
                    if(oleada==10)
                    {
                        Console.ForegroundColor=ConsoleColor.Yellow;
                        Console.WriteLine(ranking+"        "+oleada+"         "+jugador.Nombre);
                    }else
                    {
                        Console.ForegroundColor=ConsoleColor.White;
                        Console.WriteLine(ranking+"        "+oleada+"        "+jugador.Nombre);
                    }
                }
                ranking++;
            }
        }
        oleada--;
    }
}
static void Pausa()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Presione cualquier tecla para continuar");
    Console.ReadKey(true);
}

static bool SegirJugando()
{
    string respuesta;
    do
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nDesea jugar de nuevo?");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("0-Si");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("1-No");
        Console.ForegroundColor = ConsoleColor.White;
        respuesta = Console.ReadLine();
    } while (respuesta != "1" && respuesta != "0");
    if (respuesta == "0")
    {
        return true;
    }
    else
    {
        return false;
    }
}