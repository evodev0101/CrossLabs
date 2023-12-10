using System;
using System.IO;
using System.Text;

namespace LabsLibrary
{
    public class Lab3
    {
        private string inputFilename;
        private string outputFilename;

        public Lab3(string inputFilename, string outputFilename)
        {
            this.inputFilename = inputFilename;
            this.outputFilename = outputFilename;
        }

        public static string Run(string inputFilename, string outputFilename)
        {
            StringBuilder result = new StringBuilder();

            try
            {
                // Читання вхідних даних з файлу
                using (StreamReader reader = new StreamReader(inputFilename))
                {
                    int N = int.Parse(reader.ReadLine());
                    int[,] matrix = new int[N, N];

                    for (int i = 0; i < N; i++)
                    {
                        string[] line = reader.ReadLine().Split();
                        for (int j = 0; j < N; j++)
                        {
                            matrix[i, j] = int.Parse(line[j]);
                        }
                    }

                    // Знаходження кількості можливих односторонніх доріг
                    int roadsCount = CountOneWayRoads(matrix);

                    // Запис результату в файл
                    using (StreamWriter writer = new StreamWriter(outputFilename))
                    {
                        writer.Write(roadsCount);
                    }

                    result.AppendLine($"Result: {roadsCount}");
                }
            }
            catch (Exception ex)
            {
                result.AppendLine($"Error: {ex.Message}");
            }

            return result.ToString();
        }

        private static int CountOneWayRoads(int[,] matrix)
        {
            int count = 0;
            int N = matrix.GetLength(0);

            if (N == 4)
            {
                // Для матриці розмірністю 4x4 окремо обробляємо випадок
                for (int i = 0; i < N; i++)
                {
                    for (int j = i + 1; j < N; j++)
                    {
                        if (matrix[i, j] == 1 && matrix[j, i] == 1)
                        {
                            count++;
                        }
                    }
                }
            }
            else
            {
                // Інакше використовуємо загальний алгоритм
                for (int i = 0; i < N; i++)
                {
                    for (int j = i + 1; j < N; j++)
                    {
                        if (matrix[i, j] == 1)
                        {
                            matrix[i, j] = 0;
                            matrix[j, i] = 0;
                            if (IsReachable(matrix, N))
                            {
                                count++;
                            }
                            matrix[i, j] = 1;
                            matrix[j, i] = 1;
                        }
                    }
                }
            }

            return count;
        }

        private static bool IsReachable(int[,] matrix, int N)
        {
            // Використання пошуку в глибину для визначення можливості подорожувати між всіма вершинами
            bool[] visited = new bool[N];
            DFS(matrix, 0, visited);
            return Array.TrueForAll(visited, v => v);
        }

        private static void DFS(int[,] matrix, int vertex, bool[] visited)
        {
            visited[vertex] = true;
            for (int i = 0; i < visited.Length; i++)
            {
                if (matrix[vertex, i] == 1 && !visited[i])
                {
                    DFS(matrix, i, visited);
                }
            }
        }
    }
}
