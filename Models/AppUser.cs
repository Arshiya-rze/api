using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public record AppUser
(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad email format")] string Email,
    [MinLength(3), MaxLength(20)] string Password,
    [MinLength(3), MaxLength(20)] string ConfirmPassword
);