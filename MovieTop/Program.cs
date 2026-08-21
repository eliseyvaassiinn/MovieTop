using Microsoft.EntityFrameworkCore;
using MovieTop.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStatusCodePagesWithReExecute("/Home/NotFound");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieContext>();

    db.Database.EnsureCreated();

    if (!db.Movies.Any())
    {
        db.Movies.AddRange(
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
        );

        db.SaveChanges();
    }
}

app.Run();