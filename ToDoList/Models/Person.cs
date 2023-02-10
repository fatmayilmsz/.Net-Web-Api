using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class Person
    {
        public long Id { get; set; }
        [NotMapped] //Tablodaki alanla eşleştirmez
        public string Fullname => $"{FirstName} {LastName}"; //Interpolation
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

    }
}
