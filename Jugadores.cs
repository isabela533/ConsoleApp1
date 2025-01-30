using System.Diagnostics;

namespace Project
{
    public abstract class Jugador
    {
        public int Id { get; } // Identificador del jugador
        public (int Row, int Col) PosicionInicial { get; } // Posición inicial del jugador
        public (int Row, int Col) PosicionActual { get; set; } // Posición actual del jugador
        public int DiamantesRecogidos { get; set; }
        public int TiempoEnfriamiento { get; set; }
        public abstract int Velocidad { get; }
        public Jugador(int id, int row, int col)
        {
            Id = id;
            PosicionActual = (row, col);
            DiamantesRecogidos = 0; // Inicializar contador de diamantes recogidos
        }

        public void Mover(int newRow, int newCol)
        {
            PosicionActual = (newRow, newCol);
        }

        public void RecogerDiamante()
        {
            DiamantesRecogidos++;
        }

        public void ImprimirPosicion()
        {
              Console.WriteLine($"Jugador {Id}: Posición Inicial = ({PosicionInicial.Row}, {PosicionInicial.Col}), Posición Actual = ({PosicionActual.Row}, {PosicionActual.Col}), Diamantes Recogidos = {DiamantesRecogidos}");
        }
        public class JugadorBase : Jugador
        {
            public override int Velocidad => 0;
            public JugadorBase(int id, int row, int col) : base(id,row,col)
            {
                Console.WriteLine();
            }
        }

        public class Personaje1 : Jugador
        {
            public override int Velocidad => 4; // Velocidad del Personaje 1
            public Personaje1(int id, int row, int col) : base(id, row, col)
            {
                Console.WriteLine("Has elegido al Personaje 1");
                Console.WriteLine("Su personaje puede saltar paredes y avanza cuatro casillas");
            }
        }

        public class Personaje2 : Jugador
        {
            public override int Velocidad => 3;
            public Personaje2(int id, int row, int col) : base(id, row, col)
            {
                Console.WriteLine("Has elegido al Personaje 2");
                Console.WriteLine("Su personaje puede saltar trampas y avanza tres casillas ");
            }
        }

        public class Personaje3 : Jugador
        {
            public override int Velocidad => 5;
            public Personaje3(int id, int row, int col) : base(id, row, col)
            {
                Console.WriteLine("Has elegido al Personaje 3");
                Console.WriteLine("Su personaje puede robar diamantes a su oponente y avanza cinco casillas ");
            }
        }

        public class Personaje4 : Jugador
        {
            public override int Velocidad => 7;
            public Personaje4(int id, int row, int col) : base(id, row, col)
            {
                Console.WriteLine("Has elegido al Personaje 4");
                Console.WriteLine("Su personaje puede sumarse dos diamantes a su contador y avanza siete casillas ");
            }
        }

        public class Personaje5 : Jugador
        {
            public override int Velocidad => 10;
            public Personaje5(int id, int row, int col) : base(id, row, col)
            {
                Console.WriteLine("Has elegido al personaje 5");
                Console.WriteLine("Su personaje no posee ninguna habilidad y avanza diez casillas");
            }
        }

    }
}