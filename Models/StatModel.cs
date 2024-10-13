using SQLite;

namespace ERozaniec.Models
{
    [Table("Stats")]
    public class StatModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int NumberOfBeads { get; set; }
    }
}