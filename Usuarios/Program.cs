using System.Text.Json;
using EspacioUsuario;

// Creo una única instancia HttpClient y Extraigo todo el texto del archivo JSON:
HttpClient cliente = new HttpClient();
string bodyJson = await ObtenerJson(cliente, @"https://jsonplaceholder.typicode.com/users/");

// Creo la lista de usuarios donde DESERIALIZO la info del archivo descerializado:
List<Usuario> listaUsuarios = JsonSerializer.Deserialize<List<Usuario>>(bodyJson);

// Muestro la lista para comprobar que se haya cargado todo correctamente:
MostrarUsuarios(listaUsuarios, 5);

// Ahora debo SERIALIZAR la lista de Usuarios en un archivo JSON
JsonSerializerOptions opciones = new JsonSerializerOptions
{
    WriteIndented = true
};
string stringJson = JsonSerializer.Serialize(listaUsuarios, opciones);

// Guardo el stringJson es el direcotrio de trabajo actual
string archivoJson = await GuardarJsonEnDirectorioActual("usuarios.json", stringJson);

if (File.Exists(archivoJson))
{
    FileInfo infoJson = new FileInfo(archivoJson);
    Console.WriteLine($"\nEl archivo '{infoJson.Name}' se ha guardado con éxito!\n");
}

//////////////////////////////////////////////////////////////////////////

static async Task<string> ObtenerJson( HttpClient client, string url)
{
    HttpResponseMessage respuesta = await client.GetAsync(url);
    respuesta.EnsureSuccessStatusCode();
    
    return await respuesta.Content.ReadAsStringAsync();
}

static async Task<string> GuardarJsonEnDirectorioActual(string nombreArchivoJson, string contenido)
{
    string ruta = Path.Combine(Directory.GetCurrentDirectory(), nombreArchivoJson);
    
    // Opcion (1)
    await File.WriteAllTextAsync(ruta, contenido);
    
    // Opcion (2)
    // using(StreamWriter sw = new StreamWriter(ruta))
    // {
    //     await sw.WriteAsync(contenido);
    // }

    return ruta;
}

static void MostrarUsuarios(List<Usuario> listaUsu, int cant_mostrar)
{
    Console.WriteLine("\n------------------------");
    Console.WriteLine("\tUSUARIOS");
    Console.WriteLine("------------------------");
    for (int i = 0; i < cant_mostrar && i < listaUsu.Count(); i++)
    {
        Console.WriteLine($"\nNombre: {listaUsu[i].name}");
        Console.WriteLine($"Correo electrónico: {listaUsu[i].email}");
        Console.WriteLine("--Domicilio--");
        Console.WriteLine($"\tCalle: {listaUsu[i].address.Street}");
        Console.WriteLine($"\tSuite: {listaUsu[i].address.Suite}");
        Console.WriteLine($"\tCity: {listaUsu[i].address.City}");
        Console.WriteLine($"\tZipcode: {listaUsu[i].address.Zipcode}");
        Console.WriteLine($"\tGeo: [LAT = {listaUsu[i].address.Geo.lat}];[LNG = {listaUsu[i].address.Geo.lng}]");
    }
}