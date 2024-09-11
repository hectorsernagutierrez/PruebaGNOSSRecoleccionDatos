using CsvHelper;
using CsvHelper.Configuration;
/*
 * Proyecto:Akademia Gnoss Formación
* Author : Héctor Serna
* Email : hectorserna@gnoss.com
* Fecha : 11/90/2024
* Version:1.0
* 
*/

using System.Globalization;



public class RegistroPais
{
    public int? Year { get; set; }
    public string ISOCode { get; set; }
    public string? Countries { get; set; }
    public double? EconomicFreedomIndex { get; set; }
    public int? Rank { get; set; }
    public double? SizeOfGovernment { get; set; }
    public double? LegalSysPropertyRights { get; set; }
    public double? SoundMoney { get; set; }
    public double? FreedomTradeInternationally { get; set; }
    public double? Regulation { get; set; }

}

//Clase para mapear las columnas del CSV con las propiedades de RegistroPais
public class RegistroPaisMap : ClassMap<RegistroPais>
{
    public RegistroPaisMap()
    {
        Map(m => m.Year).Name("Year").Optional();
        Map(m => m.ISOCode).Name("ISO Code");
        Map(m => m.Countries).Name("Countries").Optional();
        Map(m => m.EconomicFreedomIndex).Name("Economic Freedom Summary Index").Optional();
        Map(m => m.Rank).Name("Rank").Optional();
        Map(m => m.SizeOfGovernment).Name("Size of Government").Optional();
        Map(m => m.LegalSysPropertyRights).Name("Legal System & Property Rights").Optional();
        Map(m => m.SoundMoney).Name("Sound Money").Optional();
        Map(m => m.FreedomTradeInternationally).Name("Freedom to trade internationally").Optional();
        Map(m => m.Regulation).Name("Regulation").Optional();
    }
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


        /*+
         *  ESTA ES LA FORMA DE HACERLO DIRECTAMENTE PARA LEER EL TEXTO, EVIDENTEMENTE LA CONSTRUCCIO HABRÍA QUE METERLA EN UN WHILE Y HACERLA MANUAL. 
         
           // Notar que File.ReadLines(filePath).ToList().First() tiene las cabeceras del fichero y por tanto al printeralo por pantalla me sale las columnas del archivo.


          // Leer el archivo CSV línea por línea
        foreach (string line in File.ReadLines(filePath)) 
        {
            // Dividir la línea en campos usando la coma como delimitador
            string[] fields = line.Split(',');

            // Procesar los campos (aquí simplemente los imprimimos)
            for (int i = 0; i < fields.Length; i++)
            {
                Console.WriteLine($"Campo {i + 1}: {fields[i]}");
            }
            Console.WriteLine(); // Nueva línea para separar registros
        }*/


        //HACERLO CON UN MAP ES MÁS ELEGANTE DEJA EL CÓDIGO MÁS FÁCIL DE MANTENER Y ESCALAR. 
        //EN CASO DE QUE EL PROYECTO FUESE GRANDE PERMITIRIA DEJAR SEPARADOS LOS MAPAS Y HACER PATRONES DE UNA MANERA MÁS LEGIBLE 


        using (var reader = new StreamReader(filePath))// Usar variable en vez de la URI directamente nos otorga granularidad y hace que el código se mantega adecuadamente.
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {

            csv.Context.RegisterClassMap<RegistroPaisMap>();// El contexto sirve para añadir el  mapa adecuadamente de las cabeceras del CSV con  los atributos de la clase en cuestion. Util cuando las cabeceras son de varias palabras.

            var Paises = csv.GetRecords<RegistroPais>().ToList();//Dentro del lector cvs obtenemos las listas de registroPais , con las instancias ya creadas gracias al mapa.

            //  Mostrar el contenido por consola con tabulaciones como delimitador
            foreach (var pais in Paises)
            {
                Console.WriteLine($"{pais.Countries}\t{pais.Year}\t{pais.Rank}"); // recordatorio el uso de $ permite meter variable de forma natural dentro del texto, queda más elegante
            }
        }
    }
}
