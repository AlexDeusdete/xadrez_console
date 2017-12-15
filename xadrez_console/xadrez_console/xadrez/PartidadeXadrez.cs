using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidadeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get;private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidadeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incremetQtdeMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach(Peca x in capturadas)
            {
                if (x.cor == cor)
                    aux.Add(x);
            }

            return aux;
        }

        public HashSet<Peca> emJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                    aux.Add(x);
            }

            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não Existe Peça na Posição!!!!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A Peça de Origem escolhida não é sua!!!!");
            }
            if (!tab.peca(pos).existeMovPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escplhida   !!!!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
                throw new TabuleiroException("Posição de destino inválida!!!!");    
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
                jogadorAtual = Cor.Preta;
            else
                jogadorAtual = Cor.Branca;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 2, new Rei(tab, Cor.Preta));
            colocarNovaPeca('b', 5, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Rei(tab, Cor.Preta));
            

            colocarNovaPeca('b', 3, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 7, new Rei(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('h', 8, new Rei(tab, Cor.Branca));
        }
    }
}
