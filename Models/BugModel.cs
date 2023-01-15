using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class BugModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        public int BugId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Title { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null!;

        public DateTime OpenedDate { get; set; }

        public string UserId { get; set; } = null!;

        public bool Archived { get; set; } = false;

        public string? UserName { get; set; }

        public List<SelectListItem>? UserNameSelectList { get; set; }
    }
}
