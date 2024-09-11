using System;
using System.IO;

public class Record
{
    public int Year { get; set; }
    public string ISOCode { get; set; }
    public string Countries { get; set; }
    public double EconomicFreedomIndex { get; set; }
    public int Rank { get; set; }
    public int SizeOfGovernment { get; set; }
    public double LegalSysPropertyRights { get; set; }
    public double SoundMoney { get; set; }
    public double FreedomTradeInternationally { get; set; }
    public double Regulation { get; set; }

}
class Program
{
    static void Main()
    {
        // Ruta del archivo CSV
        string filePath = "C:\\Users\\hectorserna\\Downloads\\economicdata2000-2021.csv";

        // Verificar si el archivo existe
        if (!File.Exists(filePath))
        {
            Console.WriteLine("El archivo no existe.");
            return;
        }

        // Leer el archivo CSV línea por línea
        string line = File.ReadLines(filePath).ToArray()[4];
        {
            // Dividir la línea en campos usando la coma como delimitador
            string[] fields = line.Split(',');

            // Procesar los campos (aquí simplemente los imprimimos)
            for (int i = 0; i < fields.Length; i++)
            {
                Console.WriteLine($"Campo {i + 1}: {fields[i]}");
            }
            Console.WriteLine(); // Nueva línea para separar registros
        }
    }
}