//arreglar lo de la posicion inicial de los jugadores 
// arreglar que cuando en las opciones se ponga otra cosa que no sean llos numeros que de error

using System.Diagnostics;
namespace Project
{
    public class MazeGenerator
    {
        private string NameGame; 
        private string[,] mapa;
        private int Rows;
        private int Cols;
        private Random random;
        private Jugador jugador1;
        private Jugador jugador2;
        private (int Row, int Col) metaJugador1 = (33, 1); // Meta del Jugador 1
        private (int Row, int Col) metaJugador2 = (33, 33); //Meta del jugador 2
        

        public MazeGenerator(int rows, int cols)
        {
            NameGame = "Juego de Diamantes";
            Rows = rows;
            Cols = cols;
            mapa = new string[Rows, Cols];
            random = new Random();
            jugador1 = new Jugador.JugadorBase(1,1,1);
            jugador2 = new Jugador.JugadorBase(2,1,33);
            InitializeMaze();
        }

        private void InitializeMaze()
        {
            Console.WriteLine($"Bienvenidos al {NameGame}");
            Console.WriteLine("Por favor, escriba el numero del personaje que desea para el jugador 1:");
            Console.WriteLine("1. Personaje 1");
            Console.WriteLine("2. Personaje 2");
            Console.WriteLine("3. Personaje 3");
            Console.WriteLine("4. Personaje 4");
            Console.WriteLine("5. Personaje 5");

            int opcion1;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out opcion1) && opcion1 >= 1 && opcion1 <= 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opción inválida. Por favor, ingrese un número entre 1 y 5.");
                }
            }
            switch (opcion1)
            {
                case 1:
                    jugador1 = new Jugador.Personaje1(1, 1, 1); // Posición inicial del jugador 1
                    break;
                case 2:
                    jugador1 = new Jugador.Personaje2(1, 1, 1); // Posición inicial del jugador 1
                    break;
                case 3:
                    jugador1 = new Jugador.Personaje3(1, 1, 1); // Posición inicial del jugador 1
                    break;
                case 4:
                    jugador1 = new Jugador.Personaje4(1, 1, 1); // Posición inicial del jugador 1
                    break;
                case 5:
                    jugador1 = new Jugador.Personaje5(1, 1, 1); // Posición inicial del jugador 1
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
            

            Console.WriteLine("Por favor, escriba el numero del personaje que desea para el jugador 2:");
            Console.WriteLine("1. Personaje 1");
            Console.WriteLine("2. Personaje 2");
            Console.WriteLine("3. Personaje 3");
            Console.WriteLine("4. Personaje 4");
            Console.WriteLine("5. Personaje 5");

            int opcion2;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out opcion2) && opcion2 >= 1 && opcion2 <= 5)
                {
                    break;
                }
                 else
                {
                    Console.WriteLine("Opción inválida. Por favor, ingrese un número entre 1 y 5.");
                }
            }
            switch (opcion2)
            {
                case 1:
                    jugador2 = new Jugador.Personaje1(2, 1, 33); // Posición inicial del jugador 2
                    break;
                case 2:
                    jugador2 = new Jugador.Personaje2(2, 1, 33); // Posición inicial del jugador 2
                    break;
                case 3:
                    jugador2 = new Jugador.Personaje3(2, 1, 33); // Posición inicial del jugador 2
                    break;
                case 4:
                    jugador2 = new Jugador.Personaje4(2, 1, 33); // Posición inicial del jugador 2
                    break;
                case 5:
                    jugador2 = new Jugador.Personaje5(2, 1, 33); // Posición inicial del jugador 2
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
            

            Console.WriteLine("Las reglas del juego son las siguientes:");
            Console.WriteLine("1. Cada jugador debe recoger 10 diamantes y llevarlos hacia la meta que le corresponde. El primer jugador que lo logre, gana");
            Console.WriteLine("2. Evite las trampas ya que estas hacen que pierda 2 turnos");
            Console.WriteLine("3. Puede usar su habilidad en el momento que lo desee pulsando la tecla 'G'");
            Console.WriteLine("4. Una vez utilizada su habilidad, no podra usarla nuevamente por los siguientes 5 tunos");
            Console.WriteLine("Ahora si, que comience el juego!!");
            
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    mapa[i, j] = "⬜ "; // Inicializa todo como paredes
                }
            }

            GenerarLaberinto(1,1);
            mapa[33,1] = "😠 "; // meta del jugador 1
            mapa[33,33] = "❤️ "; // meta del jugador 2
            ColocarFichasDeRecompensa(20, 8);
        }
        public void ColocarFichasDeRecompensa(int cantidadFichas, int cantidadTrampas)
        {
            Random random = new Random();
            int filas = mapa.GetLength(0);
            int columnas = mapa.GetLength(1);
            
            // Colocar fichas de recompensa
            for (int i = 0; i < cantidadFichas; i++)
            {
                int fila, columna;
                do
                {
                    fila = random.Next(1, filas - 1);
                    columna = random.Next(1, columnas - 1);
                } while ((mapa[fila, columna] != "   ") || (fila==1 && columna==1) || (fila == 1 && columna == 33)); // Asegurarse que el espacio esté vacío

                mapa[fila, columna] = "💎 ";
            }
            for (int i = 0; i < cantidadTrampas; i++)
            {
                int fila, columna;
                do
                {
                    fila = random.Next(1, filas - 1);
                    columna = random.Next(1, columnas - 1);
                } while ((mapa[fila, columna] != "   ") || (fila==1 && columna==1) || (fila == 1 && columna == 33)); // Asegurarse que el espacio esté vacío

                mapa[fila, columna] = "🧨 ";
            }    
        }
        public void GenerarLaberinto(int row, int col)
        {
            mapa[row, col] = "   "; // Marca la celda actual como espacio vacío

            var move = new(int, int)[]
            {
                (-2, 0),
                (2, 0),
                (0, 2),
                (0, -2)
            };

            move = move.OrderBy(x => random.Next()).ToArray(); // Ordena los movimientos aleatoriamente

            foreach (var (dRow, dCol) in move)
            {
                int newRow = row + dRow;
                int newCol = col + dCol;

                // Verifica si la nueva posición está dentro de los límites y es una pared
                if (newRow > 0 && newRow < Rows && newCol > 0 && newCol < Cols && mapa[newRow, newCol] == "⬜ ")
                {
                    mapa[row + dRow / 2, col + dCol / 2] = "   "; // Elimina la pared entre la celda actual y la nueva celda
                    GenerarLaberinto(newRow, newCol); // Llama recursivamente al método para la nueva celda
                }
            }
        }

        public void PrintMaze()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    // Verifica si la celda actual es la posición del Jugador 1
                    if (i == jugador1.PosicionActual.Row && j == jugador1.PosicionActual.Col)
                    {
                            Console.Write("😊 "); // Imprime al Jugador 1
                    }
                    // Verifica si la celda actual es la posición del Jugador 2
                    else if (i == jugador2.PosicionActual.Row && j == jugador2.PosicionActual.Col)
                    {
                            Console.Write("😁 "); // Imprime al Jugador 2
                    }
                    else
                    {
                        Console.Write(mapa[i, j]); 
                    }
                }
                Console.WriteLine(); 
            }

            // Imprime las posiciones de los jugadores
            jugador1.ImprimirPosicion();
            jugador2.ImprimirPosicion();

            Console.Out.Flush();
        }
        
        public bool MoverJugador(int idJugador)
        {
            // Obtener la posición actual del jugador
            //condicion ternaria
            (int currentRow, int currentCol) = idJugador == 1
                ? (jugador1.PosicionActual.Row, jugador1.PosicionActual.Col)
                : (jugador2.PosicionActual.Row, jugador2.PosicionActual.Col);

            if(idJugador==1)
            {
                // Solicitar al usuario que ingrese una tecla (W, A, S, D)
                Console.WriteLine($"Mover Jugador {idJugador}. Ingrese una tecla (W: Arriba, A: Izquierda, S: Abajo, D: Derecha) o Q para salir:");
            }
            else if (idJugador==2)
            {
                Console.WriteLine($"Mover Jugador {idJugador}. Ingrese una tecla (I: Arriba, K: Izquierda, J: Abajo, L: Derecha) o Q para salir:");
            }
            char tecla = Console.ReadKey().KeyChar;
            Console.WriteLine(); // Salto de línea

            // Calcular la nueva posición basada en la tecla ingresada
            int newRow = currentRow;
            int newCol = currentCol;

            //verificar si se quiere salir
            if (char.ToUpper(tecla) == 'q' || char.ToUpper(tecla) == 'Q') // Condición de salida
            {
                Console.WriteLine("Saliste");
                Console.ReadLine();
                Environment.Exit(0); // Salir del juego
            }

            switch (char.ToUpper(tecla))
            {
                case 'W': // Arriba
                   newRow--;
                   break;
                case 'A': // Izquierda
                    newCol--; 
                    break;
                    case 'S': // Abajo
                    newRow++;
                    break;
                case 'D': // Derecha
                    newCol++;
                    break;
                default:
                    Console.WriteLine("Tecla inválida. Use W, A, S o D.");
                    return false;
            }
                
            // Verificar si la nueva posición está dentro de los límites del laberinto
            if (newRow < 0 || newRow >= Rows || newCol < 0 || newCol >= Cols)
            {
                Console.WriteLine("Movimiento inválido: Fuera de los límites del laberinto.");
                return false;
            }

            // Verificar si la nueva posición es una pared
            if (mapa[newRow, newCol] == "⬜ ")
            {
                Console.WriteLine("Movimiento inválido: No puedes moverte a una pared.");
                return false;
            }
        
            if (mapa[newRow, newCol] == "💎 ")
            {
                // Recoger el diamante
                if (idJugador == 1)
                {
                    jugador1.RecogerDiamante();
                }
                else if (idJugador == 2)
                {
                    jugador2.RecogerDiamante();
                }

                // Limpiar la posición del diamante en el mapa
                mapa[newRow, newCol] = "   ";
            }
            else if (mapa[newRow,newCol]=="🧨 ")
            {
                if (idJugador == 1)
                {
                    jugador1.TiempoEnfriamiento = 2; // 2 turnos de enfriamiento
                }
                else if (idJugador == 2)
                {
                    jugador2.TiempoEnfriamiento = 2; // 2 turnos de enfriamiento
                }
                Console.WriteLine("¡Has caído en una trampa! Pierdes tu turno.");
                return false;
            }

            // Mover al jugador a la nueva posición
            if (idJugador == 1)
            {
                jugador1.Mover(newRow, newCol);
            }
            else if (idJugador == 2)
            {
                jugador2.Mover(newRow, newCol);
            }

            if (idJugador == 1 && newRow == metaJugador1.Row && newCol == metaJugador1.Col && jugador1.DiamantesRecogidos >= 10)
            {
                Console.WriteLine("¡Felicidades! El Jugador 1 ha llegado a su meta.");
                return true; // Victoria del Jugador 1
            }
            else if (idJugador == 2 && newRow == metaJugador2.Row && newCol == metaJugador2.Col && jugador2.DiamantesRecogidos >= 10)
            {
                Console.WriteLine("¡Felicidades! El Jugador 2 ha llegado a su meta.");
                return true; // Victoria del Jugador 2
            }

            // Imprimir el laberinto actualizado
            PrintMaze();

            return false;
        }
        public void JugarPorTurno()
        {
            while (true) // Bucle infinito hasta que el usuario decida salir
            {
                bool JuegoTerminado = false;
                while(!JuegoTerminado)
                {   
                    for(int i = 0; i < jugador1.Velocidad;i++)
                    {
                        if(jugador1.TiempoEnfriamiento > 0)
                        {
                            Console.WriteLine("Jugador 1 esta en turno de enfriamiento debido a que cayo en una trampa");
                            jugador1.TiempoEnfriamiento --;
                        }

                        else 
                        {
                            if(MoverJugador(1)==true)
                            {
                                JuegoTerminado = true; // Victoria del Jugador 1
                                break;       
                            }  
                        }
                    }

                    for(int i = 0; i < jugador2.Velocidad; i++)
                    {
                        
                        if(jugador2.TiempoEnfriamiento > 0)
                        {
                            Console.WriteLine("Jugador 2 esta en turno de enfriamiento debido a que cayo en una trampa");
                            jugador2.TiempoEnfriamiento --;
                        }
            
                        else 
                        {
                            if ( MoverJugador(2)==true)
                            {
                                JuegoTerminado = true; // Victoria del Jugador 2
                                break; 
                            } 
                        }     
                    }
                }   
            } 
        }
    }
}
