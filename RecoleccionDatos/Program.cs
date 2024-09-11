using CsvHelper;
using CsvHelper.Configuration;
using CsvToolkit.Read;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


/*
 * Proyecto:Akademia Gnoss Formación
* Author : Héctor Serna
* Email : hectorserna@gnoss.com
* Fecha : 11/90/2024
* Version:1.0 .
* Documentacion: /RecoleccionDatos/bin/Debug/net6.0/html/index.html
*/



/// <summary>
/// La clase RegistroPais contiene las propiedades que mapean los datos de un archivo CSV 
/// con información sobre índices de libertad económica de varios países.
/// </summary>

public class RegistroPais
{
    /// <summary>
    /// Año de estudio
    /// </summary>
    public int? Year { get; set; }
    /// <summary>
    /// Código del país sigiuendo estandr ISO
    /// </summary>
    public string ISOCode { get; set; }
    /// <summary>
    /// País escrito al completo
    /// </summary>
    public string? Countries { get; set; }
    /// <summary>
    /// indice de libertad econonómica
    /// </summary>
    public double? EconomicFreedomIndex { get; set; }
    /// <summary>
    /// Ranking del país en el periodo de estudio
    /// </summary>
    public int? Rank { get; set; }
    /// <summary>
    /// Quartil que se esta estudiando
    /// </summary>
    public int? Quartile { get; set; }
    /// <summary>
    /// Tamaño del gobierno
    /// </summary>

    public double? SizeOfGovernment { get; set; }
    /// <summary>
    ///  Sistema legal y derechos de propiedad.
    /// </summary>
    public double? LegalSysPropertyRights { get; set; }
    /// <summary>
    /// Dinero en líquido. Salud económica
    /// </summary>
    public double? SoundMoney { get; set; }
    /// <summary>
    /// Libertad para comerciar internacionalmente
    /// </summary>
    public double? FreedomTradeInternationally { get; set; }
    /// <summary>
    /// Regulación gubernamental
    /// </summary>
    public double? Regulation { get; set; }

}

//Clase para mapear las columnas del CSV con las propiedades de RegistroPais
/// <summary>
/// Clase que mapea las columnas del archivo CSV con las
/// propiedades de la clase RegistroPais.
/// </summary>
public class RegistroPaisMap : ClassMap<RegistroPais>
{
    /// <summary>
    /// Constructor que realiza el mapeo
    /// entre las cabeceras del archivo CSV 
    /// y las propiedades de la clase RegistroPais.
    /// </summary>
    public RegistroPaisMap()
    {
        Map(m => m.Year).Name("Year").Optional();
        Map(m => m.ISOCode).Name("ISO Code");
        Map(m => m.Countries).Name("Countries").Optional();
        Map(m => m.EconomicFreedomIndex).Name("Economic Freedom Summary Index").Optional();
        Map(m => m.Rank).Name("Rank").Optional();
        Map(m => m.Quartile).Name("Quartile").Optional();       
        Map(m => m.SizeOfGovernment).Name("Size of Government").Optional();
        Map(m => m.LegalSysPropertyRights).Name("Legal System & Property Rights").Optional();
        Map(m => m.SoundMoney).Name("Sound Money").Optional();
        Map(m => m.FreedomTradeInternationally).Name("Freedom to trade internationally").Optional();
        Map(m => m.Regulation).Name("Regulation").Optional();
    }
}

/// <summary>
/// Clase principal del programa que carga un archivo CSV
/// y muestra en la consola la información de los países.
/// </summary>
class Program
{
    /// <summary>
    /// Método principal que se encarga
    /// de leer el archivo CSV y mostrar los datos de los países en la consola.
    /// </summary>
    static void Main()
    {
        // Ruta del archivo CSV
        string filePath = ".\\..\\..\\..\\economicdata2000-2021.csv";
         

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
                Console.WriteLine($"Pais:{pais.Countries}\t Año:{pais.Year}\t  Rank:{pais.Rank}"); // recordatorio el uso de $ permite meter variable de forma natural dentro del texto, queda más elegante
            }
        }
    }
}
