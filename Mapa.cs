using System.Diagnostics;
namespace Project
{
    public class MazeGenerator
    {
        #region Variables
        private string NameGame;
        public string[,] mapa;
        public int Rows;
        public int Cols;
        private Random random;
        private (int, int, int) poss1;
        private (int, int, int) poss2;
        public Jugador jugador1;
        public Jugador jugador2;
        private (int Row, int Col) metaJugador1 = (33, 1); // Meta del Jugador 1
        private (int Row, int Col) metaJugador2 = (33, 33); //Meta del jugador 2
        #endregion

        public MazeGenerator(int rows, int cols)
        {
            NameGame = "Juego de Diamantes";
            Rows = rows;
            Cols = cols;
            mapa = new string[Rows, Cols];
            random = new Random();
            poss1 = (1, 1, 1);
            poss2 = (2, 1, 33);
            InitializeMaze();
        }

        private void InitializeMaze()
        {
            Console.WriteLine($"Bienvenidos al {NameGame}");
            Console.WriteLine("Por favor, escriba el numero del personaje que desea para el jugador 1:");
            Console.WriteLine("Nota: Su ficha es => ⚪ Meta correspondiente => 🏳️");
            Console.WriteLine("1. Personaje 1  Habilidad: Saltar paredes                                                      Velocidad: 4 casillas");
            Console.WriteLine("2. Personaje 2  Habilidad: Saltar trampas                                                      Velocidad: 5 casillas");
            Console.WriteLine("3. Personaje 3  Habilidad: Cambiar su cantidad de diamantes por la de su oponente              Velocidad: 8 casillas");
            Console.WriteLine("4. Personaje 4  Habilidad: Sumarse dos diamantes                                                 Velocidad: 4 casillas");
            Console.WriteLine("5. Personaje 5  Habilidad: Habilidad: Intercambiar posicion con su oponente                    Velocidad: 10 casillas");

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
                    jugador1 = new Personaje1(poss1, this); // Posición inicial del jugador 1
                    break;
                case 2:
                    jugador1 = new Personaje2(poss1, this); // Posición inicial del jugador 1
                    break;
                case 3:
                    jugador1 = new Personaje3(poss1, this); // Posición inicial del jugador 1
                    break;
                case 4:
                    jugador1 = new Personaje4(poss1, this); // Posición inicial del jugador 1
                    break;
                case 5:
                    jugador1 = new Personaje5(poss1, this);//, this); // Posición inicial del jugador 1
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }


