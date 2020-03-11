using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarWebApi.Models
{
    public class Car
    {
        [BsonId]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [BsonElement("name")]
        [Required]
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
    }
}
