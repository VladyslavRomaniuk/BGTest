using BgLocal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BGLocalWeb.Controllers {
    [Authorize]
    public class BookController : Controller {
        public async Task<IActionResult> Index() {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync("api/books");

                var data = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<List<Book>>(data);

                return View(books);
            }
        }

        public async Task<IActionResult> GetBook(int id) {
            var book = await GetBookHelperMethod(id);

            if (book == null) {
                return RedirectToAction("Index", "Book");
            }

            return View(book);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book) {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/books", book);

                if (response.IsSuccessStatusCode) {
                    var data = await response.Content.ReadAsStringAsync();
                    var createdBook = JsonConvert.DeserializeObject<Book>(data);

                    return RedirectToAction("GetBook", new { id = createdBook.Id });
                } else {
                    return View("Error");
                }
            }
        }

        public async Task<IActionResult> Edit(int id) {
            var book = await GetBookHelperMethod(id);

            if (book == null) {
                return RedirectToAction("Index", "Book");
            }

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book) {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"api/books/{id}", book);

                if (response.IsSuccessStatusCode) {
                    return RedirectToAction("GetBook", new { id = id });
                } else {
                    return View("Error");
                }
            }
        }

        public async Task<IActionResult> Delete(int id) {
            var book = await GetBookHelperMethod(id);

            if (book == null) {
                return RedirectToAction("Index", "Book");
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int id) {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.DeleteAsync($"api/books/{id}");

                if (response.IsSuccessStatusCode) {
                    return RedirectToAction("Index");
                } else {
                    return View("Error");
                }
            }
        }

        public async Task<Book?> GetBookHelperMethod(int id) {
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = new Uri("http://api.bg-local.net:5001");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync($"api/books/{id}");

                if (response.IsSuccessStatusCode) {
                    var data = await response.Content.ReadAsStringAsync();
                    var book = JsonConvert.DeserializeObject<Book>(data);

                    return book;
                }

                return null;
            }
        }
    }
}
