using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using MovieMVC.Models;
using System.Net;
using System.Net.Http.Json;

namespace MovieMVC.Controllers
{
    public class MovieController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Movie> movies = new List<Movie>();
            using(HttpClient client = new HttpClient())
            {
                var url = "https://localhost:44301/movie";
                HttpResponseMessage message = await client.GetAsync(url);
                var movieResponse = message.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<Movie>>(movieResponse);
            }
            return View(movies);
        }

        public async Task<IActionResult> Create()
        {
            Movie movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(Movie movie)
        {
            using(HttpClient client = new HttpClient())
            {
                var url = "https://localhost:44301/movie/Create";
                var content = JsonContent.Create(movie);
                var message = await client.PostAsync(url, content);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete()
        {
            int Id = 0;
            return View(Id);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMovie(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = "https://localhost:44301/movie/Delete";
                var delete = await client.DeleteAsync(url + "/" + Id.ToString());
            }
            return RedirectToAction("Index");
        }
    }
}
