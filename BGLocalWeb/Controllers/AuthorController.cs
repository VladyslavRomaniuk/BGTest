using BgLocal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BGLocalWeb.Controllers {
    [Authorize]
    public class AuthorController : Controller {
        public async Task<IActionResult> Index() {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync("api/authors");

                var data = await response.Content.ReadAsStringAsync();
                var authors = JsonConvert.DeserializeObject<List<Author>>(data);

                return View(authors);
            }
        }

        public async Task<IActionResult> GetAuthor(int id) {
            var author = await GetAuthorHelperMethod(id);

            if (author == null) {
                return RedirectToAction("Index", "Author");
            }

            return View(author);
        } 

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author) {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/authors", author);

                if (response.IsSuccessStatusCode) {
                    var data = await response.Content.ReadAsStringAsync();
                    var createdAuthor = JsonConvert.DeserializeObject<Author>(data);

                    return RedirectToAction("GetAuthor", new { id = createdAuthor.Id });
                } else {
                    return View("Error");
                }
            }
        }

        public async Task<IActionResult> Edit(int id) {
            var author = await GetAuthorHelperMethod(id);

            if (author == null) {
                return RedirectToAction("Index", "Author");
            }

            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Author author) {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"api/authors/{id}", author);

                if (response.IsSuccessStatusCode) {
                    return RedirectToAction("GetAuthor", new { id = id });
                } else {
                    return View("Error");
                }
            }
        }

        public async Task<IActionResult> Delete(int id) {
            var author = await GetAuthorHelperMethod(id);

            if (author == null) {
                return RedirectToAction("Index", "Author");
            }

            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int id) {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.DeleteAsync($"api/authors/{id}");

                if (response.IsSuccessStatusCode) {
                    return RedirectToAction("Index");
                } else {
                    return View("Error");
                }
            }
        }

        public async Task<Author?> GetAuthorHelperMethod(int id) {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync($"api/authors/{id}");

                if (response.IsSuccessStatusCode) {
                    var data = await response.Content.ReadAsStringAsync();
                    var author = JsonConvert.DeserializeObject<Author>(data);

                    return author;
                }

                return null;
            }
        }
    }
}
