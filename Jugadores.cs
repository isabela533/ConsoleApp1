using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Project
{
    public abstract class Jugador
    {
        public int Id { get; set; } // Identificador del jugador
        public (int Row, int Col) PosicionInicial { get; } // Posición inicial del jugador
        public (int Row, int Col) PosicionActual { get; set; } // Posición actual del jugador
        public int DiamantesRecogidos { get; set; }
        public int Trampas { get; set; }
        public abstract int Velocidad { get; }
        public int TurnosHastaHabilidad { get; set; }
        public bool HabilidadDisponible { get; set; }
        public Jugador(int id, int row, int col)
        {
            Id = id;
            PosicionActual = (row, col);
            DiamantesRecogidos = 0; // Inicializar contador de diamantes recogidos
            Trampas = 0;
            TurnosHastaHabilidad = 0;
            HabilidadDisponible = true;
        }

        public Jugador((int id, int row, int col) vector)
        {
            Id = vector.id;
            PosicionActual = (vector.row, vector.col);
            DiamantesRecogidos = 0; // Inicializar contador de diamantes recogidos
            Trampas = 0;
            TurnosHastaHabilidad = 0;
            HabilidadDisponible = true;
        }

        public void Mover(int newRow, int newCol)
        {
            PosicionActual = (newRow, newCol);
        }

        public void RecogerDiamante()
        {
            DiamantesRecogidos++;
        }

        public void CaerTrampa()
        {
            Trampas++;
        }

        public virtual void ActivarHabilidad()
        {
        }

        public virtual void DesactivarHabilidad()
        {
        }

        public void ImprimirPosicion()
        {

            Console.WriteLine($"Jugador {Id}:Posición Actual = ({PosicionActual.Row}, {PosicionActual.Col}), Diamantes Recogidos = {DiamantesRecogidos}, Trampas = {Trampas}");

        }
    }

    public class Personaje1 : Jugador
    {
        private MazeGenerator laberinto;
        public override int Velocidad => 4; // Velocidad del Personaje 1
        public Personaje1(int id, int row, int col, MazeGenerator laberinto) : base(id, row, col)
        {
            Console.WriteLine("Has elegido al Personaje 1");
            this.laberinto = laberinto;
        }

        public Personaje1((int id, int row, int col) vector, MazeGenerator laberinto) : base(vector)
        {
            Console.WriteLine("Has elegido al Personaje 1");
            this.laberinto = laberinto;
        }

        public override void ActivarHabilidad()
        {
            Console.WriteLine("Has activado la habilidad de saltar paredes");
            Console.WriteLine("Ingrese hacia qué dirección le gustaría saltar (W: Arriba, S: Abajo, A: Izquierda, D: Derecha):");
            char direccion = Console.ReadKey().KeyChar;

            // Obtener la posición actual del jugador
            int filaActual = PosicionActual.Row;
            int columnaActual = PosicionActual.Col;

            // Variables para la nueva posición después del salto
            int nuevaFila = filaActual;
            int nuevaColumna = columnaActual;

            // Calcular la nueva posición basada en la dirección
            switch (char.ToUpper(direccion))
            {
                case 'W': // Arriba
                    nuevaFila -= 2;
                    break;

                case 'S': // Abajo
                    nuevaFila += 2;
                    break;

                case 'A': // Izquierda
                    nuevaColumna -= 2;
                    break;

                case 'D': // Derecha
                    nuevaColumna += 2;
                    break;

                default:
                    Console.WriteLine("Dirección inválida.");
                    return;
            }

            // Verificar si la nueva posición está dentro de los límites del laberinto
            if (nuevaFila < 0 || nuevaFila >= laberinto.Rows || nuevaColumna < 0 || nuevaColumna >= laberinto.Cols)
            {
                Console.WriteLine("No puedes saltar fuera de los límites del laberinto.");
                return;
            }


            if (laberinto.HayPared(nuevaFila, nuevaColumna))
            {
                Console.WriteLine("Por favor, salte hacia un lugar donde haya un espacio vacio");
            }

            // Verificar si hay una pared en la dirección seleccionada
            if (laberinto.HayPared(filaActual + (nuevaFila - filaActual) / 2, columnaActual + (nuevaColumna - columnaActual) / 2))
            {
                // Mover al jugador a la nueva posición
                PosicionActual = (nuevaFila, nuevaColumna);
                HabilidadDisponible = false;
                laberinto.ResetearHabilidad();
            }
            else
            {
                Console.WriteLine("No hay una pared en esa dirección.");
            }

            laberinto.PrintMaze();

        }

    }

    public class Personaje2 : Jugador
    {
        private MazeGenerator laberinto;
        public override int Velocidad => 5;
        public Personaje2(int id, int row, int col, MazeGenerator laberinto) : base(id, row, col)
        {
            Console.WriteLine("Has elegido al Personaje 2");
            this.laberinto = laberinto;
        }

        public Personaje2((int id, int row, int col) vector, MazeGenerator laberinto) : base(vector)
        {
            Console.WriteLine("Has elegido al Personaje 2");
            this.laberinto = laberinto;
        }

        public override void ActivarHabilidad()
        {
            Console.WriteLine("Has activado la habilidad de saltar trampas");
            Console.WriteLine("Ingrese hacia qué dirección le gustaría saltar (W: Arriba, S: Abajo, A: Izquierda, D: Derecha):");
            char direccion = Console.ReadKey().KeyChar;

            // Obtener la posición actual del jugador
            int filaActual = PosicionActual.Row;
            int columnaActual = PosicionActual.Col;

            // Variables para la nueva posición después del salto
            int nuevaFila = filaActual;
            int nuevaColumna = columnaActual;

            // Calcular la nueva posición basada en la dirección
            switch (char.ToUpper(direccion))
            {
                case 'W': // Arriba
                    nuevaFila -= 2;
                    break;

                case 'S': // Abajo
                    nuevaFila += 2;
                    break;

                case 'A': // Izquierda
                    nuevaColumna -= 2;
                    break;

                case 'D': // Derecha
                    nuevaColumna += 2;
                    break;

                default:
                    Console.WriteLine("Dirección inválida.");
                    return;
            }

            // Verificar si la nueva posición está dentro de los límites del laberinto
            if (nuevaFila < 0 || nuevaFila >= laberinto.Rows || nuevaColumna < 0 || nuevaColumna >= laberinto.Cols)
            {
                Console.WriteLine("No puedes saltar fuera de los límites del laberinto.");
                return;
            }

            if (laberinto.HayPared(nuevaFila, nuevaColumna))
            {
                Console.WriteLine("Por favor, salte hacia un lugar donde haya un espacio vacio");
            }

            // Verificar si hay una trampa en la nueva posición
            if (laberinto.HayTrampa(filaActual + (nuevaFila - filaActual) / 2, columnaActual + (nuevaColumna - columnaActual) / 2))
            {
                // Saltar la trampa
                PosicionActual = (nuevaFila, nuevaColumna);
                HabilidadDisponible = false;
                laberinto.ResetearHabilidad();
            }
            else
            {
                Console.WriteLine("No hay una trampa en esa dirección.");
            }
            laberinto.PrintMaze();
        }
    }

    public class Personaje3 : Jugador
    {
        public override int Velocidad => 8;
        public Personaje3(int id, int row, int col) : base(id, row, col)
        {
            Console.WriteLine("Has elegido al Personaje 3");
        }

        public Personaje3((int id, int row, int col) vector) : base(vector)
        {
            Console.WriteLine("Has elegido al Personaje 3");
        }
    }

    public class Personaje4 : Jugador
    {
        private MazeGenerator laberinto;
        public override int Velocidad => 7;
        public Personaje4(int id, int row, int col, MazeGenerator laberinto) : base(id, row, col)
        {
            Console.WriteLine("Has elegido al Personaje 4");
            this.laberinto = laberinto;
        }

        public Personaje4((int id, int row, int col) vector, MazeGenerator laberinto) : base(vector)
        {
            Console.WriteLine("Has elegido al Personaje 4");
            this.laberinto = laberinto;
        }

        public override void ActivarHabilidad()
        {
            Console.WriteLine("Has activado la habilidad de sumarse un diamante a su contador");
            DiamantesRecogidos += 2;
            HabilidadDisponible = false;
            laberinto.ResetearHabilidad();
        }
    }

    public class Personaje5 : Jugador
    {
        public override int Velocidad => 10;
        public Personaje5(int id, int row, int col) : base(id, row, col)
        {
            Console.WriteLine("Has elegido al personaje 5");
        }

        public Personaje5((int id, int row, int col) vector) : base(vector)
        {
            Console.WriteLine("Has elegido al personaje 5");
        }
    }
}
