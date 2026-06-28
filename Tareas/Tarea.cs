using System.Text.Json.Serialization;

namespace EspacioTarea
{
    public class Tarea
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("completed")]
        public bool Completed { get; set; }
        // public Tarea() {}

        // public Tarea(int userId, int id, string title, bool completed)
        // {
        //     UserId = userId;
        //     Id = id;
        //     Title = title;
        //     Completed = completed;
        // }

        // public int UserId { get; set; }
        // public int Id { get => Id; set => Id = value; }
        // public string? Title { get => Title; set => Title = value; }
        // public bool Completed { get => Completed; set => Completed = value; }
    }
}