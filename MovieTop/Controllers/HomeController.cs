using Microsoft.AspNetCore.Mvc;
using MovieTop.Models;

namespace MovieTop.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var movies = new List<Movie>
        {
            new Movie
            {
                Title = "Тед",
                Director = "Сет Макфарлейн",
                Genre = "комедия",
                Year = 2012,
                Poster = "/images/ted.webp",
                Description = "Джон пытается сохранить отношения с девушкой, несмотря на своего говорящего плюшевого медведя Теда."
            },

            new Movie
            {
                Title = "Тед 2",
                Director = "Сет Макфарлейн",
                Genre = "комедия",
                Year = 2015,
                Poster = "/images/ted2.webp",
                Description = "Тед хочет доказать, что он является настоящей личностью и имеет право на собственную жизнь."
            },

            new Movie
            {
                Title = "Человек-паук",
                Director = "Сэм Рэйми",
                Genre = "боевик, фантастика",
                Year = 2002,
                Poster = "/images/spiderman.jpg",
                Description = "Питер Паркер получает сверхспособности и начинает бороться с преступностью в Нью-Йорке."
            },

            new Movie
            {
                Title = "Астрал",
                Director = "Джеймс Ван",
                Genre = "ужасы",
                Year = 2010,
                Poster = "/images/astral.jpg",
                Description = "Семья сталкивается с потусторонними силами после того, как их сын впадает в загадочную кому."
            },

            new Movie
            {
                Title = "Мир Юрского периода",
                Director = "Колин Треворроу",
                Genre = "фантастика, приключения",
                Year = 2015,
                Poster = "/images/Jurassic_World.jpg",
                Description = "Огромный парк с динозаврами выходит из-под контроля после создания нового опасного хищника."
            },

            new Movie
            {
                Title = "Крик",
                Director = "Уэс Крэйвен",
                Genre = "ужасы, триллер",
                Year = 1996,
                Poster = "/images/scream.jpg",
                Description = "Таинственный убийца в маске начинает охоту на подростков небольшого американского города."
            }
        };

        return View(movies);
    }
}