            Console.WriteLine("Por favor, escriba el numero del personaje que desea para el jugador 2:");
            Console.WriteLine("Nota: Su ficha es => ⚫ Meta correspondiente => 🏴");
            Console.WriteLine("1. Personaje 1  Habilidad: Saltar paredes                                                      Velocidad: 4 casillas");
            Console.WriteLine("2. Personaje 2  Habilidad: Saltar trampas                                                      Velocidad: 5 casillas");
            Console.WriteLine("3. Personaje 3  Habilidad: Cambiar su cantidad de diamantes por la de su oponente              Velocidad: 8 casillas");
            Console.WriteLine("4. Personaje 4  Habilidad: Sumarse dos diamante                                                Velocidad: 4 casillas");
            Console.WriteLine("5. Personaje 5  Habilidad: Intercambiar posicion con su oponente                               Velocidad: 10 casillas");

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
                    jugador2 = new Personaje1(poss2, this); // Posición inicial del jugador 2
                    break;
                case 2:
                    jugador2 = new Personaje2(poss2, this); // Posición inicial del jugador 2
                    break;
                case 3:
                    jugador2 = new Personaje3(poss2, this); // Posición inicial del jugador 2
                    break;
                case 4:
                    jugador2 = new Personaje4(poss2, this); // Posición inicial del jugador 2
                    break;
                case 5:
                    jugador2 = new Personaje5(poss2, this);//, this); // Posición inicial del jugador 2
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }


            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    mapa[i, j] = "⬜ "; // Inicializa todo como paredes
                }
            }

            GenerarLaberinto(1, 1);
            mapa[33, 1] = "🏳️ "; // meta del jugador 1
            mapa[33, 33] = "🏴 "; // meta del jugador 2
            ColocarFichasDeRecompensa(20, 4, 4, 3);
        }

        public void ColocarFichasDeRecompensa(int cantidadFichas, int cantidadTrampas1, int cantidadTrampas2, int cantidadTrampas3)
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
                } while ((mapa[fila, columna] != "   ") || (fila == 1 && columna == 1) || (fila == 1 && columna == 33)); // Asegurarse que el espacio esté vacío

                mapa[fila, columna] = "💎 ";
            }
            
            for (int i = 0; i < cantidadTrampas1; i++)
            {
                int fila, columna;
                do
                {
                    fila = random.Next(1, filas - 1);
                    columna = random.Next(1, columnas - 1);
                } while ((mapa[fila, columna] != "   ") || (fila == 1 && columna == 1) || (fila == 1 && columna == 33)); // Asegurarse que el espacio esté vacío

                mapa[fila, columna] = "🧨 ";
            }
            
            for (int i = 0; i < cantidadTrampas2; i++)
            {
                int fila, columna;
                do
                {
                    fila = random.Next(1, filas - 1);
                    columna = random.Next(1, columnas - 1);
                } while ((mapa[fila, columna] != "   ") || (fila == 1 && columna == 1) || (fila == 1 && columna == 33)); // Asegurarse que el espacio esté vacío

                mapa[fila, columna] = "👿 ";
            }
             
            for (int i = 0; i < cantidadTrampas2; i++)
            {
                int fila, columna;
                do
                {
                    fila = random.Next(1, filas - 1);
                    columna = random.Next(1, columnas - 1);
                } while ((mapa[fila, columna] != "   ") || (fila == 1 && columna == 1) || (fila == 1 && columna == 33)); // Asegurarse que el espacio esté vacío

                mapa[fila, columna] = "☠️  ";
            }
        }
        
        public void GenerarLaberinto(int row, int col)
        {
            mapa[row, col] = "   "; // Marca la celda actual como espacio vacío

            var move = new (int, int)[]
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
            Console.WriteLine("Las reglas del juego son las siguientes:");
            Console.WriteLine("1. Cada jugador debe recoger 10 diamantes y llevarlos hacia la meta que le corresponde. El primer jugador que lo logre, gana");
            Console.WriteLine("2. Puede usar su habilidad en el momento que lo desee pulsando la tecla 'G'");
            Console.WriteLine("3. Una vez utilizada su habilidad, no podra usarla nuevamente por los siguientes 5 tunos");
            Console.WriteLine("4. Cuando veas una comentario presiona Enter");
            Console.WriteLine("OJO: Hay tres tipos de trampas: pueden hacer que pierda su turno, o que lo devuelva a su posicion inicial e incluso se le puede disminuir su cantidad de diamantes");
            Console.WriteLine("Ahora si, que comience el juego!!");
            Console.WriteLine();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    // Verifica si la celda actual es la posición del Jugador 1
                    if (i == jugador1.PosicionActual.Row && j == jugador1.PosicionActual.Col)
                    {
                        Console.Write("⚪ "); // Imprime al Jugador 1
                    }
                    // Verifica si la celda actual es la posición del Jugador 2
                    else if (i == jugador2.PosicionActual.Row && j == jugador2.PosicionActual.Col)
                    {
                        Console.Write("⚫ "); // Imprime al Jugador 2
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
        }

        public bool MoverJugador(int idJugador)
        {
            // Obtener la posición actual del jugador
            //condicion ternaria
            (int currentRow, int currentCol) = idJugador == 1
                ? (jugador1.PosicionActual.Row, jugador1.PosicionActual.Col)
                : (jugador2.PosicionActual.Row, jugador2.PosicionActual.Col);

            if (idJugador == 1)
            {
                // Solicitar al usuario que ingrese una tecla (W, A, S, D)
                Console.WriteLine($"Mover Jugador {idJugador}. Ingrese una tecla (W: Arriba, A: Izquierda, S: Abajo, D: Derecha), G para activar su habilidad o Q para salir:");
            }
            else if (idJugador == 2)
            {
                Console.WriteLine($"Mover Jugador {idJugador}. Ingrese una tecla (I: Arriba, J: Izquierda, K: Abajo, L: Derecha) G para activar su habilidad o Q para salir:");
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

            switch (idJugador)
            {
                case 1:
                    switch (char.ToUpper(tecla))
                    {
                        case 'G': // Activar habilidad del jugador 1
                            if (jugador1.HabilidadDisponible == true)
                            {
                                jugador1.ActivarHabilidad();
                                jugador1.TurnosHastaHabilidad += 5;
                            }
                            else
                            {
                                Console.WriteLine("La habilidad no está disponible en este momento. Vuelva a intentar pasados 5 turnos");
                                Console.ReadLine();
                                Console.Clear();
                                PrintMaze();
                            }
                            return false;
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
                            Console.WriteLine("Tecla inválida. Use W, A, S, D o G.");
                            Console.ReadLine();
                            Console.Clear();
                            PrintMaze();
                            return MoverJugador(idJugador);
                    }
                    break;

                case 2:
                    switch (char.ToUpper(tecla))
                    {
                        case 'G':
                            if (jugador2.HabilidadDisponible == true)
                            {
                                // Activar la habilidad del jugador 2
                                jugador2.ActivarHabilidad();
                                jugador2.TurnosHastaHabilidad += 5;
                            }
                            else
                            {
                                Console.WriteLine("La habilidad no está disponible en este momento. Vuelva a intentar pasados 5 turnos");
                                Console.ReadLine();
                                Console.Clear();
                                PrintMaze();
                            }
                            return false;
                        case 'I': // Arriba
                            newRow--;
                            break;
                        case 'J': // Izquierda
                            newCol--;
                            break;
                        case 'K': // Abajo
                            newRow++;
                            break;
                        case 'L': // Derecha
                            newCol++;
                            break;
                        default:
                            Console.WriteLine("Tecla inválida. Use I, J, K, L o G.");
                            Console.ReadLine();
                            Console.Clear();
                            PrintMaze();
                            return MoverJugador(idJugador);
                    }
                    break;
            }

            // Verificar si la nueva posición está dentro de los límites del laberinto
            if (newRow < 0 || newRow >= Rows || newCol < 0 || newCol >= Cols)
            {
                Console.WriteLine("Movimiento inválido: Fuera de los límites del laberinto.");
                Console.ReadLine();
                Console.Clear();
                PrintMaze();
                return MoverJugador(idJugador);
            }

            // Verificar si la nueva posición es una pared
            if (mapa[newRow, newCol] == "⬜ ")
            {
                Console.WriteLine("Movimiento inválido: No puedes moverte a una pared.");
                Console.ReadLine();
                Console.Clear();
                PrintMaze();
                return MoverJugador(idJugador);
            }

            //Efecto de los diamantes
            if (mapa[newRow, newCol] == "💎 ")
            {
                // Recoger el diamante
                if (idJugador == 1)
                {
                    jugador1.RecogerDiamante();
                    jugador1.Mover(newRow,newCol);
                }
                else if (idJugador == 2)
                {
                    jugador2.RecogerDiamante();
                    jugador2.Mover(newRow,newCol);
                }

                // Limpiar la posición del diamante en el mapa
                mapa[newRow, newCol] = "   ";
                PrintMaze();
                return false;
            }
            
            //Efecto de la trampa 1 : saltar truno
            if (mapa[newRow, newCol] == "🧨 ")
            {
                if (idJugador == 1)
                {
                    jugador1.Mover(newRow,newCol);
                    jugador1.CaerTrampa();
                    PrintMaze();
                    
                    Console.WriteLine("¡Has caído en una trampa! Pierdes tu turno.");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();
                    
                    for (int i = 0; i < jugador2.Velocidad; i++)
                    {
                        MoverJugador(2);
                    }
                }
                else if (idJugador == 2)
                {
                    jugador2.Mover(newRow,newCol);
                    jugador2.CaerTrampa();
                    PrintMaze();
                    
                    Console.WriteLine("¡Has caído en una trampa! Pierdes tu turno.");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();

                    for (int i = 0; i < jugador1.Velocidad; i++)
                    {
                        MoverJugador(1);
                    }
                }
                mapa[newRow, newCol] = "   ";
                PrintMaze();
                return false;
            }

            //Efecto de la trampa 2: diamantes recogidos - 2
            if(mapa[newRow, newCol]== "👿 ")
            {
                //jugador 1 
                if (idJugador == 1 && jugador1.DiamantesRecogidos < 2)
                {
                    jugador1.Mover(newRow,newCol);
                    jugador1.CaerTrampa();
                    PrintMaze();
                    
                    Console.WriteLine("Ha caido en una trampa, se pasa el turno al otro jugador");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();
                    
                    for (int i = 0; i < jugador2.Velocidad; i++)
                    {
                        MoverJugador(2);
                    }
                }
                else if (idJugador == 1 && jugador1.DiamantesRecogidos >= 2)
                {
                    jugador1.Mover(newRow,newCol);
                    jugador1.CaerTrampa();
                    PrintMaze();
                    
                    Console.WriteLine("Ha caido en una trampa, pierde dos diamantes");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();
                    
                    //restarle dos diamantes
                    jugador1.DiamantesRecogidos -= 2;
                }

                //jugador 2
                if (idJugador == 2 && jugador2.DiamantesRecogidos < 2)
                {
                    jugador2.Mover(newRow,newCol);
                    jugador2.CaerTrampa();
                    PrintMaze();
                    
                    Console.WriteLine("Ha caido en una trampa, se pasa el turno al otro jugador");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();

                    for (int i = 0; i < jugador1.Velocidad; i++)
                    {
                        MoverJugador(1);
                    }
                }
                else if (idJugador == 2 && jugador2.DiamantesRecogidos >= 2)
                {
                    jugador2.Mover(newRow,newCol);
                    jugador2.CaerTrampa();
                    PrintMaze();
                    Console.WriteLine("Ha caido en una trampa, pierde dos diamantes");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();

                    //restarle dos diamantes
                    jugador2.DiamantesRecogidos -= 2;
                }

                mapa [newRow,newCol] = "   ";
                PrintMaze();
                return false;
            }

            //Efecto de la trampa 3: te coloca nuevamente en la entrada  
            if(mapa[newRow, newCol] == "☠️  ")
            {            
                if(idJugador==1)
                {
                    jugador1.Mover(newRow, newCol);
                    PrintMaze();
                    Console.WriteLine("Has caido en una trampa, vuelves a la posicion inicial");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();

                    jugador1.Mover(1,1);
                }
                else if (idJugador == 2)
                {
                    jugador2.Mover(newRow, newCol);
                    PrintMaze();
                    Console.WriteLine("Has caido en una trampa, vuelves a la posicion inicial");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();

                    jugador2.Mover(1,33);
                }
                mapa[newRow, newCol] = "   ";
                PrintMaze();
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
                PrintMaze();
                Console.WriteLine("¡Felicidades! El Jugador 1 ha llegado a su meta.");
                Console.ReadLine();
                PrintMaze();
                return true; // Victoria del Jugador 1
            }
            else if (idJugador == 2 && newRow == metaJugador2.Row && newCol == metaJugador2.Col && jugador2.DiamantesRecogidos >= 10)
            {
                Console.WriteLine("¡Felicidades! El Jugador 2 ha llegado a su meta.");
                Console.ReadLine();
                PrintMaze();
                return true; // Victoria del Jugador 2
            }

            // Imprimir el laberinto actualizado
            PrintMaze();
            //ResetearHabilidad();

            return false;
        }

        public void JugarPorTurno()
        {
            while (true) // Bucle infinito hasta que el usuario decida salir
            {
                bool JuegoTerminado = false;
                while (!JuegoTerminado)
                {
                    for (int i = 0; i < jugador1.Velocidad; i++)
                    {
                        if (MoverJugador(1) == true)
                        {
                            JuegoTerminado = true; // Victoria del Jugador 1
                            Environment.Exit(0);
                        }
                    }

                    for (int i = 0; i < jugador2.Velocidad; i++)
                    {

                        if (MoverJugador(2) == true)
                        {
                            JuegoTerminado = true; // Victoria del Jugador 2
                            Environment.Exit(0);
                        }
                    }
                    ResetearHabilidad();
                }
            }
        }

        public void ResetearHabilidad()
        {
            if (jugador1.HabilidadDisponible == false)
            {
                jugador1.TurnosHastaHabilidad--;
                if (jugador1.TurnosHastaHabilidad <= 0)
                {
                    Console.WriteLine("Se restauro la habilidad del jugador 1");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();
                    jugador1.HabilidadDisponible = true;
                }
            }

            if (jugador2.HabilidadDisponible == false)
            {
                jugador2.TurnosHastaHabilidad--;
                if (jugador2.TurnosHastaHabilidad <= 0)
                {
                    Console.WriteLine("Se restauro la habilidad del jugador 2");
                    Console.ReadLine();
                    Console.Clear();
                    PrintMaze();
                    jugador2.HabilidadDisponible = true;
                }
            }
        }

        public bool HayPared(int fila, int columna)
        {
            // Verifica si la posición está dentro de los límites del laberinto
            if (fila < 0 || fila >= Rows || columna < 0 || columna >= Cols)
            {
                return true; // Si está fuera del laberinto, se considera una pared
            }

            // Verifica si la celda es una pared
            return mapa[fila, columna] == "⬜ ";
        }

        public bool HayTrampa(int fila, int columna)
        {
            // Verificar si la posición está dentro de los límites del laberinto
            if (fila < 0 || fila >= Rows || columna < 0 || columna >= Cols)
            {
                return false;
            }

            // Verificar si hay una trampa en la posición
            if (mapa[fila, columna] == "🧨 " || mapa[fila,columna]=="👿 " || mapa[fila,columna] == "☠️  ")
            {
                return true;
            }

            return false;
        }

    }
}
