using System.ComponentModel.DataAnnotations;
using MovieTop.Validation;

namespace MovieTop.Models;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "введите название фильма")]
    [StringLength(100, ErrorMessage = "название не должно превышать 100 символов")]
    [NoDigits]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "введите режиссёра")]
    [StringLength(100, ErrorMessage = "имя режиссёра не должно превышать 100 символов")]
    public string Director { get; set; } = "";

    [Required(ErrorMessage = "введите жанр")]
    [StringLength(50, ErrorMessage = "жанр не должен превышать 50 символов")]
    public string Genre { get; set; } = "";

    [Range(1888, 2100, ErrorMessage = "введите корректный год")]
    public int Year { get; set; }

    public string Poster { get; set; } = "";

    [Required(ErrorMessage = "введите описание")]
    [StringLength(1000, ErrorMessage = "описание не должно превышать 1000 символов")]
    public string Description { get; set; } = "";
}