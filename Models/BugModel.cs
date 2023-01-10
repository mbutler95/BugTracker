﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BugTracker.Models
{
    public class BugModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        public int BugId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime OpenedDate { get; set; }

        public int UserId { get; set; }

        public bool Archived { get; set; } = false;
    }
}