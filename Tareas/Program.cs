using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using EspacioTarea;

// 1. Creo una instancia HttpClient()
HttpClient cliente = new HttpClient();
await GetTarea(); // invoco la funcion GetTarea() con un await ya que la solicitud es asíncrona

async Task GetTarea()
{
    var url = @"https://jsonplaceholder.typicode.com/todos/"; // dirección web donde consultamos el servicio
    
    // 2. Envío una solicitud GET a la URL especificada y verifico que la respuesta sea exitosa
    HttpResponseMessage respuesta = await cliente.GetAsync(url);
    respuesta.EnsureSuccessStatusCode();
    
    // 3. Leo y Deserializo la Respuesta
    string textoExtraido = await respuesta.Content.ReadAsStringAsync();

    // Copmruebo que la API funciona correctamente:
    // Console.WriteLine(textoExtraido.Substring(0,200));
    // Console.WriteLine();

    List<Tarea> listaTareas = JsonSerializer.Deserialize<List<Tarea>>(textoExtraido);

    // Segunda prueba: inspeccionar el primer objeto
    // Console.WriteLine(listaTareas.Count);
    // Console.WriteLine(listaTareas[3].UserId);
    // Console.WriteLine(listaTareas[3].Id);
    // Console.WriteLine(listaTareas[3].Title);
    // Console.WriteLine(listaTareas[3].Ccompleted);


    // 4. Muestro por consola la lista de tareas
    // Console.WriteLine("\n-----LISTA DE TAREAS-----\n");
    // foreach (Tarea t in listaTareas)
    // {
    //     Console.WriteLine($"-----\nTítulo: {t.Title}\nEstado: {t.Ccompleted}\n-----");
    // }

    // 5. Realizo un Filtrado entre tareas 'completadas' y 'pendientes'
    // a) FILTRADO MANUAL con LinQ de tareas completadas y pendientes al mostrar:
    Console.WriteLine("\n--- TAREAS PENDIENTES ---\n");

    foreach (var t in listaTareas.Where(t => !t.Completed))
    {
        Console.WriteLine($"-----\nTítulo: {t.Title}\nEstado: {t.Completed}\n-----");
    }

    Console.WriteLine("\n--- TAREAS COMPLETADAS ---\n");

    foreach (var t in listaTareas.Where(t => t.Completed))
    {
        Console.WriteLine($"-----\nTítulo: {t.Title}\nEstado: {t.Completed}\n-----");
    }

    // b) FILTRADO con listas separadas de tareas completadas y pendientes.
    // List<Tarea> pendientes = listaTareas.Where(t => !t.Completed).ToList();
    // List<Tarea> completadas = listaTareas.Where(t => t.Completed).ToList();
    
    // Console.WriteLine("\n--- TAREAS PENDIENTES ---\n");
    // foreach (Tarea t in pendientes)
    // {
    //     Console.WriteLine($"-----\nTítulo: {t.Title}\nEstado: {t.Completed}\n-----");
    // }
    // Console.WriteLine("\n--- TAREAS COMPLETADAS ---\n");
    // foreach (Tarea t in completadas)
    // {
    //     Console.WriteLine($"-----\nTítulo: {t.Title}\nEstado: {t.Completed}\n-----");
    // }

    //

    // Configuro el JsonSerializer() para que el texto quede indentado luego de serializar:
    JsonSerializerOptions opciones = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    // 6. A la lista completa de tareas la SERIALIZO nuevamente a formato JSON:
    string jsonTareasString = JsonSerializer.Serialize(listaTareas, opciones);
    // Console.WriteLine(jsonString.Substring(0,300));

    // 7. Guardo el resultado en un archivo llamado "tareas.json" en el directorio donde se está ejecutando la aplicación
    // a) Puedo hacerlo directamente de esta forma en una sola línea:
    // await File.WriteAllTextAsync("tareas.json", jsonTareasString);
    // b) O puedo gurdar también la ruta donde la guardo, en caso de que la necesite más tarde:
    string ruta = Path.Combine(Directory.GetCurrentDirectory(),"tareas.json");
    await File.WriteAllTextAsync(ruta, jsonTareasString);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nEl archivo se ha guardado en : '{ruta}'\n");
    Console.ResetColor();
}