using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {

            PartidadeXadrez partida = new PartidadeXadrez();
            while (!partida.terminada)
            {
                try
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);
                    Console.WriteLine("\nTruno: " + partida.turno);
                    Console.WriteLine("Aguardando jogada: "+ partida.jogadorAtual);

                    Console.Write("\nOrigem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    partida.validarPosicaoDeOrigem(origem);

                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentoPossiveis();

                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.Write("\nDestino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partida.realizaJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }   


            Console.ReadLine();
        }
    }
}
