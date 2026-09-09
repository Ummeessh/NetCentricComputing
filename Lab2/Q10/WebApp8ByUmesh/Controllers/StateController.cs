using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApp8ByUmesh.Controllers
{
    public class StateController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public StateController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            // 1. Session State (Persists across multiple requests per user)
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
            {
                HttpContext.Session.SetString("UserSession", "Session value saved for Umesh");
            }

            // 2. TempData (Persists for short-lived data across single redirect)
            TempData["Notification"] = "TempData message set in Index action!";

            // 3. MemoryCache (Persists globally across all users in app memory)
            if (!_memoryCache.TryGetValue("CacheData", out string? cacheValue))
            {
                cacheValue = $"Cached item stored at {DateTime.Now:HH:mm:ss}";
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _memoryCache.Set("CacheData", cacheValue, cacheOptions);
            }

            // Read values for display
            ViewBag.SessionVal = HttpContext.Session.GetString("UserSession");
            ViewBag.ItemVal = HttpContext.Items["RequestItem"];
            ViewBag.CacheVal = cacheValue;

            return View();
        }

        public IActionResult TestRedirect()
        {
            // TempData set in Index will still be available after this redirect
            return RedirectToAction("Index");
        }
    }
}