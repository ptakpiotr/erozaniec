using SQLite;

namespace ERozaniec.Models
{
    [Table("Parts")]
    public class PartModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public RosaryPart Part { get; set; }

        public int Order { get; set; }

        [NotNull]
        public string Name { get; set; } = default!;

        [NotNull]
        public string Description { get; set; } = default!;
    }
}