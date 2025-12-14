using GraphLib3;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLib3
{
    public class AdjacencyMatrix : IGraph
    {
        //Valeur sentinelle pour représenter aucun lien
        //entre deux noeuds
        public const int NoEdge = -1;
        private int[,] matrix;
        //readonly -> c'est comme une constante, mais qui peut
        //seulement être initialisée dans un constructeur
        //Nous n'avons pas accès à `init` dans .NET Standard 2.1
        private readonly int nNodes;

        public AdjacencyMatrix(int n)
        {
            nNodes = n;
            //peut lancer une exception si n < 0
            matrix = new int[n, n];
            //remplir le tableau2D avec des -1
            //(valeur par défaut d'un int est 0)
            FillMatrix(matrix, NoEdge);
        }

        public int NodeCount => nNodes;

        //Opération directe !
        //Performance constante peut importe n !
        public void AddEdge(int nodeA, int nodeB, int cost)
        {
            if (cost < NoEdge)
                throw new ArgumentException("le coût ne doit pas être négatif");
            matrix[nodeA, nodeB] = cost;
        }

        //Opération directe !
        //Performance constante peut importe n !
        public void RemoveEdge(int nodeA, int nodeB)
        {
            AddEdge(nodeA, nodeB, NoEdge);
        }

        //Opération directe !
        //Performance constante peut importe n !
        public int GetEdgeCost(int nodeA, int nodeB)
        {
            return matrix[nodeA, nodeB];
        }
        //Opération directe !
        //Performance constante peut importe n !
        public bool HasEdge(int nodeA, int nodeB)
        {
            return GetEdgeCost(nodeA, nodeB) != NoEdge;
        }

        //Opération avec performance linéaire selon n
        public IEnumerable<(int, int)> GetOutgoingEdges(int node)
        {
            //(type, type) -> tuple -> struct : type valeur
            //(int, int) pour représenter un noeud et un coût vers le noeud

            //Capacité initiale approximative de n/2 (c'est un guess !)
            List<(int, int)> outgoingEdges = new List<(int, int)>(NodeCount / 2);

            for (int j = 0; j < NodeCount; ++j)
            {
                int cost = matrix[node, j];
                if (cost != NoEdge)
                    outgoingEdges.Add((j, cost));
            }

            //retourner un tableau au lieu de la liste
            //un tableau a une taille fixe; le code client n'a pas
            //besoin des fonctionnalités additionnelles d'une liste
            return outgoingEdges.ToArray();
        }

        //inefficace de toujours recréer la string...
        public override string ToString()
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int maxWidth = FindMaxValueWidth(matrix, NoEdge);
            int capacity = ComputeStringBuilderCapacity(rows, cols, maxWidth);

            var sb = new StringBuilder(capacity);

            sb.Append(' ', maxWidth + 1);
            for (int col = 0; col < cols; col++)
            {
                sb.Append(col.ToString().PadLeft(maxWidth));
                sb.Append(' ');
            }
            sb.AppendLine();

            sb.Append(' ', maxWidth + 1);
            string dashBlock = new string('-', maxWidth) + "-";
            for (int col = 0; col < cols; col++)
            {
                sb.Append(dashBlock);
            }
            sb.AppendLine();

            for (int row = 0; row < rows; row++)
            {
                sb.Append(row);
                sb.Append('|');
                sb.Append(' ');

                for (int col = 0; col < cols; col++)
                {
                    int value = matrix[row, col];
                    if (value != NoEdge)
                    {
                        sb.Append(value.ToString().PadLeft(maxWidth));
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append(new string('-', maxWidth));
                        sb.Append(' ');
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
        private static int FindMaxValueWidth(int[,] data, int noEdgeValue)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            int maxWidth = 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int value = data[i, j];
                    if (value == noEdgeValue)
                        continue;

                    int len = value.ToString().Length;
                    if (len > maxWidth)
                        maxWidth = len;
                }
            }

            return maxWidth;
        }
        private static int ComputeStringBuilderCapacity(int rows, int cols, int maxWidth)
        {
            int newlineLen = Environment.NewLine.Length;

            int headerLineLength = (maxWidth + 1) * (cols + 1) + newlineLen;
            int headersTotal = 2 * headerLineLength;

            int rowLabelMaxLength = (rows - 1).ToString().Length + 2;

            int dataRowLength = rowLabelMaxLength + cols * (maxWidth + 1) + newlineLen;
            int dataRowsTotal = rows * dataRowLength;

            return headersTotal + dataRowsTotal;
        }
        public static void FillMatrix(int[,] matrix, int value)
        {
            //Sur un tableau2D, on peut faire
            //tableau2D.GetLenght(0 ou 1) pour obtenir
            //le nombre de lignes ou de colonnes
            int nLines = matrix.GetLength(0);
            int nCols = matrix.GetLength(1);

            for (int i = 0; i < nLines; ++i)
            {
                for (int j = 0; j < nCols; ++j)
                {
                    matrix[i, j] = value;
                }
            }
        }
    }
}
